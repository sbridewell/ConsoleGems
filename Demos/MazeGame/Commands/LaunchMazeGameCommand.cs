// <copyright file="LaunchMazeGameCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Commands
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Commands;

    /// <summary>
    /// Command to launch the maze game.
    /// </summary>
    public class LaunchMazeGameCommand(IAutoCompleter autoCompleter, IGameController mazeGameController)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var mazeFiles = Directory.GetFiles("MazeData", "*.maze.txt").ToList();
            var mazeFile = autoCompleter.ReadLine(
                mazeFiles,
                "Tab through the available maze files: ");
            mazeGameController.Play(mazeFile);
        }
    }
}
