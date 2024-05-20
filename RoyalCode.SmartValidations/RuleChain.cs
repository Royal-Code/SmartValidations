﻿using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;
using RoyalCode.SmartProblems;

// ReSharper disable ParameterHidesMember

namespace RoyalCode.SmartValidations;

/// <summary>
/// <para>
///     A rule that applies some validation to the model and, if the validation fails, returns a problem.
/// </para>
/// <para>
///     The return of the rule is a boolean that indicates if the validation was successful or not.
/// </para>
/// </summary>
/// <typeparam name="TModel">The instance to be validated.</typeparam>
public delegate bool Rule<TModel>(ValidationContext<TModel> context, [NotNullWhen(false)] out Problem? problem);

public readonly struct ValidationContext<TModel>(TModel model, RuleResources resources)
{
    public TModel Model { get; } = model;

    public RuleResources Resources { get; } = resources;
}

public class RuleResources
{
    private Dictionary<string, string> data = new();
    private Func<string>? messageFormatter;

    /// <summary>
    /// <para>
    ///     A code that identifies the problem generated by the rule (normally used for the default rules).
    /// </para>
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The template of the message that will be used to generate the details of the problem.
    /// </summary>
    public string? Template { get; set; }

    public void AddData(string name, string value)
    {
        data[name] = value;
    }

    public string GetMessage()
    {
        messageFormatter ??= CreateMessageFormatter();
        return messageFormatter();


        if (Template is not null)
        {
        }

        return "The model is null.";
    }

    private Func<string> CreateMessageFormatter()
    {
        if (Template is not null)
        {
            StringBuilder formattableTemplate = new StringBuilder(Template);
            List<Func<object?>>? argumentsProviders = null;

            var regex = new Regex(@"\{(.+?)\}");
            var matches = regex.Matches(Template);

            foreach (Match match in matches)
            {
                var key = match.Groups[1].Value;
                argumentsProviders ??= [];
                var index = argumentsProviders.Count;
                argumentsProviders.Add(() => data.TryGetValue(key, out var value) ? value : string.Empty);
                formattableTemplate.Replace($"{{{key}}}", $"{{{index}}}");
            }

            return () => string.Format(formattableTemplate.ToString(), argumentsProviders.Select(p => p()).ToArray());
        }

        return () => "The model is null.";
    }
}

public class RuleSetOptions
{
    public bool BreakOnFirstError { get; set; }
}

public interface IRuleSet<in TModel>
{
    bool HasProblems(TModel model, [NotNullWhen(true)] out Problems? problems);
}

public interface IRuleChainBuilder<TModel>
{
    RuleChain<TModel> Must(Rule<TModel> rule);
}

public class RuleChain<TModel> : IRuleSet<TModel>, IRuleChainBuilder<TModel>
{
    private readonly RuleChain<TModel>? previous;
    private readonly Rule<TModel> rule;
    private readonly RuleSetOptions options;
    private readonly RuleResources resources;

    public RuleChain(Rule<TModel> rule)
    {
        this.rule = rule;
        options = new RuleSetOptions();
        resources = new RuleResources();
    }

    private RuleChain(RuleChain<TModel> previous, Rule<TModel> rule, RuleSetOptions options) : this(rule)
    {
        this.previous = previous;
        this.options = options;
    }

    /// <summary>
    /// <para>
    ///     Adds a new rule to the rule set, returning a new instance of <see cref="RuleChain{TModel}"/>
    ///     for configure the new rule and chaining more rules.
    /// </para>
    /// </summary>
    /// <param name="rule">The new rule to be added.</param>
    /// <returns>
    ///     A new instance of <see cref="RuleChain{TModel}"/> with the new rule added.
    /// </returns>
    public RuleChain<TModel> Must(Rule<TModel> rule)
    {
        return new RuleChain<TModel>(this, rule, options);
    }

    public bool HasProblems(TModel model, [NotNullWhen(true)] out Problems? problems)
    {
        problems = null;
        if (previous is not null
            && previous.HasProblems(model, out problems)
            && options.BreakOnFirstError)
        {
            return true;
        }

        var context = new ValidationContext<TModel>(model, resources);

        if (rule(context, out var problem))
            return problems is not null;

        problems ??= [];
        problems.Add(problem);
        return true;
    }
}

public class DefaultRulesResources
{
}

public static class Rules
{
    public static RuleFor<TModel> For<TModel>()
    {
        return new RuleFor<TModel>();
    }

    public static RuleChain<TModel> NotNull<TModel>(this IRuleChainBuilder<TModel> builder)
    {
        return builder.Must(DefaultRules.ModelNotNull);
    }

    public sealed class RuleFor<TModel> : IRuleChainBuilder<TModel>
    {
        public RuleChain<TModel> Must(Rule<TModel> rule)
        {
            return new RuleChain<TModel>(rule);
        }
    }
}

internal static class DefaultRules
{
    internal static bool ModelNotNull<TModel>(
        ValidationContext<TModel> context,
        [NotNullWhen(false)] out Problem? problem)
    {
        if (context.Model is not null)
        {
            problem = null;
            return true;
        }

        var message = context.Resources.GetMessage();

        problem = Problems.InvalidParameter(message);
        return false;
    }
}