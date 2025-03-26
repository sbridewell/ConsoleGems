// <copyright file="MainMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using System.Collections.Generic;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// The main menu in the application.
    /// </summary>
    public class MainMenu(
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState,
        BuiltInCommandsMenu builtInCommandsMenu,
        CustomCommandsMenu customCommandsMenu)
        : AbstractMenu(autoCompleter, menuWriter, console, applicationState), IMenu
    {
        /// <inheritdoc/>
        public override string Title => "Main menu";

        /// <inheritdoc/>
        public override string Description => "The main menu in the application";

        /// <inheritdoc/>
        public override List<MenuItem> MenuItems =>
        [
            new () { Key = "1", Description = "Built-in commands aeroasghirghotrhpsothporthjotpwhjrtjpjorphjtphjojsdpdhrtjhpojstophjpsrjthpjrthpjsrophrt", Command = builtInCommandsMenu.ShowCommand },
            new () { Key = "2", Description = "Custom commands", Command = customCommandsMenu.ShowCommand },
        ];
    }
}
