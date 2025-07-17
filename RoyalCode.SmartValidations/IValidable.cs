using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations;

/// <summary>
/// It represents an object that can be validated.
/// <br />
/// The means of validation is to check for problems, i.e. errors.
/// </summary>
public interface IValidable
{
    /// <summary>
    /// Determines whether the object has a validation problem, i.e. is invalid.
    /// </summary>
    /// <param name="problems">
    /// When this method returns <c>true</c>, contains the detected <see cref="Problems"/>; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if there are validation problems (invalid); otherwise, <c>false</c> (valid).
    /// </returns>
    public bool HasProblems([NotNullWhen(true)] out Problems? problems);
}
