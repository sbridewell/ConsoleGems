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
        // TODO: can we make Segments private?

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
        /// Gets or sets the current position of the snake's head.
        /// </summary>
        public ConsolePoint Position { get; set; }

        /// <summary>
        /// Gets or sets the current direction of the snake.
        /// </summary>
        public Direction CurrentDirection { get; set; }

        /// <summary>
        /// Gets the length of the snake.
        /// </summary>
        public int Length => this.Segments.Count;

        /// <summary>
        /// Gets a value indicating whether the snake has run into its own tail.
        /// </summary>
        public bool HasRunIntoOwnTail =>
            this.Segments.Count > 1 && this.Segments.Take(this.Segments.Count - 1).Contains(this.Position);

        /// <summary>
        /// Initialises the snake by resetting its length and moving it to the initial position.
        /// </summary>
        /// <param name="initialPosition">The snake's starting position.</param>
        public void Initialise(ConsolePoint initialPosition)
        {
            this.Segments.Clear();
            this.Segments.Enqueue(initialPosition);
        }

        /// <summary>
        /// Changes the snake's direction of travel, unless the new direction is the opposite of the current direction.
        /// </summary>
        /// <param name="newDirection">The new direction.</param>
        public void ChangeDirection(Direction newDirection)
        {
            if ((this.CurrentDirection == Direction.Up && newDirection == Direction.Down) ||
                (this.CurrentDirection == Direction.Down && newDirection == Direction.Up) ||
                (this.CurrentDirection == Direction.Left && newDirection == Direction.Right) ||
                (this.CurrentDirection == Direction.Right && newDirection == Direction.Left))
            {
                return;
            }

            this.CurrentDirection = newDirection;
        }

        /// <summary>
        /// Moves the snake forward by one position.
        /// </summary>
        public void MoveForward()
        {
            var head = this.Segments.Last();
            switch (this.CurrentDirection)
            {
                case Direction.Up:
                    this.Position = new ConsolePoint(head.X, head.Y - 1);
                    break;
                case Direction.Down:
                    this.Position = new ConsolePoint(head.X, head.Y + 1);
                    break;
                case Direction.Left:
                    this.Position = new ConsolePoint(head.X - 1, head.Y);
                    break;
                case Direction.Right:
                    this.Position = new ConsolePoint(head.X + 1, head.Y);
                    break;
            }

            this.Segments.Enqueue(this.Position); // Add the new head segment
        }

        /// <summary>
        /// Determines whether the head of the snake is within the supplied rectangle.
        /// This can be used to test whether the snake has hit a wall.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>True if the snake's head is within the rectangle.</returns>
        public bool IsWithin(ConsoleRectangle rectangle)
        {
            return rectangle.Contains(this.Position);
        }

        /// <summary>
        /// Determines whether the supplied point is occupied by the snake.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>True if the point contains part of the snake.</returns>
        public bool OccupiesPoint(ConsolePoint point)
        {
            return this.Segments.Contains(point);
        }
    }
}
