using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartValidations;

public class DisplayNames
{
    private readonly ConcurrentDictionary<Type, string> typeNames = new();
    private readonly ConcurrentDictionary<(Type, string), string> propertyNames = new();
    private readonly Func<Type, string> createTypeDisplayName;
    private readonly Func<(Type, string), string> createPropertyDisplayName;

    public DisplayNames()
    {
        createTypeDisplayName = CreateTypeDisplayName;
        createPropertyDisplayName = CreatePropertyDisplayName;
    }

    /// <summary>
    /// Obtém o display name de um determinado tipo.
    /// </summary>
    /// <param name="type">O tipo.</param>
    /// <returns>O DisplayName, se disponível, ou o nome do tipo. Quando nulo, retorna "?".</returns>
    public string GetDisplayName(Type? type)
    {
        if (type is null)
            return "?";

        return typeNames.GetOrAdd(type, createTypeDisplayName);
    }

    /// <summary>
    /// Obtém o display name de uma determinada propriedade de um determinado tipo.
    /// </summary>
    /// <param name="type">O tipo que deve conter a propriedade.</param>
    /// <param name="property">O nome da propriedade.</param>
    /// <returns>
    ///     O DisplayName, se disponível, ou o nome da propriedade.
    ///     Quando o tipo for nulo, será retornado o nome da propriedade, 
    ///     e se a propriedade for nula, retorna "?".
    /// </returns>
    public string GetDisplayName(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]Type? type,
        string? property)
    {
        if (type is null || property is null)
            return property ?? "?";

        return propertyNames.GetOrAdd((type, property), createPropertyDisplayName);
    }

    private string CreateTypeDisplayName(Type type)
    {
        var attr = type.GetCustomAttribute<DisplayNameAttribute>();
        if (attr is not null)
            return attr.DisplayName;

        return type.Name;
    }

    private string CreatePropertyDisplayName((Type type, string property) key)
    {
        var propertyInfo = key.type.GetRuntimeProperty(key.property);
        if (propertyInfo == null)
            return key.property;

        var attr = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
        if (attr is not null)
            return attr.DisplayName;

        return key.property;
    }
}