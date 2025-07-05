// <copyright file="ISnakeGameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    /// <summary>
    /// Controlling module for the snake game.
    /// </summary>
    public interface ISnakeGameController
    {
        /// <summary>
        /// Implements the main game loop for the snake game.
        /// </summary>
        void Play();
    }
}
