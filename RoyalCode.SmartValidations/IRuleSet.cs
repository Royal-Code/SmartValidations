using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations;

/// <summary>
/// Interface to apply validation rules and collect the result.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface IRuleSet<out TSelf>
    where TSelf: IRuleSet<TSelf>
{
    /// <summary>
    /// Add a problem to the validation result.
    /// </summary>
    /// <param name="problem">The problem to add.</param>
    /// <returns>The validation result with the problem added.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    TSelf AddProblem(Problem problem);
    
    /// <summary>
    /// Get the display name of a property.
    /// </summary>
    /// <param name="property">The property name.</param>
    /// <returns>The display name of the property.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    string GetDisplayName(string? property);

    /// <summary>
    /// Check if the validation result has problems, and return them if it has.
    /// </summary>
    /// <param name="problems">The problems found.</param>
    /// <returns>
    ///     True if the validation result has problems, otherwise false.
    ///     When all rules are valid, the problems will be null and the return will be false.
    ///     When any rule is invalid, the problems will be not null and the return will be true.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool HasProblems([NotNullWhen(true)] out Problems? problems);
}