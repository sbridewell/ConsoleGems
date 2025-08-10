# Coding standards and style

- Use the C# coding conventions as defined in the [Microsoft documentation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).
- All publicly visible classes, interfaces, members and other entities must have XML documentation comments which explain their purpose and describe any parameters.
- Names of classes, interfaces, members and other entities must use British English spelling, for example
  - Colour, not color
  - Grey, not gray
  - Serialise, not serialize

# Testing

- Use XUnit framework, and use FluentAssertions for assertions in unit tests.
- Unit tests must follow the Arrange, Act, Assert pattern.
- Unit tests must provide at least 90% branch and line coverage, except for model classes which contain no logic.
- Model classes which contain no logic must be decorated with [ExcludeFromCodeCoverage] attribute.
- Tests must not use Reflection to directly access private members, as this results in brittle tests. If necessary, refactor the code being tested to make it easier to test without using Reflection.