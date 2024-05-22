namespace RoyalCode.SmartValidations;

/// <summary>
/// Static type for managing object validation rules.
/// </summary>
public static class Rules
{
    /// <summary>
    /// The resources used by the built-in rules.
    /// </summary>
    public static RuleSetResources Resources { get; } = new();

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
    public static RuleSet Set<T>() => new(typeof(T));
    
    public static IRuleChainBuilder<RuleChain<TModel>, TModel> Builder<TModel>()
    {
        return new RuleBuilder<TModel>();
    }

    public static TBuilder NotNull<TBuilder, TModel>(this IRuleChainBuilder<TBuilder, TModel> builder)
        where TBuilder : IRuleChainBuilder<TBuilder, TModel>
    {
        return builder.Must(DefaultRules.ModelNotNull);
    }

    private sealed class RuleBuilder<TModel> : IRuleChainBuilder<RuleChain<TModel>, TModel>
    {
        public RuleChain<TModel> Must(Rule<TModel> rule) => new(rule);
    }
}
