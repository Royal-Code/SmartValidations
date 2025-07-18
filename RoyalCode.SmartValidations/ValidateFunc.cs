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
