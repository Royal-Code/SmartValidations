# SmartValidations

Fluent, model-first validation for .NET that produces structured Problems instead of exceptions.
Built on top of SmartProblems to deliver actionable, localized, and machine-readable validation results.

Why SmartValidations
- Single-pass model validation: express all rules fluently in one `RuleSet`.
- No exceptions for control flow: return `Problems` you can serialize and show to users.
- Strongly-typed and fluent: compile-time safety with `INumber<T>`, `CallerArgumentExpression`, and generics.
- First-class nested validation: validate objects and collections, automatically chaining property paths (with indexes for lists).
- Ready for APIs and UI: consistent error shapes, localization-friendly message templates, and rich metadata.

Targets and requirements
- .NET 8, .NET 9, .NET 10.
- C# 12+ using `CallerArgumentExpression` and generic math (`INumber<T>`).
- Depends on SmartProblems for `Problem` and `Problems`.

Installation
- Add SmartValidations and SmartProblems (NuGet when available).

Core concepts
- `Rules.Set()` / `Rules.Set<T>()`: creates a `RuleSet` for applying rules.
- `RuleSet`: fluent DSL that accumulates `Problems` whenever a rule fails.
- `IValidable` and `ValidateFunc`: plug-in points for nested validations.
- Implicit conversion: a `RuleSet` can be treated as `Problems?` or queried via `HasProblems(out var problems)`.
- Conditional rules: `When` and `Unless` let you apply rule groups conditionally or as alternatives.

Quick start
```csharp
using RoyalCode.SmartValidations;
using RoyalCode.SmartProblems;

public class CreateOrderRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<CreateOrderRequest>()
            .NotEmpty(CustomerName)
            .GreaterThan(TotalAmount, 0)
            .HasProblems(out problems);
    }
}
```

String and pattern rules
```csharp
var set = Rules.Set()
    .NotEmpty(Name)
    .MinLength(Name, 3)
    .MaxLength(Name, 100)
    .OnlyLettersOrDigits(Username)
    .NoWhiteSpace(Username)
    .Matches(Email, @"^.+@.+\..+$", "email pattern")
    .Email(Email)
    .Url(Website);

if (set.HasProblems(out var problems))
{
    // Serialize problems to your API response
}
```

Comparisons and ranges
```csharp
var set = Rules.Set()
    .Equal(Code, "ABC", StringComparison.OrdinalIgnoreCase)
    .NotEqual(Status, "inactive")
    .Min(Age, 18)
    .Max(ItemsCount, 100)
    .MinMax(Score, 0, 100)
    .LessThan(StartDate, EndDate)
    .GreaterThanOrEqual(Quantity, 1);
```

Conditional rules (When/Unless)
```csharp
// Apply rules only when a condition is true
var set = Rules.Set()
    .When(isGuestCheckout,
        s => s.NotEmpty(Email).Email(Email));

// Skip rules when a condition is true
set = set.Unless(hasAddressOnFile,
    s => s.NotEmpty(ShippingAddress.Street)
         .NotEmpty(ShippingAddress.City)
         .NotEmpty(ShippingAddress.ZipCode));

// Alternative groups: add problems from both if both fail
set = set.Unless(
    s => s.NotEmpty(PromoCode),        // condition group
    s => s.Min(TotalAmount, 100));     // alternative group

// Using factories/builders with prefixes preserved/normalized
set = Rules.Set<object>()
    .WithPropertyPrefix("order")
    .Unless(
        () => Rules.Set().WithPropertyPrefix("order").NotEmpty(order.CustomerId),
        s => s.NotEmpty(order.CustomerId)); // Property becomes "CustomerId" (prefix removed)
```

Custom rules with Must
```csharp
var set = Rules.Set()
    .Must(Password,
        p => p is { Length: >= 8 } && p.Any(char.IsDigit) && p.Any(char.IsUpper),
        (prop, _) => $"{prop} must contain at least 8 chars, an uppercase letter and a digit.",
        ruleName: "password.policy")
    .BothMust(Start, End,
        (s, e) => s < e,
        (p1, p2, _, _) => $"{p1} must be before {p2}.",
        ruleName: "period.order");
```

Nested validation (objects)
```csharp
public class CheckoutRequest : IValidable
{
    public string CustomerId { get; set; } = string.Empty;
    public Address? ShippingAddress { get; set; }
    public Address? BillingAddress { get; set; }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<CheckoutRequest>()
            .NotEmpty(CustomerId)
            .NotNullNested(ShippingAddress, addr => Rules.Set<Address>()
                .WithPropertyPrefix("addr")
                .NotEmpty(addr.Street)
                .NotEmpty(addr.City)
                .NotEmpty(addr.ZipCode)
                .NotEmpty(addr.Country))
            .Nested(BillingAddress, addr => Rules.Set<Address>()
                .WithPropertyPrefix("addr")
                .NotEmpty(addr.Street)
                .NotEmpty(addr.City)
                .NotEmpty(addr.ZipCode)
                .NotEmpty(addr.Country))
            .HasProblems(out problems);
    }
}
```

Nested validation (collections)
```csharp
public class Order : IValidable
{
    public List<OrderItem>? Items { get; set; }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<Order>()
            .NotEmpty(Items)
            .Nested(Items, item => Rules.Set<OrderItem>()
                .WithPropertyPrefix("item")
                .NotEmpty(item.ProductId)
                .GreaterThan(item.Quantity, 0)
                .GreaterThanOrEqual(item.Price, 0))
            .HasProblems(out problems);
    }
}
```

Validating structs (value objects)
```csharp
public readonly struct Price : IValidable
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<Price>()
            .GreaterThanOrEqual(Amount, 0)
            .NotEmpty(Currency)
            .HasProblems(out problems);
    }
}

// Replace property name with the argument name automatically
var prices = new[] { new Price(-1, ""), new Price(10, "USD") };
var set = Rules.Set().Validate((IEnumerable<Price>)prices);
```

Property names and prefixes
- Property names are captured by `CallerArgumentExpression`, so refactors keep error paths accurate.
- Use `WithPropertyPrefix("prefix")` to remove a known prefix from nested paths when chaining problems.
- Collections automatically include an index (e.g., `Items[2].Quantity`).

SmartProblems integration
- Every failing rule adds a `Problem` via `Problems.InvalidParameter(...)` with metadata like:
  - `rule` (`Rules.RuleProperty`): the rule name (e.g., `min`, `max`, `lessThan`).
  - `current` (`Rules.CurrentValueProperty`): the current value.
  - `expected` (`Rules.ExpectedValueProperty`): expected value(s), when applicable.
  - `pattern` (`Rules.PatternProperty`): regex used in `Matches/NotMatches`.
  - For dual-operand rules (`Both*`, comparisons), properties and values are attached for both operands.
  - Conditional rules (`When/Unless`) simply control whether rule groups run; metadata remains consistent for each failing rule.

Best practices
- Centralize validation per request/DTO in a single function that returns `Problems?`.
- Favor `IValidable`/`ValidateFunc` to validate aggregates and nested objects.
- Prefer message templates from `R` for localization consistency.
- Use explicit `StringComparison` for string rules.
- Avoid throwing for validation flow—return `Problems` and let callers decide.

License
- See repository license.
