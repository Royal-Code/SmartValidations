namespace RoyalCode.SmartValidations;

/// <summary>
/// Tipo estático para gerir regras de validação de objetos.
/// </summary>
public static partial class Rules
{
    public static DefaultRuleSetResources Resources { get; } = new();

    public static RuleSet Set() => new();

    public static RuleSet Set<T>() => new(typeof(T));
    
    public static RuleFor<TModel> For<TModel>()
    {
        return new RuleFor<TModel>();
    }

    public static RuleChain<TModel> NotNull<TModel>(this IRuleChainBuilder<TModel> builder)
    {
        return builder.Must(DefaultRules.ModelNotNull);
    }

    public sealed class RuleFor<TModel> : IRuleChainBuilder<TModel>
    {
        public RuleChain<TModel> Must(Rule<TModel> rule)
        {
            return new RuleChain<TModel>(rule);
        }
    }
}
