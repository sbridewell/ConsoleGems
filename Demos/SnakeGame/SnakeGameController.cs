// <copyright file="SnakeGameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The controlling module for the snake game.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SnakeGameController(IGame game)
        : ISnakeGameController
    {
        /// <inheritdoc/>
        public void Play()
        {
            game.Initialise();
            while (!game.GameOver)
            {
                game.Iterate();
            }

            game.TearDown();
        }
    }
}
