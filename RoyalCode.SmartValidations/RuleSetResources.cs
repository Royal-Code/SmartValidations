namespace RoyalCode.SmartValidations;

#pragma warning disable CS1591 // Xml doc.
#pragma warning disable S125 // remove comment code.

/// <summary>
/// Resources used to generate built-in validations messages.
/// </summary>
public class RuleSetResources
{
    /// <summary>
    /// Names used for properties and types.
    /// </summary>
    public DisplayNames DisplayNames { get; } = new();

    public string NotNullOrEmptyMessageTemplate => R.NotNullOrEmptyMessageTemplate; // "O campo '{0}' deve ser informado";

    public string NullOrNotMessageTemplate => R.NullOrNotMessageTemplate; // "Os campo '{0}', quando informado, deve conter um valor";

    public string BothNullOrNotMessageTemplate => R.BothNullOrNotMessageTemplate; // "Ambos os campos '{0}' e '{1}' deve ser informados juntos ou não informados";

    public string NotEqualMessageTemplate => R.NotEqualMessageTemplate; // "O campo '{0}' não deve ser igual a '{1}'";

    public string EqualMessageTemplate => R.EqualMessageTemplate; // "O campo '{0}' deve ser igual a '{1}'";

    public string BothNotEqualMessageTemplate => R.BothNotEqualMessageTemplate; // "O campo '{0}' não deve ser igual ao campo '{1}'";

    public string BothEqualMessageTemplate => R.BothEqualMessageTemplate; // "O campo '{0}' deve ser igual ao campo '{1}'";

    public string MinMessageTemplate => R.MinMessageTemplate; // "O campo '{0}' deve ter o valor mínimo de '{1}'";

    public string NullOrMinMessageTemplate => R.NullOrMinMessageTemplate; // "O campo '{0}', quando informado, deve ter o valor mínimo de '{1}'";

    public string MaxMessageTemplate => R.MaxMessageTemplate; // "O campo '{0}' deve ter o valor máximo de '{1}'";

    public string NullOrMaxMessageTemplate => R.NullOrMaxMessageTemplate; // "O campo '{0}', quando informado, deve ter o valor máximo de '{1}'";

    public string MinMaxMessageTemplate => R.MinMaxMessageTemplate; // "O campo '{0}' deve ter o valor mínimo de '{1}' e máximo de '{2}'";

    public string NullOrMinMaxMessageTemplate => R.BothEqualMessageTemplate; // "O campo '{0}', quando informado, deve ter o valor mínimo de '{1}' e máximo de '{2}'";

    public string MinLengthMessageTemplate => R.MinLengthMessageTemplate; // "O campo '{0}' deve ter o comprimento mínimo de '{1}' caractere(s)";

    public string NullOrMinLengthMessageTemplate => R.NullOrMinLengthMessageTemplate; // "O campo '{0}', quando informado, deve ter o comprimento mínimo de '{1}' caractere(s)";

    public string MaxLengthMessageTemplate => R.MaxLengthMessageTemplate; // "O campo '{0}' deve ter o comprimento máximo de '{1}' caractere(s)";

    public string NullOrMaxLengthMessageTemplate => R.NullOrMaxLengthMessageTemplate; // "O campo '{0}', quando informado, deve ter o comprimento máximo de '{1}' caractere(s)";

    public string LengthMessageTemplate => R.LengthMessageTemplate; // "The '{0}' field must have a length between '{1}' and '{2}' characters";

    public string NullOrLengthMessageTemplate => R.NullOrLengthMessageTemplate; // "O campo '{0}', quando informado, deve ter o comprimento entre '{1}' e '{2}' caracteres";

    public string LessThanMessageTemplate => R.LessThanMessageTemplate; // "O valor do campo '{0}' deve ser menor que o valor do campo '{1}'";

    public string LessThanOrEqualMessageTemplate => R.LessThanOrEqualMessageTemplate; // "O valor do campo '{0}' deve ser menor ou igual ao valor o do campo '{1}'";

    public string GreaterThanMessageTemplate => R.GreaterThanMessageTemplate; // "O valor do campo '{0}' deve ser maior que o valor do campo '{1}'";

    public string GreaterThanOrEqualMessageTemplate => R.GreaterThanOrEqualMessageTemplate; // "O valor do campo '{0}' deve ser maior ou igual ao valor o do campo '{1}'";

    public string EmailMessageTemplate => R.EmailMessageTemplate; // "O campo '{0}' deve ser um e-mail válido";

    public string UrlMessageTemplate => R.UrlMessageTemplate; // "O campo '{0}' deve ser uma URL válida";

    public string EntityNotFound => R.EntityNotFound; // "O registro de '{0}' não foi encontrado";

    public string EntityNotFoundBy => R.EntityNotFoundBy; // "O registro de '{0}' com {1} '{2}' não foi encontrado";

    public string EntityNotFoundById => R.EntityNotFoundById; // "O registro de '{0}' com id '{1}' não foi encontrado";

    public string NotFoundBy<TEntity>(string propertyName, object? propertyValue)
    {
        var entityName = DisplayNames.GetDisplayName(typeof(TEntity));
        return string.Format(EntityNotFoundBy, entityName, propertyName, propertyValue);
    }
}
