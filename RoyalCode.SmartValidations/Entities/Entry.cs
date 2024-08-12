using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RoyalCode.SmartValidations.Entities;

/// <summary>
/// Represents an entity record obtained from the database.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public readonly struct Entry<TEntity>
{
    /// <summary>
    /// Implicit operator to create the record from the entity.
    /// </summary>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entry<TEntity>(TEntity entity) => new(entity);

    /// <summary>
    /// Implicit operator to create the record with problems due to not finding the entity.
    /// </summary>
    /// <param name="problems">Os problemas.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entry<TEntity>(Problems problems) => new(problems);

    /// <summary>
    /// Implicit operator to create the record with problems due to not finding the entity.
    /// </summary>
    /// <param name="problem">O problema.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entry<TEntity>(Problem problem) => new(problem);

    /// <summary>
    /// Implicit operator to create the record with problems due to not finding the entity.
    /// </summary>
    /// <param name="p">Parameters to create the not found message.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entry<TEntity>((string byName, string propertyName, object? objectValue) p)
        => Problem(p.byName, p.propertyName, p.objectValue);

    /// <summary>
    /// <para>
    ///     Creates a new <see cref="Entry{TEntity}"/> for when the entity is not found, 
    ///     generating a category problem <see cref="ProblemCategory.NotFound"/> and using a standard message.
    /// </para>
    /// <para>
    ///     The standard message uses the display name of the entity (<typeparamref name="TEntity"/>), 
    ///     the name of the field for which the entity was searched (<paramref name="byName"/>), 
    ///     and the value used in the search (<paramref name="propertyValue"/>).
    /// </para>
    /// </summary>
    /// <param name="byName">Name of the field by which the entity was searched.</param>
    /// <param name="propertyName">Name of the property to be added as extra data to the problem.</param>
    /// <param name="propertyValue">Value used to search for the entity.</param>
    /// <returns>
    ///     A new <see cref="Entry{TEntity}"/> with the problem generated.
    /// </returns>
    public static Entry<TEntity> Problem(string byName, string propertyName, object? propertyValue)
    {
        return Problems.NotFound(Rules.Resources.NotFoundBy<TEntity>(byName, propertyValue))
            .With("entity", typeof(TEntity).Name)
            .With(propertyName, propertyValue);
    }

    private readonly Problems? problems;

    /// <summary>
    /// Creates a new Entry for the existing entity.
    /// </summary>
    /// <param name="entity"></param>
    public Entry(TEntity entity)
    {
        Entity = entity;
    }

    /// <summary>
    /// Creates new Entry with non-existent entity problems, or other invalid status.
    /// </summary>
    /// <param name="problems"></param>
    public Entry(Problems? problems)
    {
        this.problems = problems;
    }

    /// <summary>
    /// The entity, not null when there are no problems, null if there are problems.
    /// </summary>
    public TEntity? Entity { get; }

    /// <summary>
    /// It checks whether the entity has been found and is available or not, returning problems if it is not.
    /// </summary>
    /// <param name="problems">Problems, cause of entity not available, not found.</param>
    /// <returns>True if the entity was not found, false if it exists.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [MemberNotNullWhen(false, nameof(Entity))]
    public bool NotFound([NotNullWhen(true)] out Problems? problems)
    {
        if (Entity is null)
        {
            var typeName = Rules.Resources.DisplayNames.GetDisplayName(typeof(TEntity));
            var datails = string.Format(Rules.Resources.EntityNotFound, typeName);

            problems = this.problems ?? Problems.NotFound(datails).With("entity", typeof(TEntity).Name);
            return true;
        }

        problems = null;
        return false;
    }
}

/// <summary>
/// Represents an entity record obtained from the database.
/// </summary>
/// <typeparam name="TEntity">Type of entity.</typeparam>
/// <typeparam name="TId">Type of id value.</typeparam>
public readonly struct Entry<TEntity, TId>
    where TEntity : class
{
    private readonly TId id;

    /// <summary>
    /// Creates a new Entry for the entity, existing or not, and with the id used to search for it.
    /// </summary>
    /// <param name="entity">A entity.</param>
    /// <param name="id">The entity id.</param>
    public Entry(TEntity? entity, TId id)
    {
        Entity = entity;
        this.id = id;
    }

    /// <summary>
    /// The entity, not null when there are no problems, null if there are problems.
    /// </summary>
    public TEntity? Entity { get; }

    /// <summary>
    /// It checks whether the entity has been found and is available or not, returning problems if it is not.
    /// </summary>
    /// <param name="problems">Problems, cause of entity not available, not found.</param>
    /// <returns>True if the entity was not found, false if it exists.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [MemberNotNullWhen(false, nameof(Entity))]
    public bool NotFound([NotNullWhen(true)] out Problems? problems)
    {
        if (Entity is null)
        {
            var typeName = Rules.Resources.DisplayNames.GetDisplayName(typeof(TEntity));
            var datails = string.Format(Rules.Resources.EntityNotFoundById, typeName, id);

            problems = Problems.NotFound(datails)
                .With("entity", typeof(TEntity).Name)
                .With("id", id);

            return true;
        }

        problems = null;
        return false;
    }
}