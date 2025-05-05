using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations;

/// <summary>
/// Static type for managing object validation rules.
/// </summary>
public static class Rules
{
    /// <summary>
    /// Create a new rule set to apply validation rules.
    /// </summary>
    /// <returns>A new rule set.</returns>
    public static RuleSet Set() => new();

    /// <summary>
    /// Create a new rule set to apply validation rules.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    /// <returns>A new rule set.</returns>
    public static RuleSet Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]T>() => RuleSet.For<T>();
}
