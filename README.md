# SmartValidations

Biblioteca de validação fluente para .NET, construída sobre SmartProblems, focada em validar propriedades de modelos (especialmente DTOs/requests) e produzir `Problems` quando regras falham.

Objetivo
- Validar propriedades de um modelo em uma única passagem usando um `RuleSet`.
- Retornar `Problems` via integração com SmartProblems (Problem, Problems, Result), sem lançar exceções.
- Facilitar validações aninhadas (objetos e coleções) e compor regras de forma fluente.

Targets e requisitos
- .NET 8, .NET 9, .NET 10.
- C# 12+ com uso de `CallerArgumentExpression` e genéricos com `INumber<T>`.
- Depende de SmartProblems para agregar e enriquecer erros.

Instalação
- Adicione SmartValidations e SmartProblems ao seu projeto (NuGet quando disponível).

Conceitos principais
- `Rules.Set()` / `Rules.Set<T>()`: cria um `RuleSet` para aplicar regras.
- `RuleSet`: DSL fluente que acumula `Problems` quando validações falham.
- `BuildInPredicates`: funções utilitárias usadas internamente pelas regras.
- `IValidable` e `ValidateFunc`: pontos de integração para validações aninhadas.

Uso básico
```csharp
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

Validações de strings e padrões
```csharp
Rules.Set()
    .NotEmpty(Name)
    .MinLength(Name, 3)
    .MaxLength(Name, 100)
    .OnlyLettersOrDigits(Username)
    .NoWhiteSpace(Username)
    .Matches(Email, @"^.+@.+\..+$", "email pattern")
    .Email(Email)
    .Url(Website)
    .HasProblems(out var problems);
```

Comparações e limites
```csharp
Rules.Set()
    .Equal(Code, "ABC", StringComparison.OrdinalIgnoreCase)
    .NotEqual(Status, "inactive")
    .Min(Age, 18)
    .Max(ItemsCount, 100)
    .MinMax(Score, 0, 100)
    .LessThan(StartDate, EndDate)
    .GreaterThanOrEqual(Quantity, 1)
    .HasProblems(out var problems);
```

Regras customizadas (`Must`)
```csharp
Rules.Set()
    .Must(Password, p => p.Length >= 8 && p.Any(char.IsDigit),
        (prop, val) => $"{prop} must contain at least 8 chars and a digit.")
    .BothMust(Start, End, (s, e) => s < e,
        (p1, p2, v1, v2) => $"{p1} must be before {p2}.")
    .HasProblems(out var problems);
```

Validações aninhadas (objetos)
```csharp
public class Order : IValidable
{
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public Address? ShippingAddress { get; set; }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<Order>()
            .NotEmpty(CustomerName)
            .GreaterThan(TotalAmount, 0)
            .NotNullNested(ShippingAddress, address => Rules.Set<Address>()
                .WithPropertyPrefix("address")
                .NotEmpty(address.Street)
                .NotEmpty(address.City)
                .NotEmpty(address.ZipCode)
                .NotEmpty(address.Country))
            .HasProblems(out problems);
    }
}
```

Validações aninhadas (coleções)
```csharp
public class OrderColl : IValidable
{
    public List<Address>? Addresses { get; set; }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<OrderColl>()
            .Nested(Addresses, address => Rules.Set<Address>()
                .WithPropertyPrefix("address")
                .NotEmpty(address.Street)
                .NotEmpty(address.City)
                .NotEmpty(address.ZipCode)
                .NotEmpty(address.Country))
            .HasProblems(out problems);
    }
}
```

Integração com SmartProblems
- Cada regra que falha adiciona um `Problem` via `Problems.InvalidParameter(...)` com metadados:
  - `rule`: nome da regra (`Rules.RuleProperty`).
  - `current`: valor atual (`Rules.CurrentValueProperty`).
  - `expected`: valor(es) esperado(s) (`Rules.ExpectedValueProperty`).
  - `pattern`: expressão regular utilizada (`Rules.PatternProperty`).
- O `RuleSet` pode ser convertido implicitamente em `Problems?` ou consultado via `HasProblems(out var problems)`.

Nomes de propriedades e prefixos
- `CallerArgumentExpression` captura o nome da propriedade automaticamente.
- `WithPropertyPrefix("prefix")` permite remover o prefixo do nome ao encadear erros de objetos aninhados.
- Coleções incluem índice no encadeamento de propriedade.

Boas práticas
- Concentre a validação em uma função por modelo de entrada (único `RuleSet`).
- Use `IValidable`/`ValidateFunc` para validar agregados e objetos aninhados.
- Prefira mensagens em `R` para internacionalização e consistência.
- Utilize `StringComparison` apropriado em regras de string.

Licença
- Consulte a licença do repositório.
