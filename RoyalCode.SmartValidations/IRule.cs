using RoyalCode.OperationResults;

namespace RoyalCode.SmartValidations;

public interface IRule<in TModel, in TDependecy>
{
    ValidableResult Validate(TModel model, TDependecy dependency, ValidableResult result);
}

public interface IRule<in TModel>
{
    ValidableResult Validate(TModel model, ValidableResult result);
}