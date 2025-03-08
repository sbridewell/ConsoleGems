// <copyright file="AutoCompleteDemo.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoCompleteDemo
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Prompters;

    /// <summary>
    /// Demonstrates the use of an <see cref="IAutoCompleter"/> to select a drink.
    /// </summary>
    public class AutoCompleteDemo(
        IAutoCompleter autoCompleter,
        IFilePrompter filePrompter,
        IDirectoryPrompter directoryPrompter,
        IConsole console)
    {
        /// <summary>
        /// Allows the user to choose a drink from a list of suggestions.
        /// </summary>
        public void ChooseADrink()
        {
            console.WriteLine("Type some text and press the Tab key to see suggestions.");
            console.WriteLine("Tab again to cycle forward through suggestions.");
            console.WriteLine("Shift-Tab to cycle backwards through suggestions.");
            console.WriteLine("You can use home, end, left and right arrow keys to move the cursor left and right.");
            console.WriteLine("Shift-F1 and shift-F2 do the same as the arrow keys.");
            console.WriteLine("Delete and backspace keys delete a character.");
            console.WriteLine("Ctrl-V pastes text from the clipboard.");
            console.WriteLine("Press Enter to submit your answer.");
            var suggestions = new List<string> { "tea", "coffee", "water", "beer", "wine", "whisky" };
            var drink = autoCompleter.ReadLine(suggestions, "What do you want to drink? ");
            console.WriteLine($"You chose {drink}.");
            var currentDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var file = filePrompter.Prompt(currentDirectory, "Enter a filename: ", mustAlreadyExist: true);
            console.WriteLine($"You chose {file}.");
            var directory = directoryPrompter.Prompt(currentDirectory, "Enter a directory name: ", mustAlreadyExist: true);
            console.WriteLine($"You chose {directory}.");
        }
    }
}
