# SmartValidations AI Rules

Use SmartValidations to produce `Problems` with `RuleSet` and `IValidable`.

## Core Rules

- Use `Rules.Set<T>()` inside the validated type.
- Use `Rules.Set()` outside a specific validated type.
- Finish validation with `.HasProblems(out Problems? problems)` or return the implicit `Problems?`.
- Do not throw exceptions for expected validation failures.
- Do not create `Problem` manually for normal input validation.
- Do not pass property names as strings when the validated expression can be passed directly.
- Do not use expression selectors such as `x => x.Property`; `RuleSet` uses `CallerArgumentExpression`.
- Keep `RuleSet` local and synchronous. It is a `readonly ref struct`.
- Do not store `RuleSet` in fields.
- Do not capture `RuleSet` across `await` or async lambdas.

## Standard Shape

```csharp
public bool HasProblems(out Problems? problems)
{
    return Rules.Set<MyType>()
        .NotEmpty(Name)
        .Min(Age, 18)
        .HasProblems(out problems);
}
```

For standalone validators:

```csharp
public static Problems? Validate(string code, decimal price)
{
    return Rules.Set()
        .NotEmpty(code)
        .Min(price, 0m);
}
```

## Required And Optional Values

- Use `NotEmpty(value)` for required strings, numbers, collections, dates, GUIDs and arrays.
- Use `NotNull(value)` when only null is invalid.
- Use `NullOrNotEmpty(value)` for optional values that cannot be empty when provided.
- Use `NullOrMin`, `NullOrMax`, `NullOrMinMax`, `NullOrMinLength`, `NullOrMaxLength` and `NullOrLength` for optional bounded values.

## Numeric Rules

- Use `Min(value, min)` for inclusive lower bounds.
- Use `Max(value, max)` for inclusive upper bounds.
- Use `MinMax(value, min, max)` for inclusive ranges.
- Use `Positive(value)` for values greater than zero.
- Use `Negative(value)` for values less than zero.
- Use `Zero(value)` when only zero is valid.
- Use `NotZero(value)` when zero is invalid.
- Use `LessThan`, `LessThanOrEqual`, `GreaterThan` and `GreaterThanOrEqual` for comparisons between two values, not as the first option for fixed limits.

## Strings

- Use `Length(value, min, max)` for required exact/ranged length checks.
- Use `MinLength` and `MaxLength` for one-sided length checks.
- Use `Matches` and `NotMatches` for regex.
- Use `StartsWith`, `EndsWith`, `Contains` and `NotContain` for substring rules.
- Use `OnlyLetters`, `OnlyDigits`, `OnlyLettersOrDigits` and `NoWhiteSpace` for character rules.
- Pass `StringComparison` explicitly when case or culture matters.

## Email, URL And Date Rules

- Use `Email(value)` for email format.
- Use `Url(value)` for generic absolute URL format.
- Use `HttpsUrl(value)` when HTTPS is required.
- Use `AbsoluteUrl(value)` when an absolute URL is required.
- Use `RelativeUrl(value)` when a relative URL/path is required.
- Use `InPast`, `InFuture` and `Today` for `DateTime`, `DateTimeOffset` and `DateOnly`.
- Use `After`, `Before` and `Between` for date/time comparisons against fixed values.

## Nested Validation

- Use `Nested(value, validator)` for optional nested objects. `null` is accepted and ignored.
- Use `NotNullNested(value, validator)` for required nested objects. `null` generates a problem.
- Use `Nested(values, validator)` for optional nested collections. `null` is accepted and ignored.
- Use `NotNullNested(values, validator)` for required nested collections. `null` generates a problem.
- Use `Nested(value)` or `NotNullNested(value)` when the nested class implements `IValidable`.
- Use `Validate(value)` and `Validate(values)` for structs implementing `IValidable`.
- Use `WithPropertyPrefix` in reusable validators to remove local parameter names from problem paths.

Reusable nested validator:

```csharp
static Problems? ValidateAddress(Address address)
    => Rules.Set<Address>()
        .WithPropertyPrefix(nameof(address))
        .NotEmpty(address.Street)
        .NotEmpty(address.City)
        .NotEmpty(address.ZipCode);

return Rules.Set<Order>()
    .WithPropertyPrefix(nameof(order))
    .NotNullNested(order.ShippingAddress, address => ValidateAddress(address))
    .HasProblems(out problems);
```

## Custom And Conditional Rules

- Use `Must(value, predicate, messageFormatter, ruleName)` only when no built-in rule exists.
- Use `BothMust(value1, value2, predicate, messageFormatter, ruleName)` for custom rules involving two values.
- Always provide `ruleName` for custom rules.
- Use `When(condition, builder)` to apply a rule group only when the condition is true.
- Use `Unless(condition, builder)` to apply a rule group only when the condition is false.
- Use `Unless(conditionRules, alternativeRules)` for alternative rule groups.

## Metadata

- Rely on `RuleSet` to set metadata.
- Use `Rules.RuleProperty` for `rule`.
- Use `Rules.CurrentValueProperty` for `current`.
- Use `Rules.ExpectedValueProperty` for `expected`.
- Use `Rules.PatternProperty` for `pattern`.
- Expect `properties` and `values` on two-operand rules.

## Output Contracts

- Methods on validatable objects should return `bool HasProblems(out Problems? problems)`.
- Standalone validation helpers should return `Problems?`.
- API handlers should convert `Problems` to the SmartProblems/ProblemDetails response used by the project.

## Avoid

- Avoid manual `Problem` construction for common validation rules.
- Avoid string property names when `CallerArgumentExpression` can capture the expression.
- Avoid exceptions for invalid input.
- Avoid `Must` when a built-in `RuleSet` method exists.
- Avoid keeping `RuleSet` in long-lived state.
