// <copyright file="LaunchMazeGameCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Commands
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// Command to launch the maze game.
    /// </summary>
    public class LaunchMazeGameCommand(IAutoCompleter autoCompleter, IMazeGameController mazeGameController)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var mazeFiles = Directory.GetFiles("MazeData", "*.maze.txt").ToList();
            var mazeFile = autoCompleter.ReadLine(
                mazeFiles,
                "Tab through the available maze files: ");
            var options = new MazeGameOptions()
                .WithMapViewOrigin(41, 3)
                .WithPovViewOrigin(0, 3)
                .WithStatusOrigin(0, 0)
                .WithMazeDataFile(mazeFile);
            mazeGameController.Initialise(options);
            mazeGameController.Play();
        }
    }
}
