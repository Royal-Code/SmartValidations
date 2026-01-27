using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace RoyalCode.SmartValidations;

#pragma warning disable S1121 // Assignments should not be made from within sub-expressions

/// <summary>
/// Build-in predicates.
/// </summary>
public static class BuildInPredicates
{
    private static readonly EmailAddressAttribute EmailAddressValidator = new();
    private static readonly UrlAttribute UrlValidator = new();

    #region Mist

    /// <summary>
    /// Determines whether the specified value matches the pattern of a valid email address.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the specified value is valid, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmail([NotNullWhen(true)] string? value) 
        => value is not null && EmailAddressValidator.IsValid(value);

    /// <summary>
    /// Validates the format of the specified URL.
    /// </summary>
    /// <param name="value">The URL to validate.</param>
    /// <returns>True if the URL format is valid, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUrl([NotNullWhen(true)] string? value) 
        => value is not null && UrlValidator.IsValid(value);

    /// <summary>
    /// Validates whether the specified URL is a valid HTTPS URL.
    /// </summary>
    /// <param name="value">The URL to validate.</param>
    /// <returns>True if the URL is a valid HTTPS URL, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsHttpsUrl([NotNullWhen(true)] string? value)
        => value is not null && UrlValidator.IsValid(value) && value.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Validates whether the specified URL is a valid absolute URL.
    /// </summary>
    /// <param name="value">The URL to validate.</param>
    /// <returns>True if the URL is a valid absolute URL, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAbsoluteUrl([NotNullWhen(true)] string? value)
        => value is not null && UrlValidator.IsValid(value) && Uri.TryCreate(value, UriKind.Absolute, out _);

    /// <summary>
    /// Validates whether the specified URL is a valid relative URL.
    /// </summary>
    /// <param name="value">The URL to validate.</param>
    /// <returns>True if the URL is a valid relative URL, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRelativeUrl([NotNullWhen(true)] string? value)
        => value is not null && UrlValidator.IsValid(value) && Uri.TryCreate(value, UriKind.Relative, out _);

    #endregion

    #region String and Pattern

    /// <summary>
    /// Validates whether the specified string matches the given regular expression pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Matches(string? value, string pattern)
        => value is not null && Regex.IsMatch(value, pattern);

    /// <summary>
    /// Validates whether the specified string matches the given regular expression.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Matches(string? value, Regex regex)
        => value is not null && regex.IsMatch(value);

    /// <summary>
    /// Validates whether the specified string does not match the given regular expression pattern.
    /// Null is considered valid.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotMatches(string? value, string pattern)
        => value is null || !Regex.IsMatch(value, pattern);

    /// <summary>
    /// Validates whether the specified string does not match the given regular expression.
    /// Null is considered valid.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotMatches(string? value, Regex regex)
        => value is null || !regex.IsMatch(value);

    /// <summary>
    /// Validates whether the specified string starts with the given prefix using the comparison option.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(string? value, string prefix, StringComparison comparison = StringComparison.Ordinal)
        => value is not null && value.StartsWith(prefix, comparison);

    /// <summary>
    /// Validates whether the specified string ends with the given suffix using the comparison option.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(string? value, string suffix, StringComparison comparison = StringComparison.Ordinal)
        => value is not null && value.EndsWith(suffix, comparison);

    /// <summary>
    /// Validates whether the specified string contains the given substring using the comparison option.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(string? value, string substring, StringComparison comparison = StringComparison.Ordinal)
        => value is not null && value.Contains(substring, comparison);

    /// <summary>
    /// Validates whether the specified string does not contain the given substring using the comparison option.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotContain(string? value, string substring, StringComparison comparison = StringComparison.Ordinal)
        => value is null || !value.Contains(substring, comparison);

    /// <summary>
    /// Validates whether the specified string contains only alphabetic characters. Empty string is invalid.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool OnlyLetters(string? value)
        => value is not null && value.Length > 0 && value.All(char.IsLetter);

    /// <summary>
    /// Validates whether the specified string contains only numeric characters (digits). Empty string is invalid.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool OnlyDigits(string? value)
        => value is not null && value.Length > 0 && value.All(char.IsDigit);

    /// <summary>
    /// Validates whether the specified string contains only alphanumeric characters. Empty string is invalid.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool OnlyLettersOrDigits(string? value)
        => value is not null && value.Length > 0 && value.All(char.IsLetterOrDigit);

    /// <summary>
    /// Validates whether the specified string contains no whitespace characters. Empty string is valid.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NoWhiteSpace(string? value)
        => value is not null && !value.Any(char.IsWhiteSpace);

    #endregion

