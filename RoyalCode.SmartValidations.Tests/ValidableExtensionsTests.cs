using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations.Tests;

public class ValidableExtensionsTests
{
    [Fact]
    public void Validate_WithValidValue_ReturnsResultWithValue()
    {
        // Arrange
        var value = new TestValidable(id: 1, name: "John", age: 30);

        // Act
        var result = value.Validate();

        // Assert
        Assert.False(result.HasProblems(out _));
        Assert.True(result.HasValue(out var resultValue));
        Assert.Equal(value, resultValue);
    }

    [Fact]
    public void Validate_WithInvalidValue_ReturnsResultWithProblems()
    {
        // Arrange
        var value = new TestValidable(id: 0, name: "", age: 10);

        // Act
        var result = value.Validate();

        // Assert
        Assert.True(result.HasProblems(out var problems));
        Assert.NotNull(problems);
        Assert.Equal(3, problems.Count);
    }

    [Fact]
    public void Validate_WithInvalidValue_ProblemsContainCorrectProperties()
    {
        // Arrange
        var value = new TestValidable(id: 0, name: "", age: 10);

        // Act
        var result = value.Validate();

        // Assert
        Assert.True(result.HasProblems(out var problems));
        var problemList = problems.ToList();
        Assert.Contains(problemList, p => p.Detail.Contains("Id"));
        Assert.Contains(problemList, p => p.Detail.Contains("Name"));
        Assert.Contains(problemList, p => p.Detail.Contains("Age"));
    }

    [Fact]
    public async Task ValidateAsync_WithTask_AndValidValue_ReturnsResultWithValue()
    {
        // Arrange
        var value = new TestValidable(id: 1, name: "Jane", age: 25);
        var task = Task.FromResult(value);

        // Act
        var result = await task.ValidateAsync();

        // Assert
        Assert.False(result.HasProblems(out _));
        Assert.True(result.HasValue(out var resultValue));
        Assert.Equal(value, resultValue);
    }

    [Fact]
    public async Task ValidateAsync_WithTask_AndInvalidValue_ReturnsResultWithProblems()
    {
        // Arrange
        var value = new TestValidable(id: -1, name: null, age: 5);
        var task = Task.FromResult(value);

        // Act
        var result = await task.ValidateAsync();

        // Assert
        Assert.True(result.HasProblems(out var problems));
        Assert.NotNull(problems);
        Assert.Equal(3, problems.Count);
    }

    [Fact]
    public async Task ValidateAsync_WithTask_CompletesBefore_ResultIsGenerated()
    {
        // Arrange
        var value = new TestValidable(id: 1, name: "Test", age: 20);
        var task = Task.Delay(10).ContinueWith(_ => value);

        // Act
        var result = await task.ValidateAsync();

        // Assert
        Assert.False(result.HasProblems(out _));
        Assert.True(result.HasValue(out var resultValue));
        Assert.Equal(value, resultValue);
    }

    [Fact]
    public async Task ValidateAsync_WithValueTask_AndValidValue_ReturnsResultWithValue()
    {
        // Arrange
        var value = new TestValidable(id: 1, name: "Bob", age: 30);
        var valueTask = new ValueTask<TestValidable>(value);

        // Act
        var result = await valueTask.ValidateAsync();

        // Assert
        Assert.False(result.HasProblems(out _));
        Assert.True(result.HasValue(out var resultValue));
        Assert.Equal(value, resultValue);
    }

    [Fact]
    public async Task ValidateAsync_WithValueTask_AndInvalidValue_ReturnsResultWithProblems()
    {
        // Arrange
        var value = new TestValidable(id: 0, name: "   ", age: 0);
        var valueTask = new ValueTask<TestValidable>(value);

        // Act
        var result = await valueTask.ValidateAsync();

        // Assert
        Assert.True(result.HasProblems(out var problems));
        Assert.NotNull(problems);
        Assert.Equal(3, problems.Count);
    }

    [Fact]
    public async Task ValidateAsync_WithValueTask_FromAsyncMethod_ReturnsResultWithValue()
    {
        // Arrange
        async ValueTask<TestValidable> GetValidValue()
        {
            await Task.Delay(5);
            return new TestValidable(id: 2, name: "Async", age: 40);
        }

        // Act
        var result = await GetValidValue().ValidateAsync();

        // Assert
        Assert.False(result.HasProblems(out _));
        Assert.True(result.HasValue(out var resultValue));
        Assert.Equal(2, resultValue.Id);
        Assert.Equal("Async", resultValue.Name);
    }

    [Fact]
    public async Task ValidateAsync_WithValueTask_FromAsyncMethod_ReturnsResultWithProblems()
    {
        // Arrange
        async ValueTask<TestValidable> GetInvalidValue()
        {
            await Task.Delay(5);
            return new TestValidable(id: -5, name: "", age: 100);
        }

        // Act
        var result = await GetInvalidValue().ValidateAsync();

        // Assert
        Assert.True(result.HasProblems(out var problems));
        Assert.NotNull(problems);
        // Only Id and Name have problems (Age >= 18 is valid for age 100)
        Assert.Equal(2, problems.Count);
    }

    /// <summary>
    /// Test struct that implements IValidable for testing ValidableExtensions methods.
    /// </summary>
    private readonly struct TestValidable : IValidable, IEquatable<TestValidable>
    {
        public int Id { get; }
        public string? Name { get; }
        public int Age { get; }

        public TestValidable(int id, string? name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public bool HasProblems([NotNullWhen(true)] out Problems? problems)
        {
            Problems result = [];

            if (Id <= 0)
                result.Add(Problems.InvalidParameter("invalid Id", "Id"));

            if (string.IsNullOrWhiteSpace(Name))
                result.Add(Problems.InvalidParameter("invalid Name", "Name"));

            if (Age < 18)
                result.Add(Problems.InvalidParameter("invalid Age", "Age"));

            if (result.Count == 0)
            {
                problems = null!;
                return false;
            }

            problems = result;
            return true;
        }

        public override bool Equals(object? obj)
        {
            return obj is TestValidable validable && Equals(validable);
        }

        public bool Equals(TestValidable other)
        {
            return Id == other.Id && Name == other.Name && Age == other.Age;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Age);
        }
    }
}
