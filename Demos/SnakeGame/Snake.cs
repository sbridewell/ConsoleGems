// <copyright file="Snake.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Represents the snake controlled by the player.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Gets or sets the current direction of the snake.
        /// </summary>
        public Direction CurrentDirection { get; set; }

        /// <summary>
        /// Gets the segments of the snake.
        /// </summary>
        /// <remarks>
        /// This is implemented as a <see cref="Queue{ConsolePoint}"/> so that as the snake
        /// moves forward, we can simply enqueue the new head position and dequeue the tail
        /// position.
        /// </remarks>
        public Queue<ConsolePoint> Segments { get; } = new ();

        /// <summary>
        /// Gets the length of the snake.
        /// </summary>
        public int Length => this.Segments.Count;
    }
}
