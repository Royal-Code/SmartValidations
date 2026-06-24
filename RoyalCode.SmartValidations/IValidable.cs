using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations;

/// <summary>
/// It represents an object that can be validated.
/// <br />
/// The means of validation is to check for problems, i.e. errors.
/// </summary>
public interface IValidable
{
    /// <summary>
    /// Determines whether the object has a validation problem, i.e. is invalid.
    /// </summary>
    /// <param name="problems">
    /// When this method returns <c>true</c>, contains the detected <see cref="Problems"/>; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if there are validation problems (invalid); otherwise, <c>false</c> (valid).
    /// </returns>
    public bool HasProblems([NotNullWhen(true)] out Problems? problems);
}

/// <summary>
/// Provides extension methods for the <see cref="IValidable"/> interface.
/// </summary>
public static class ValidableExtensions
{
    /// <summary>
    /// Validates the value and returns a <see cref="Result{T}"/> containing the value if it is valid, or a <see cref="Problems"/> if it is not valid.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="Result{T}"/> containing the value if it is valid, or a <see cref="Problems"/> if it is not valid.</returns>
    public static Result<T> Validate<T>(this T value)
        where T: IValidable
    {
        if (value.HasProblems(out var problems))
            return problems;
        return value;
    }

    /// <summary>
    /// Validates the value returned by the specified <see cref="Task{T}"/> 
    /// and returns a <see cref="Result{T}"/> containing the value if it is valid,
    /// or a <see cref="Problems"/> if it is not valid.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="task">The task that produces the value to validate.</param>
    /// <returns>A <see cref="Result{T}"/> containing the value if it is valid, or a <see cref="Problems"/> if it is not valid.</returns>
    public static async Task<Result<T>> ValidateAsync<T>(this Task<T> task)
        where T: IValidable
    {
        var value = await task;
        if (value.HasProblems(out var problems))
            return problems;
        return value;
    }

    /// <summary>
    /// Validates the value returned by the specified <see cref="ValueTask{T}"/>
    /// and returns a <see cref="Result{T}"/> containing the value if it is valid,
    /// or a <see cref="Problems"/> if it is not valid.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="task">The task that produces the value to validate.</param>
    /// <returns>A <see cref="Result{T}"/> containing the value if it is valid, or a <see cref="Problems"/> if it is not valid.</returns>
    public static async Task<Result<T>> ValidateAsync<T>(this ValueTask<T> task)
        where T: IValidable
    {
        var value = await task;
        if (value.HasProblems(out var problems))
            return problems;
        return value;
    }
}