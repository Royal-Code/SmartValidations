using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations;

/// <summary>
/// Represents a delegate for a validation method of a class.
/// <br />
/// The method checks for validation problems and returns whether any were found.
/// </summary>
/// <param name="problems">
/// When this method returns <c>true</c>, contains the detected <see cref="Problems"/>; otherwise, <c>null</c>.
/// </param>
/// <returns>
/// <c>true</c> if there are validation problems (invalid); otherwise, <c>false</c> (valid).
/// </returns>
public delegate bool ValidateFunc([NotNullWhen(true)] out Problems? problems);

/// <summary>
/// Represents a delegate for building or modifying a <see cref="RuleSet"/>.
/// </summary>
/// <param name="set">The <see cref="RuleSet"/> to build or modify.</param>
/// <returns>The modified <see cref="RuleSet"/>.</returns>
public delegate RuleSet RuleSetBuilder(RuleSet set);

/// <summary>
/// Represents a delegate for creating a new <see cref="RuleSet"/>.
/// </summary>
/// <returns>The created <see cref="RuleSet"/>.</returns>
public delegate RuleSet RuleSetFactory();