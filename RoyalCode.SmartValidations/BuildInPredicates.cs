using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RoyalCode.SmartValidations;

#pragma warning disable S1121 // Assignments should not be made from within sub-expressions

public static class BuildInPredicates
{
    private static readonly EmailAddressAttribute EmailAddressValidator = new();
    private static readonly UrlAttribute UrlValidator = new();

    #region Mist
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmail([NotNullWhen(true)] string? value) 
        => value is not null && EmailAddressValidator.IsValid(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUrl([NotNullWhen(true)] string? value) 
        => value is not null && UrlValidator.IsValid(value);
    
    #endregion

    #region NotEmpty

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T value) where T: INumber<T> 
        => value != T.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T? value) where T : struct, INumber<T>
        => value.HasValue && value.Value != T.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T[]? value) 
        => value is not null && value.Length != 0;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] ICollection<T>? value) 
        => value is not null && value.Count != 0;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] IEnumerable<T>? value) 
        => value is not null && value.Any();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateTime value) => value != DateTime.MinValue;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateTime? value)
        => value.HasValue && value.Value != DateTime.MinValue;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateTimeOffset value) 
        => value != DateTimeOffset.MinValue;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateTimeOffset? value)
        => value.HasValue && value.Value != DateTimeOffset.MinValue;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateOnly value) => value != DateOnly.MinValue;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateOnly? value)
        => value.HasValue && value.Value != DateOnly.MinValue;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(Guid value) 
        => value != Guid.Empty;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] Guid? value) 
        => value.HasValue && value.Value != Guid.Empty;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] string? value) 
        => !string.IsNullOrWhiteSpace(value);
    
    #endregion

    #region NotNull Or Empty

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NullOrNotEmpty<T>(T value) where T : INumber<T>
        => value is null || value != T.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NullOrNotEmpty<T>(T? value) where T : struct, INumber<T>
        => !value.HasValue || value.Value != T.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NullOrNotEmpty(string? value)
        => value is null || !string.IsNullOrWhiteSpace(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNullOrNotEmpty(string? value1, string? value2)
    {
        return (value1 is null && value2 is null)
               || (!string.IsNullOrWhiteSpace(value1) && !string.IsNullOrWhiteSpace(value2));
    }

    #endregion

    #region Equals NotEquals

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equal(string? value,
        string expected,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return expected.Equals(value, comparison);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equal<T>(T? value, T expected)
        where T: IEquatable<T>
    {
        return expected.Equals(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEqual(string? value,
        string expected,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return !expected.Equals(value, comparison);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEqual<T>(T? value, T expected)
        where T: IEquatable<T>
    {
        return !expected.Equals(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothEqual(
        string? value1,
        string? value2,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return string.Equals(value1, value2, comparison);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothEqual<T>(T? value1, T? value2)
        where T: IEquatable<T>
    {
        return value1 is null 
            ? value2 is null 
            : value1.Equals(value2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNotEqual(
        string? value1,
        string? value2,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return !string.Equals(value1, value2, comparison);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNotEqual<T>(T? value1, T? value2)
        where T: IEquatable<T>
    {
        return !(value1 is null 
            ? value2 is null 
            : value1.Equals(value2));
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