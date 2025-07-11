// <copyright file="ISnake.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Interface representing a snake in the Snake Game.
    /// </summary>
    public interface ISnake
    {
        /// <summary>
        /// Gets the current position of the snake's head.
        /// </summary>
        ConsolePoint HeadPosition { get; }

        /// <summary>
        /// Gets the current position of the snake's tail.
        /// </summary>
        ConsolePoint TailPosition { get; }

        /// <summary>
        /// Gets the current direction of the snake.
        /// </summary>
        Direction CurrentDirection { get; }

        /// <summary>
        /// Gets the length of the snake.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets a value indicating whether the snake has run into its own tail.
        /// </summary>
        bool HasRunIntoOwnTail { get; }

        /// <summary>
        /// Initialises the snake by resetting its length and moving it to the initial position.
        /// </summary>
        /// <param name="initialPosition">The snake's starting position.</param>
        void Initialise(ConsolePoint initialPosition);

        /// <summary>
        /// Changes the snake's direction of travel, unless the new direction is the opposite of the current direction.
        /// </summary>
        /// <param name="newDirection">The new direction.</param>
        void ChangeDirection(Direction newDirection);

        /// <summary>
        /// Moves the snake forward by one position.
        /// </summary>
        void MoveForward();

        /// <summary>
        /// Removes the tail segment of the sname.
        /// This should be called in conjunction with the MoveForward method,
        /// unless the snake has just eaten food, in which case the tail should
        /// not be removed.
        /// </summary>
        void TrimTail();

        /// <summary>
        /// Determines whether the head of the snake is within the supplied rectangle.
        /// This can be used to test whether the snake has hit a wall.
        /// </summary>
        /// <param name="playingSurface">The rectangle.</param>
        /// <returns>True if the snake's head is within the rectangle.</returns>
        bool IsWithin(ConsoleRectangle playingSurface);

        /// <summary>
        /// Determines whether the supplied point is occupied by the snake.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>True if the point contains part of the snake.</returns>
        bool OccupiesPoint(ConsolePoint point);
    }
}
