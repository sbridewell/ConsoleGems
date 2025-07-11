// <copyright file="SnakeGameRandomiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Generates random values for the snake game, such as food positions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SnakeGameRandomiser : ISnakeGameRandomiser
    {
        private static readonly Random TheRandom = new ();

        /// <inheritdoc/>
        public ConsolePoint GetFoodPosition(IGame game)
        {
            ConsolePoint foodPosition;
            do
            {
                foodPosition = GetCandidateFoodPosition();
            }
            while (game.Snake.OccupiesPoint(foodPosition));
            return foodPosition;
        }

        private static ConsolePoint GetCandidateFoodPosition()
        {
            return new ConsolePoint(
                TheRandom.Next(0, Game.PlayingSurfaceWidth),
                TheRandom.Next(0, Game.PlayingSurfaceHeight));
        }
    }
}
