// <copyright file="CustomCommandsMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using System.Collections.Generic;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// Menu containin commands which are defined in this application.
    /// </summary>
    public class CustomCommandsMenu(
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState,
        RollDiceCommand rollDiceCommand,
        GetADrinkCommand getADrinkCommand,
        ThrowExceptionCommand throwExceptionCommand)
        : AbstractMenu(
            autoCompleter,
            menuWriter,
            console,
            applicationState)
    {
        /// <inheritdoc/>
        public override string Title => "Custom commands";

        /// <inheritdoc/>
        public override string Description => "Commands which are defined in this application.";

        /// <inheritdoc/>
        public override List<MenuItem> MenuItems =>
        [
            new () { Key = "dice", Description = "Simulate rolling dice", Command = rollDiceCommand },
            new () { Key = "drink", Description = "Get a drink", Command = getADrinkCommand },
            new () { Key = "throw", Description = "Throw an exception", Command = throwExceptionCommand },
        ];
    }
}
