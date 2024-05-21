using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations;

public static class BuildInRules
{
    #region NotNull
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TSelf NotNull<TSelf, TValue>(
        this TSelf ruleSet,
        [NotNullWhen(true)] TValue value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
        where TSelf: IRuleSet<TSelf>
    {
        return value is not null 
            ? ruleSet 
            : ruleSet.NullOrEmptyProblem(property);
    }

    #endregion
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TSelf NotEmpty<TSelf>(
        this TSelf ruleSet,
        [NotNullWhen(true)] string? value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
        where TSelf: IRuleSet<TSelf>
    {
        return BuildInPredicates.NotEmpty(value)
            ? ruleSet 
            : ruleSet.NullOrEmptyProblem(property);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TSelf NullOrEmptyProblem<TSelf>(this TSelf ruleSet, string? property)
        where TSelf: IRuleSet<TSelf> 
    {
        var propertyName = ruleSet.GetDisplayName(property);

        return ruleSet.AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.NotNullOrEmptyMessageTemplate, propertyName), property));
    }
}