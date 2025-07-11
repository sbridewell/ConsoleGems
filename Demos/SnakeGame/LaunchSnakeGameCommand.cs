// <copyright file="LaunchSnakeGameCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Commands;

    /// <summary>
    /// Command to launch the snake game.
    /// </summary>
    public class LaunchSnakeGameCommand(ISnakeGameController snakeGameController)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            snakeGameController.Play();
        }
    }
}
