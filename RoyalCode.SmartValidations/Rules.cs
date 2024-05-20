using RoyalCode.SmartValidations.Validations;

namespace RoyalCode.SmartValidations;

/// <summary>
/// Tipo estático para gerir regras de validação de objetos.
/// </summary>
public static class Rules
{
    public static DefaultRulesResources Resources { get; } = new DefaultRulesResources();

    public static RuleSet Set() => new();

    public static RuleSet Set<T>() => new(typeof(T));
}
