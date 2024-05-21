using System.Linq.Expressions;
using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations;

public interface IValidationRule<in TModel, in TDependecy>
{
    Result Validate(TModel model, TDependecy dependency, Result result);
}

public interface IValidationRule<in TModel>
{
    Result Validate(TModel model, Result result);
}

public static class Validations
{
    public static ValidatorBuilder<TModel> For<TModel>()
    {
        return new ValidatorBuilder<TModel>();
    }
}

public class ValidatorBuilder<TModel>
{
    private readonly List<IValidationRule<TModel>> rules = [];
    
    public ValidatorBuilder<TModel> AddRule(IValidationRule<TModel> validationRule)
    {
        rules.Add(validationRule);
        return this;
    }
}

public class PropertyExpressions
{
    
}

public class PropertyValidatorBuilder<TModel, TProperty>
{
    private readonly ValidatorBuilder<TModel> validatorBuilder;
    private readonly Expression<Func<TModel, TProperty>> propertyExpression;

    public PropertyValidatorBuilder(ValidatorBuilder<TModel> validatorBuilder, 
        Expression<Func<TModel, TProperty>> propertyExpression)
    {
        this.validatorBuilder = validatorBuilder;
        this.propertyExpression = propertyExpression;
    }
    
    
}

public interface IPropertyRule<in TModel, in TProperty>
{
    public Result Validate(TModel model, TProperty property, Result result);
}