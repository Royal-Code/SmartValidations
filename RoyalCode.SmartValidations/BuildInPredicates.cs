using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Min<T>(T value, T min) where T: IComparable<T>
    {
        return value.CompareTo(min) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Min<T>(T? value, T min) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(min) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Max<T>(T value, T max) where T: IComparable<T>
    {
        return value.CompareTo(max) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Max<T>(T? value, T max) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(max) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MinMax<T>(T value, T min, T max) where T: IComparable<T>
    {
        return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MinMax<T>(T? value, T min, T max) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(min) >= 0 && value.Value.CompareTo(max) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MinLength(string? value, int length)
    {
        return value?.Length >= length;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MaxLength(string? value, int length)
    {
        return value?.Length <= length;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Length(string? value, int min, int max)
    {
        return value?.Length >= min && value.Length <= max;
    }
    
    #endregion
    
    #region Less/Greater Than Or Equal
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThan<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) < 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThanOrEqual<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) <= 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThan<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) > 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThanOrEqual<T>(T value, T other) where T: IComparable<T>
    {
        return value.CompareTo(other) >= 0;
    }
    
    #endregion
}