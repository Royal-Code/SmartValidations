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
    private static readonly DateOnly DateOnlyUnixEpoch = new(1970, 1, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmail([NotNullWhen(true)] string? value) 
        => value is not null && EmailAddressValidator.IsValid(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUrl([NotNullWhen(true)] string? value) 
        => value is not null && UrlValidator.IsValid(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty<T>([NotNullWhen(true)] T value) where T: INumber<T> 
        => value != T.Zero;
    
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
    public static bool NotEmpty(DateTime value) => value != DateTime.MinValue && value != DateTime.UnixEpoch;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateTime? value)
        => value.HasValue && value.Value != DateTime.MinValue && value.Value != DateTime.UnixEpoch;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateTimeOffset value) 
        => value != DateTimeOffset.MinValue && value != DateTimeOffset.UnixEpoch;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateTimeOffset? value)
        => value.HasValue && value.Value != DateTimeOffset.MinValue && value.Value != DateTimeOffset.UnixEpoch;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(DateOnly value) => value != DateOnly.MinValue && value != DateOnlyUnixEpoch;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] DateOnly? value)
        => value.HasValue && value.Value != DateOnly.MinValue && value.Value != DateOnlyUnixEpoch;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty(Guid value) 
        => value != Guid.Empty;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] Guid? value) 
        => value.HasValue && value.Value != Guid.Empty;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotEmpty([NotNullWhen(true)] string? value) 
        => !string.IsNullOrWhiteSpace(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BothNullOrNotEmpty(string? value1, string? value2)
    {
        return (value1 is null && value2 is null)
               || (!string.IsNullOrWhiteSpace(value1) && !string.IsNullOrWhiteSpace(value2));
    }
}