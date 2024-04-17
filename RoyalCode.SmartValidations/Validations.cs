using System.Linq.Expressions;
using RoyalCode.OperationResults;

namespace RoyalCode.SmartValidations;

public static class Validations
{
    public static ValidatorBuilder<TModel> For<TModel>()
    {
        return new ValidatorBuilder<TModel>();
    }
}

public class ValidatorBuilder<TModel>
{
    private readonly List<IRule<TModel>> rules = [];
    
    public ValidatorBuilder<TModel> AddRule(IRule<TModel> rule)
    {
        rules.Add(rule);
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
    public ValidableResult Validate(TModel model, TProperty property, ValidableResult result);
}