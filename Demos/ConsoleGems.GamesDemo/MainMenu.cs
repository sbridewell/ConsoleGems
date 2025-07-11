﻿// <copyright file="MainMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.GamesDemo
{
    using System.Collections.Generic;
    using Sde.ConsoleGems;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;
    using Sde.MazeGame.Commands;
    using Sde.SnakeGame;

    /// <summary>
    /// The main menu in the games demo.
    /// </summary>
    public class MainMenu(
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState,
        LaunchMazeGameCommand launchMazeGameCommand,
        LaunchSnakeGameCommand launchSnakeGameCommand)
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
            new ()
            {
                Key = "s",
                Description = "A snake game.",
                Command = launchSnakeGameCommand,
            },
        };
    }
}
