using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations;

public interface IRule<in TModel>
{
    bool HasProblems(TModel model, [NotNullWhen(true)] out Problems? problems);
}