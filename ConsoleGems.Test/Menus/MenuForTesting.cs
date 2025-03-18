// <copyright file="MenuForTesting.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Menus
{
    /// <summary>
    /// A menu which is implemented for the purposes of unit testing.
    /// </summary>
    public class MenuForTesting(
        IAutoCompleter autoCompleter,
        IMenuWriter consoleMenuWriter,
        IConsole console,
        ICommand command1,
        ICommand exitCurrentMenuCommand,
        ApplicationState applicationState)
        : AbstractMenu(autoCompleter, consoleMenuWriter, console, applicationState)
    {
        /// <inheritdoc/>
        public override string Title => "Menu for testing";

        /// <inheritdoc/>
        public override string Description => "A menu which is implemented for the purpose of a unit test.";

        /// <inheritdoc/>
        public override List<MenuItem> MenuItems =>
        [
            new () { Key = "mock1", Description = "Mock command 1", Command = command1 },
            new () { Key = "back", Description = "Return to previous menu", Command = exitCurrentMenuCommand },
        ];
    }
}
