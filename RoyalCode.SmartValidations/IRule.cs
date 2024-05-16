using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations;

public interface IRule<in TModel, in TDependecy>
{
    Result Validate(TModel model, TDependecy dependency, Result result);
}

public interface IRule<in TModel>
{
    Result Validate(TModel model, Result result);
}