# ConsoleGems
Using ConsoleGems you can build console applications with a rich user experience which are easily unit testable.
<!--TOC-->
  - [IConsole](#iconsole)
    - [Happy with monocrome?](#happy-with-monocrome)
    - [Want some colour?](#want-some-colour)
    - [Don't like my colour scheme?](#dont-like-my-colour-scheme)
  - [Auto-complete](#auto-complete)
  - [Menus and commands](#menus-and-commands)
<!--/TOC-->
## IConsole
Console applications can be tricky to unit test.

The `System.Console` class is `static`, so if your unit test runner runs multiple tests in parallel, any tests which involve calling the members of `System.Console` can conflict with  each other, with one test overwriting a property's value which a second test has just set in its "arrange" or "act" set, but before the second test's "assert" step tests the property's value. Yes, you can use locking to prevent conflicting tests from running at the same time, but it's easy to forget to do this, and then spend ages wondering why one of your tests is intermittently failing when you haven't changed it or the code it's testing. And there's another problem with `System.Console`...

Some of the properties and methods of `System.Console` just throw an error if called when there's no actual console window open, such as when being called from a unit test. The `IConsole` interface provides a way to abstract the console, so you can mock it in your unit tests.

### Happy with monocrome?
Then the [Console](ConsoleGems/Consoles/Concrete/Console.cs) class is for you. It implements the `IConsole` interface, and provides a simple console implementation which uses the `System.Console` class.

```csharp
var console = new Console();
console.Write("Type something: ");
var userInput = console.ReadLine();
console.WriteLine($"You typed: {userInput}");
```

### Want some colour?
Then the [ColourfulConsole](ConsoleGems/Consoles/Concrete/ColourfulConsole.cs) class is for you. It implements the `IConsole` interface, and provides a console implementation which uses the `System.Console` class, but with the ability to write text in different colours, depending on whether the text is a prompt for user input, text typed by the user, an error message, and so on. The background and foreground colour of the text is controlled by the optional `ConsoleOutputType` parameter of the `Write` and `WriteLine` methods.

```csharp
var colourManager = new ConsoleColourManager();
var console = new ColourfulConsole(colourManager);
console.WriteLine("Type something", ConsoleOutputType.Prompt);
var userInput = console.ReadLine();
if (string.IsNullOrWhiteSpace(userInput))
{
    console.WriteLine("You didn't type anything!", ConsoleOutputType.Error);
}
else
{
    console.WriteLine($"You typed: {userInput}", ConsoleOutputType);
})
```

### Don't like my colour scheme?
The `ConsoleColours` class has a static property matching each of the values of the `ConsoleOutputType` enum, so you can change the colours used for each type of output.

```csharp
ConsoleColours.Error = new ConsoleColours(ConsoleColor.White, ConsoleColor.Red);
```

## Auto-complete
```csharp
var keyPressMappings = new AutoCompleteKeyPressDefaultMappings();
var autoCompleter = new AutoCompleter(keyPressMappings, console);
var suggestions = new List<string> { "apple", "banana", "cherry" };
var userInput = autoCompleter.ReadLine(suggestions, "Pick a fruit: ");
console.WriteLine($"You picked: {userInput}");
```

## Menus and commands

## Demo projects
Take a look at the [Demos](Demos) folder for some examples of how to use ConsoleGems with dependency injection, including using the `ConsoleGemsOptions` class to simplify the process of registering the necessary dependencies.

## Getting in touch
- Found a bug or something in this README which isn't consistent with the code?
- Got a question?
- Want to suggest a feature or enhancement?
- Want to contribute?

Scroll to the top of the page and create a new issue. ConsoleGems is developed and maintained in my spare time, so I may not be super quick in getting back to you, but I promise to engage in good faith with all reasonable requests.
```