    #region NotEmpty

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T value) where T: INumber<T> 
        => value != T.Zero;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T? value) where T : struct, INumber<T>
        => value.HasValue && value.Value != T.Zero;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T[]? value) 
        => value is not null && value.Length != 0;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] ICollection<T>? value) 
        => value is not null && value.Count != 0;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] IReadOnlyCollection<T>? value) 
        => value is not null && value.Count != 0;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] IEnumerable<T>? value) 
        => value is not null && value.Any();

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateTime value) => value != DateTime.MinValue;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateTime? value)
        => value.HasValue && value.Value != DateTime.MinValue;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateTimeOffset value) 
        => value != DateTimeOffset.MinValue;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateTimeOffset? value)
        => value.HasValue && value.Value != DateTimeOffset.MinValue;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateOnly value) => value != DateOnly.MinValue;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateOnly? value)
        => value.HasValue && value.Value != DateOnly.MinValue;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(Guid value) 
        => value != Guid.Empty;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] Guid? value) 
        => value.HasValue && value.Value != Guid.Empty;

    /// <summary>
    /// Validates whether the specified value is not empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] string? value) 
        => !string.IsNullOrWhiteSpace(value);

    #endregion

    #region Not Or Not Empty

    /// <summary>
    /// Validates the value by checking that it is not empty when it is not null. This validation accepts null values.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty or null, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NullOrNotEmpty<T>(T value) where T : INumber<T>
        => value is null || value != T.Zero;

    /// <summary>
    /// Validates the value by checking that it is not empty when it is not null. This validation accepts null values.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty or null, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NullOrNotEmpty<T>(T? value) where T : struct, INumber<T>
        => !value.HasValue || value.Value != T.Zero;

    /// <summary>
    /// Validates the value by checking that it is not empty when it is not null. This validation accepts null values.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not empty or null, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NullOrNotEmpty(string? value)
        => value is null || !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Validates both values, where both must be null or, when one is filled, both must not be empty.
    /// </summary>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <returns>True if both values are null or both are not empty, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNullOrNotEmpty(string? value1, string? value2)
    {
        return (value1 is null && value2 is null)
               || (!string.IsNullOrWhiteSpace(value1) && !string.IsNullOrWhiteSpace(value2));
    }

    #endregion

    #region Equals NotEquals

    /// <summary>
    /// Validates the value of a field, ensuring that it is equal to the expected value.
    /// </summary>
    /// <param name="value">The field value to validate.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>True if the field value is equal to the expected value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equal(string? value,
        string expected,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return expected.Equals(value, comparison);
    }

    /// <summary>
    /// Validates the value of a field, ensuring that it is equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The field value to validate.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>True if the field value is equal to the expected value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equal<T>(T? value, T expected)
        where T: IEquatable<T>
    {
        return expected.Equals(value);
    }

    /// <summary>
    /// Validates the value of a field, ensuring that it is equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The field value to validate.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>True if the field value is equal to the expected value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equal<T>(T? value, T expected)
        where T : struct, IEquatable<T>
    {
        return value.HasValue && expected.Equals(value.Value);
    }

    /// <summary>
    /// Validates the value of a field, ensuring that it is not equal to the expected value.
    /// </summary>
    /// <param name="value">The field value to validate.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>True if the field value is not equal to the expected value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEqual(string? value,
        string expected,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return !expected.Equals(value, comparison);
    }

    /// <summary>
    /// Validates the value of a field, ensuring that it is not equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The field value to validate.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>True if the field value is not equal to the expected value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEqual<T>(T? value, T expected)
        where T: IEquatable<T>
    {
        return !expected.Equals(value);
    }

    /// <summary>
    /// Validates the value of a field, ensuring that it is not equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The field value to validate.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>True if the field value is not equal to the expected value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEqual<T>(T? value, T expected)
        where T : struct, IEquatable<T>
    {
        return !(value.HasValue && expected.Equals(value.Value));
    }

    /// <summary>
    /// Validates whether both values are equal.
    /// </summary>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>True if both values are equal, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothEqual(
        string? value1,
        string? value2,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return string.Equals(value1, value2, comparison);
    }

    /// <summary>
    /// Validates whether both values are equal.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <returns>True if both values are equal, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothEqual<T>(T? value1, T? value2)
        where T: IEquatable<T>
    {
        return value1 is null 
            ? value2 is null 
            : value2 is not null && value1.Equals(value2);
    }

    /// <summary>
    /// Validates whether both values are equal.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <returns>True if both values are equal, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothEqual<T>(T? value1, T? value2)
        where T : struct, IEquatable<T>
    {
        return value1 is null
            ? value2 is null
            : value2 is not null && value1.Value.Equals(value2.Value);
    }

    /// <summary>
    /// Validates whether both values are not equal.
    /// </summary>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>True if both values are not equal, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNotEqual(
        string? value1,
        string? value2,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return !string.Equals(value1, value2, comparison);
    }

    /// <summary>
    /// Validates whether both values are not equal.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <returns>True if both values are not equal, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNotEqual<T>(T? value1, T? value2)
        where T: IEquatable<T>
    {
        return !(value1 is null 
            ? value2 is null 
            : value1.Equals(value2));
    }

    /// <summary>
    /// Validates whether both values are not equal.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value1">The first value to validate.</param>
    /// <param name="value2">The second value to validate.</param>
    /// <returns>True if both values are not equal, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNotEqual<T>(T? value1, T? value2)
        where T : struct, IEquatable<T>
    {
        return !(value1 is null
            ? value2 is null
            : value2 is not null && value1.Equals(value2));
    }

    #endregion

    #region Min Max

    /// <summary>
    /// Validates whether the specified value is greater than or equal to the minimum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value.</param>
    /// <returns>True if the value is greater than or equal to the minimum value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Min<T>(T value, T min) where T: IComparable<T>
    {
        return value.CompareTo(min) >= 0;
    }

    /// <summary>
    /// Validates whether the specified value is greater than or equal to the minimum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value.</param>
    /// <returns>True if the value is greater than or equal to the minimum value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Min<T>(T? value, T min) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(min) >= 0;
    }

    /// <summary>
    /// Validates whether the specified value is less than or equal to the maximum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>True if the value is less than or equal to the maximum value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Max<T>(T value, T max) where T: IComparable<T>
    {
        return value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// Validates whether the specified value is less than or equal to the maximum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>True if the value is less than or equal to the maximum value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Max<T>(T? value, T max) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// Validates whether the specified value is within the specified range (inclusive).
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>True if the value is within the specified range (inclusive), otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MinMax<T>(T value, T min, T max) where T: IComparable<T>
    {
        return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// Validates whether the specified value is within the specified range (inclusive).
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>True if the value is within the specified range (inclusive), otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MinMax<T>(T? value, T min, T max) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(min) >= 0 && value.Value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// Validates whether the specified string value has a minimum length.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="length">The minimum length.</param>
    /// <returns>True if the string value has a minimum length, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MinLength(string? value, int length)
    {
        return value is not null && value.Length >= length;
    }

    /// <summary>
    /// Validates whether the specified string value has a maximum length.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="length">The maximum length.</param>
    /// <returns>True if the string value has a maximum length, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MaxLength(string? value, int length)
    {
        return value is null || value.Length <= length;
    }

    /// <summary>
    /// Validates whether the specified string value has a length within the specified range (inclusive).
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="min">The minimum length.</param>
    /// <param name="max">The maximum length.</param>
    /// <returns>True if the string value has a length within the specified range (inclusive), otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Length(string? value, int min, int max)
    {
        return value is not null && value.Length >= min && value.Length <= max;
    }

    #endregion

    #region Less/Greater Than Or Equal

    /// <summary>
    /// Validates whether the specified value is less than the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is less than the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThan<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) < 0;
    }

    /// <summary>
    /// Validates whether the specified value is less than the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is less than the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThan<T>(T? value, T? other) where T : struct, IComparable<T>
    {
        return value is null && other.HasValue || (value.HasValue && other.HasValue && value.Value.CompareTo(other.Value) < 0);
    }

    /// <summary>
    /// Validates whether the specified value is less than or equal to the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is less than or equal to the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThanOrEqual<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) <= 0;
    }

    /// <summary>
    /// Validates whether the specified value is less than or equal to the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is less than or equal to the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThanOrEqual<T>(T? value, T? other) where T : struct, IComparable<T>
    {
        return value is null || (other.HasValue && value.Value.CompareTo(other.Value) <= 0);
    }

    /// <summary>
    /// Validates whether the specified value is greater than the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is greater than the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThan<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) > 0;
    }

    /// <summary>
    /// Validates whether the specified value is greater than the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is greater than the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThan<T>(T? value, T? other) where T : struct, IComparable<T>
    {
        return (other is null && value.HasValue) || (value.HasValue && other.HasValue && value.Value.CompareTo(other.Value) > 0);
    }

    /// <summary>
    /// Validates whether the specified value is greater than or equal to the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is greater than or equal to the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThanOrEqual<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) >= 0;
    }

    /// <summary>
    /// Validates whether the specified value is greater than or equal to the other value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The other value to compare with.</param>
    /// <returns>True if the value is greater than or equal to the other value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThanOrEqual<T>(T? value, T? other) where T : struct, IComparable<T>
    {
        return other is null || (value.HasValue && value.Value.CompareTo(other.Value) >= 0);
    }

    #endregion

    #region Positive Negative Zero NotZero

    /// <summary>
    /// Validates whether the specified value is positive (greater than zero).
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is positive, otherwise false.</returns>
    public static bool Positive<T>(T value) where T : INumber<T>
    {
        return value > T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is positive (greater than zero).
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is positive, otherwise false.</returns>
    public static bool Positive<T>(T? value) where T : struct, INumber<T>
    {
        return value.HasValue && value.Value > T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is negative (less than zero).
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is negative, otherwise false.</returns>
    public static bool Negative<T>(T value) where T : INumber<T>
    {
        return value < T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is negative (less than zero).
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is negative, otherwise false.</returns>
    public static bool Negative<T>(T? value) where T : struct, INumber<T>
    {
        return value.HasValue && value.Value < T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is zero.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is zero, otherwise false.</returns>
    public static bool Zero<T>(T value) where T : INumber<T>
    {
        return value == T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is zero.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is zero, otherwise false.</returns>
    public static bool Zero<T>(T? value) where T : struct, INumber<T>
    {
        return value.HasValue && value.Value == T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is not zero.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not zero, otherwise false.</returns>
    public static bool NotZero<T>(T value) where T : INumber<T>
    {
        return value != T.Zero;
    }

    /// <summary>
    /// Validates whether the specified value is not zero.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is not zero, otherwise false.</returns>
    public static bool NotZero<T>(T? value) where T : struct, INumber<T>
    {
        return value.HasValue && value.Value != T.Zero;
    }

    #endregion

    #region InPast InFuture Today AfterBefore

    /// <summary>
    /// Validates whether the specified value is in the past.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is in the past, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InPast(DateTime value)
        => value < DateTime.Now;

    /// <summary>
    /// Validates whether the specified value is in the past.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is in the past, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InPast(DateTimeOffset value)
        => value < DateTimeOffset.Now;

    /// <summary>
    /// Validates whether the specified value is in the past.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is in the past, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InPast(DateOnly value)
        => value < DateOnly.FromDateTime(DateTime.Now);

    /// <summary>
    /// Validates whether the specified value is in the future.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is in the future, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InFuture(DateTime value)
        => value > DateTime.Now;

    /// <summary>
    /// Validates whether the specified value is in the future.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is in the future, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InFuture(DateTimeOffset value)
        => value > DateTimeOffset.Now;

    /// <summary>
    /// Validates whether the specified value is in the future.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is in the future, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InFuture(DateOnly value)
        => value > DateOnly.FromDateTime(DateTime.Now);

    /// <summary>
    /// Validates whether the specified value is today.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is today, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Today(DateTime value)
        => value.Date == DateTime.Now.Date;

    /// <summary>
    /// Validates whether the specified value is today.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is today, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Today(DateTimeOffset value)
        => value.Date == DateTimeOffset.Now.Date;

    /// <summary>
    /// Validates whether the specified value is today.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the value is today, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Today(DateOnly value)
        => value == DateOnly.FromDateTime(DateTime.Now);

    /// <summary>
    /// Validates whether the specified value is after the compareTo value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare with.</param>
    /// <returns>True if the value is after the compareTo value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool After(DateTime value, DateTime compareTo)
        => value > compareTo;

    /// <summary>
    /// Validates whether the specified value is after the compareTo value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare with.</param>
    /// <returns>True if the value is after the compareTo value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool After(DateTimeOffset value, DateTimeOffset compareTo)
        => value > compareTo;

    /// <summary>
    /// Validates whether the specified value is after the compareTo value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare with.</param>
    /// <returns>True if the value is after the compareTo value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool After(DateOnly value, DateOnly compareTo)
        => value > compareTo;

    /// <summary>
    /// Validates whether the specified value is before the compareTo value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare with.</param>
    /// <returns>True if the value is before the compareTo value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Before(DateTime value, DateTime compareTo)
        => value < compareTo;

    /// <summary>
    /// Validates whether the specified value is before the compareTo value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare with.</param>
    /// <returns>True if the value is before the compareTo value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Before(DateTimeOffset value, DateTimeOffset compareTo)
        => value < compareTo;

    /// <summary>
    /// Validates whether the specified value is before the compareTo value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare with.</param>
    /// <returns>True if the value is before the compareTo value, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Before(DateOnly value, DateOnly compareTo)
        => value < compareTo;

    /// <summary>
    /// Validates whether the specified value is between the start and end values (inclusive).
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <returns>True if the value is between the start and end values (inclusive), otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(DateTime value, DateTime start, DateTime end)
        => value >= start && value <= end;

    /// <summary>
    /// Validates whether the specified value is between the start and end values (inclusive).
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <returns>True if the value is between the start and end values (inclusive), otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(DateTimeOffset value, DateTimeOffset start, DateTimeOffset end)
        => value >= start && value <= end;

    /// <summary>
    /// Validates whether the specified value is between the start and end values (inclusive).
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <returns>True if the value is between the start and end values (inclusive), otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(DateOnly value, DateOnly start, DateOnly end)
        => value >= start && value <= end;

    #endregion
}