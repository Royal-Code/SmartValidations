using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations;

/// <summary>
/// Static type for managing object validation rules.
/// </summary>
public static class Rules
{
    /// <summary>
    /// Create a new rule set to apply validation rules.
    /// </summary>
    /// <returns>A new rule set.</returns>
    public static RuleSet Set() => new();

    /// <summary>
    /// Create a new rule set to apply validation rules.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    /// <returns>A new rule set.</returns>
    public static RuleSet Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]T>() => RuleSet.For<T>();

    /// <summary>
    /// The extension property name for rules name.
    /// </summary>
    public const string RuleProperty = "rule";

    /// <summary>
    /// The extension property name for current value.
    /// </summary>
    public const string CurrentValueProperty = "current";

    /// <summary>
    /// The extension property name for expected value.
    /// </summary>
    public const string ExpectedValueProperty = "expected";

    /// <summary>
    /// The extension property name for pattern value.
    /// </summary>
    public const string PatternProperty = "pattern";

    /// <summary>
    /// Rule name for null or empty validation.
    /// </summary>
    public const string NotNullOrNotEmpty = "not-null-or-not-empty";

    /// <summary>
    /// Rule name for null or not empty validation.
    /// </summary>
    public const string NullOrNotEmpty = "null-or-not-empty";

    /// <summary>
    /// Rule name for both null or not validation.
    /// </summary>
    public const string BothNullOrNot = "both-null-or-not";

    /// <summary>
    /// Rule name for equals validation.
    /// </summary>
    public new const string Equals = "equals";

    /// <summary>
    /// Rule name for not equals validation.
    /// </summary>
    public const string NotEquals = "not-equals";

    /// <summary>
    /// Rule name for both equals validation.
    /// </summary>
    public const string BothEqual = "both-equals";

    /// <summary>
    /// Rule name for both not equals validation.
    /// </summary>
    public const string BothNotEqual = "both-not-equals";

    /// <summary>
    /// Rule name for match pattern validation.
    /// </summary>
    public const string MatchPattern = "match-pattern";

    /// <summary>
    /// Rule name for not match pattern validation.
    /// </summary>
    public const string NotMatchPattern = "not-match-pattern";

    /// <summary>
    /// Rule name for starts with validation.
    /// </summary>
    public const string StartsWith = "starts-with";

    /// <summary>
    /// Rule name for ends with validation.
    /// </summary>
    public const string EndsWith = "ends-with";

    /// <summary>
    /// Rule name for contains validation.
    /// </summary>
    public const string Contains = "contains";

    /// <summary>
    /// Rule name for does not contain validation.
    /// </summary>
    public const string NotContain = "not-contain";

    /// <summary>
    /// Rule name for only letters validation.
    /// </summary>
    public const string OnlyLetters = "only-letters";

    /// <summary>
    /// Rule name for only digits validation.
    /// </summary>
    public const string OnlyDigits = "only-digits";

    /// <summary>
    /// Rule name for only letters or digits validation.
    /// </summary>
    public const string OnlyLettersOrDigits = "only-letters-or-digits";

    /// <summary>
    /// Rule name for no white space validation.
    /// </summary>
    public const string NoWhiteSpace = "no-white-space";

    /// <summary>
    /// Rule name for Minimum value validation.
    /// </summary>
    public const string Min = "min";

    /// <summary>
    /// Rule name for Maximum value validation.
    /// </summary>
    public const string Max = "max";

    /// <summary>
    /// Rule name for Minimum and Maximum value validation.
    /// </summary>
    public const string MinMax = "min-max";

    /// <summary>
    /// Rule name for null or Minimum value validation.
    /// </summary>
    public const string NullOrMin = "null-or-min";

    /// <summary>
    /// Rule name for null or Maximum value validation.
    /// </summary>
    public const string NullOrMax = "null-or-max";

    /// <summary>
    /// Rule name for null or Minimum and Maximum value validation.
    /// </summary>
    public const string NullOrMinMax = "null-or-min-max";

    /// <summary>
    /// Rule name for maximum length validation.
    /// </summary>
    public const string MinLength = "min-length";

    /// <summary>
    /// Rule name for maximum length validation.
    /// </summary>
    public const string MaxLength = "max-length";

    /// <summary>
    /// Rule name for minimum and maximum length validation.
    /// </summary>
    public const string Length = "length";

    /// <summary>
    /// Rule name for null or maximum length validation.
    /// </summary>
    public const string NullOrMinLength = "null-or-min-length";

    /// <summary>
    /// Rule name for null or maximum length validation.
    /// </summary>
    public const string NullOrMaxLength = "null-or-max-length";

    /// <summary>
    /// Rule name for null or minimum and maximum length validation.
    /// </summary>
    public const string NullOrLength = "null-or-length";

    /// <summary>
    /// Rule name for greater than validation.
    /// </summary>
    public const string LessThan = "less-than";

    /// <summary>
    /// Rule name for greater than or equal validation.
    /// </summary>
    public const string LessThanOrEqual = "less-than-or-equal";

    /// <summary>
    /// Rule name for less than or equal validation.
    /// </summary>
    public const string GreaterThan = "greater-than";

    /// <summary>
    /// Rule name for greater than or equal validation.
    /// </summary>
    public const string GreaterThanOrEqual = "greater-than-or-equal";

    /// <summary>
    /// Rule name for email format validation.
    /// </summary>
    public const string Email = "email";

    /// <summary>
    /// Rule name for URL format validation.
    /// </summary>
    public const string Url = "url";

    /// <summary>
    /// Rule name for HTTPS URL format validation.
    /// </summary>
    public const string HttpsUrl = "https-url";

    /// <summary>
    /// Rule name for absolute URL format validation.
    /// </summary>
    public const string AbsoluteUrl = "absolute-url";

    /// <summary>
    /// Rule name for relative URL format validation.
    /// </summary>
    public const string RelativeUrl = "relative-url";

    /// <summary>
    /// Rule name for positive number validation.
    /// </summary>
    public const string Positive = "positive";

    /// <summary>
    /// Rule name for negative number validation.
    /// </summary>
    public const string Negative = "negative";

    /// <summary>
    /// Rule name for zero number validation.
    /// </summary>
    public const string Zero = "zero";

    /// <summary>
    /// Rule name for not zero number validation.
    /// </summary>
    public const string NotZero = "not-zero";

    /// <summary>
    /// Rule name for past date validation.
    /// </summary>
    public const string InPast = "in-past";

    /// <summary>
    /// Rule name for future date validation.
    /// </summary>
    public const string InFuture = "in-future";

    /// <summary>
    /// Rule name for today date validation.
    /// </summary>
    public const string Today = "today";

    /// <summary>
    /// Rule name for after date validation.
    /// </summary>
    public const string After = "after";

    /// <summary>
    /// Rule name for before date validation.
    /// </summary>
    public const string Before = "before";

    /// <summary>
    /// Rule name for date between validation.
    /// </summary>
    public const string Between = "between";
}
