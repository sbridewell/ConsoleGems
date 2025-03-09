# ConsoleGems
Using ConsoleGems you can build console applications with a rich user experience which are easily unit testable.
<!--TOC-->
  - [IConsole](#iconsole)
    - [Happy with monocrome?](#happy-with-monocrome)
    - [Want some colour?](#want-some-colour)
    - [Don't like my colour scheme?](#dont-like-my-colour-scheme)
  - [Auto-complete](#auto-complete)
    - [Limitations](#limitations)
    - [Credits](#credits)
  - [Menus and commands](#menus-and-commands)
    - [Commands](#commands)
    - [Menus](#menus)
  - [Prompters](#prompters)
  - [Putting it all together](#putting-it-all-together)
  - [Frequently asked questions](#frequently-asked-questions)
  - [Getting in touch](#getting-in-touch)
<!--/TOC-->

## IConsole
Console applications can be tricky to unit test.

The `System.Console` class is `static`, so if your unit test runner runs multiple tests in parallel, any tests which involve calling the members of `System.Console` can conflict with each other, with one test overwriting a property's value which a second test has just set in its "arrange" or "act" step, but before the second test's "assert" step tests the property's value. Yes, you can use locking to prevent conflicting tests from running at the same time, but it's easy to forget to do this, and then spend ages wondering why one of your tests is intermittently failing when you haven't changed it or the code it's testing.

And there's another problem with `System.Console`...

Some of the properties and methods of `System.Console` just throw an error if called when there's no actual console window open, such as when being called from a unit test. The `IConsole` interface provides a way to abstract the console, so you can mock it in your unit tests.

### Happy with monocrome?
Then the `Console` class is for you. It implements the `IConsole` interface, and provides a simple console implementation which uses the `System.Console` class.

```csharp
var console = new Console();
console.Write("Type something: ");
var userInput = console.ReadLine();
console.WriteLine($"You typed: {userInput}");
```

### Want some colour?
Then the `ColourfulConsole` class is for you. It implements the `IConsole` interface, and provides a console implementation which uses the `System.Console` class, but with the ability to write text in different colours, depending on whether the text is a prompt for user input, text typed by the user, an error message, and so on. The background and foreground colour of the text is controlled by the optional `ConsoleOutputType` parameter of the `Write` and `WriteLine` methods.

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
To add auto-complete functionality to your application, you need 3 things
- A `List<string>` of suggestions for the auto-completer to provide to the user
- An `IAutoCompleter` implementation which provides the auto-complete functionality. ConsoleGems provides the `AutoCompleter` implementation.
- An `IAutoCompleteKeyPressMappings` implementation which intercepts and handles specific keypresses. ConsoleGems provides the `AutoCompleteKeyPressDefaultMappings` implementation which handles the left arrow, right arrow, home, end, delete, backspace tab and ctrl-V keypresses. You can provide your own `IAutoCompleteKeyPressMappings` implementation if you want completely different keypress mappings, or you can copy the default and add or remove individual `IAutoCompleteKeyPressHandler` implementations to/from the `IAutoCompleteKeyPressMappings`'s `Mappings` property.

```csharp
var keyPressMappings = new AutoCompleteKeyPressDefaultMappings();
var autoCompleter = new AutoCompleter(keyPressMappings, console);
var suggestions = new List<string> { "apple", "banana", "cherry" };
var userInput = autoCompleter.ReadLine(suggestions, "Pick a fruit: ");
console.WriteLine($"You picked: {userInput}");
```

At the prompt, press the tab key to bring up the first suggestion. Press the tab key again to cycle forwards through the suggestions, or shift-tab to cycle backwards through the suggestions.

### Limitations
If the user input is so long that it fills the console window, the beginning of the user input will scroll off the top of the window. Whilst this text will still be included in the string returned by the `ReadLine` method, it is no longer editable after scrolling off the top of the window. I've handled this scenario as gracefully as I can, but if you can suggest a better way then I'm all ears. For now, a workaround is to increase the size of the console window.

### Credits
The auto-complete functionality in ConsoleGems is originally based on [this answer to the Stack Overflow question "User input file path with tab auto complete"](https://stackoverflow.com/a/39030981/16563198), with some inspiration from the [ReadLine](https://www.nuget.org/packages/ReadLine) library on how to handle the scenario where the user input is long enough to wrap onto a new line.

## Menus and commands
Each menu has a list of `MenuItem` objects, each of which has a `Command` property which is executed when the user selects the menu item.

### Commands
Commands implement the `ICommand` interface (not the one in the System.Windows.Input namespace), which has a single `Execute` method. Commands do not accept any parameters and do not return a value (I might change this later though), so if a command requires user input or needs to display a result to the user, it should do so via the `IConsole` methods.

```csharp
public class GreeterCommand(IConsole console) : ICommand
{
    public void Execute()
    {
        console.Write("What's your name? ", ConsoleOutputType.Prompt);
        var name = console.ReadLine();
        console.WriteLine($"Hello, {name}!", ConsoleOutputType.Default);
    }
}
```

There are also some built-in commands in the `Sde.ConsoleGems.Commands` namespace which you might find useful.

### Menus
Each menu should implement the `IMenu` interface, and ConsoleGems provides the `AbstractMenu` base class to assist with this. `AbstractMenu` has a `ShowCommand` property, which is a `ICommand` which will display that menu to the user, making it easy to create a heirarchy of menus and submenus. The commands and submenus used by a menu should be passed to the menu's constructor.

```csharp
public class MainMenu(
    IAutoCompleter autoCompleter,
    IMenuWriter menuWriter,
    IConsole console,
    ApplicationState applicationState,
    GreeterCommand greeterCommand,
    SubMenu submenu,
    ExitCommand exitCommand)
    : AbstractMenu(autoCompleter, menuWriter, console, applicationState)
{
    public override string Title => "Main menu";
    public override string Description => "This is the main menu";
    public override List<MenuItem> MenuItems =>
    [
        new () { Key = "greet", Description = "Display a greeting", Command = greeterCommand },
        new () { Key = "submenu", Description = "Go to the submenu", Command = subMenu.ShowCommand },
        new () { Key = "exit", Description = "Exit the program", Command = exitCommand },
    ];
}

public class SubMenu(
    IAutoCompleter autoCompleter,
    IMenuWriter menuWriter,
    IConsole console,
    ApplicationState applicationState,
    ExitCommand exitCommand)
    : AbstractMenu(autoCompleter, menuWriter, console, applicationState)
{
    public override string Title => "Sub menu";
    public override string Description => "This is a child menu";
    public override List<MenuItem>MenuItems = new List<MenuItem>
    [
        new () { Key = "dir", Description = "List files in the current directory", Command = dirCommand },
    ];
}
```

Any menu which isn't at the top of the menu heirarchy automatically gets a "back" menu item added to it, which will return the user to the previous menu.

To specify commands which should appear on every menu in the application, implement `ISharedMenuItemsProvider`.

## Prompters
The `Sde.ConsoleGems.Prompters` namespace contains classes which prompt the user for input where the required return value is of a type other than `string`. For example, the `FilePrompter` and `DirectoryPrompter` classes build on `IAutoCompleter` to assist in selecting a file or directory from the current working directory. Prompters implement the `IPrompter` interface, although this interface declares no members, it's just a marker interface which adds a bit of syntactic sugar for registering prompters with dependency injection.

## Putting it all together
Whilst it's possible to instantiate all these console, menu, command, etc classes at the point where they're used, it's much easier to use dependency injection. ConsoleGems provides extension methods for `IServiceCollection` and a `ConsoleGemsOptions` class which simplifies the process of registering the necessary dependencies using a fluent syntax. In particular, the `UseMainMenu` method of `ConsoleGemsOptions` allows you to specify the menu which is at the top of the menu heirarchy, and then have all the other menus and any commands used in those menus auto-discovered and registered with dependency injection. Bewware though - other dependencies used by those commands are not auto-discovered.

Take a look at the [Demos](Demos) folder for some examples of how to use ConsoleGems with dependency injection, as well as working examples of using menus, commands, colourful console output amd auto-complete.

## Frequently asked questions

| Question | Answer |
| ------------------------ | ------------------------------- |
| What version of .net do I need to target in order to use ConsoleGems? | 8.0 or later |
| Can I use ConsoleGems on a platform other than Windows? | In theory, yes, but I've only tested it on Windows and I've been relying on code analysers to tell me if anything is platform-specific. If you try it on another platform and it doesn't work, please let me know by creating an issue. |
| Is there a stable version of ConsoleGems? | No, this is a very new project, please consider it to be a pre-release version for now. As I add features and refactor I don't guarantee to maintain backwardws compatibility, so I don't recommend using it for anything which would cause big problems if I break backwards compatibility. |

## Getting in touch
- Found a bug or something in this README which isn't consistent with the code?
- Got a question?
- Want to suggest a feature or enhancement?
- Want to contribute?

Scroll to the top of the page and create a new issue to start a conversation. ConsoleGems is developed and maintained in my spare time, so I may not be super quick in getting back to you, but I promise to engage in good faith with all reasonable requests.
