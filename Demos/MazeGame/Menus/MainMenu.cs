// <copyright file="MainMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Menus
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;
    using Sde.MazeGame.Commands;

    /// <summary>
    /// The main menu in the games demo.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MainMenu(
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState,
        LaunchMazeGameCommand launchMazeGameCommand)
        : AbstractMenu(autoCompleter, menuWriter, console, applicationState)
    {
        /// <inheritdoc/>
        public override string Title => "Main menu - games demo";

        /// <inheritdoc/>
        public override string Description => "A selection of classic games implemented as console applications.";

        /// <inheritdoc/>
        public override List<MenuItem> MenuItems => new ()
        {
            new ()
            {
                Key = "m",
                Description = "A simple maze game.",
                Command = launchMazeGameCommand,
            },
        };
    }
}
