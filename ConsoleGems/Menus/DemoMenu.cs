// <copyright file="DemoMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    using Sde.ConsoleGems.Commands.Demo;

    /// <summary>
    /// A menu for demonstrating menus.
    /// </summary>
    public class DemoMenu(
        GetADrinkCommand getADrinkCommand,
        SelectAFileCommand selectAFileCommand,
        ThrowExceptionCommand throwExceptionCommand,
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState)
        : AbstractMenu(
            autoCompleter,
            menuWriter,
            console,
            applicationState)
    {
        /// <inheritdoc/>
        public override string Title => "Demo menu";

        /// <inheritdoc/>
        public override string Description => "This menu is to demonstrate how to implement a menu";

        /// <inheritdoc/>
        public override List<MenuItem> MenuItems =>
        [
            new MenuItem { Key = "drink", Description = "Demo: Get a drink", Command = getADrinkCommand, },
            new MenuItem { Key = "file", Description = "Demo: Select a file", Command = selectAFileCommand, },
            new MenuItem { Key = "throw", Description = "Demo: throw an error", Command = throwExceptionCommand, },
        ];
    }
}
