namespace RoyalCode.SmartValidations;

public class RuleSetResources
{
    public DisplayNames DisplayNames { get; } = new();

    public string NotNullOrEmptyMessageTemplate { get; set; } = "O campo '{0}' deve ser informado";

    public string NullOrNotMessageTemplate { get; set; } = "Os campo '{0}', quando informado, deve conter um valor";

    public string BothNullOrNotMessageTemplate { get; set; } = "Ambos os campos '{0}' e '{1}' deve ser informados juntos ou não informados";

    public string NotEqualMessageTemplate { get; set; } = "O campo '{0}' não deve ser igual a '{1}'";

    public string EqualMessageTemplate { get; set; } = "O campo '{0}' deve ser igual a '{1}'";

    public string BothNotEqualMessageTemplate { get; set; } = "O campo '{0}' não deve ser igual ao campo '{1}'";

    public string BothEqualMessageTemplate { get; set; } = "O campo '{0}' deve ser igual ao campo '{1}'";

    public string MinMessageTemplate { get; set; } = "O campo '{0}' deve ter o valor mínimo de '{1}'";

    public string NullOrMinMessageTemplate { get; set; } = "O campo '{0}', quando informado, deve ter o valor mínimo de '{1}'";

    public string MaxMessageTemplate { get; set; } = "O campo '{0}' deve ter o valor máximo de '{1}'";

    public string NullOrMaxMessageTemplate { get; set; } = "O campo '{0}', quando informado, deve ter o valor máximo de '{1}'";

    public string MinMaxMessageTemplate { get; set; } = "O campo '{0}' deve ter o valor mínimo de '{1}' e máximo de '{2}'";

    public string NullOrMinMaxMessageTemplate { get; set; } = "O campo '{0}', quando informado, deve ter o valor mínimo de '{1}' e máximo de '{2}'";

    public string MinLengthMessageTemplate { get; set; } = "O campo '{0}' deve ter o comprimento mínimo de '{1}' caractere(s)";

    public string NullOrMinLengthMessageTemplate { get; set; } = "O campo '{0}', quando informado, deve ter o comprimento mínimo de '{1}' caractere(s)";

    public string MaxLengthMessageTemplate { get; set; } = "O campo '{0}' deve ter o comprimento máximo de '{1}' caractere(s)";

    public string NullOrMaxLengthMessageTemplate { get; set; } = "O campo '{0}', quando informado, deve ter o comprimento máximo de '{1}' caractere(s)";

    public string LengthMessageTemplate { get; set; } = "O campo '{0}' deve ter o comprimento entre '{1}' e '{2}' caracteres";

    public string NullOrLengthMessageTemplate { get; set; } = "O campo '{0}', quando informado, deve ter o comprimento entre '{1}' e '{2}' caracteres";

    public string LessThanMessageTemplate { get; set; } = "O valor do campo '{0}' deve ser menor que o valor do campo '{1}'";

    public string LessThanOrEqualMessageTemplate { get; set; } = "O valor do campo '{0}' deve ser menor ou igual ao valor o do campo '{1}'";

    public string GreaterThanMessageTemplate { get; set; } = "O valor do campo '{0}' deve ser maior que o valor do campo '{1}'";

    public string GreaterThanOrEqualMessageTemplate { get; set; } = "O valor do campo '{0}' deve ser maior ou igual ao valor o do campo '{1}'";
}
