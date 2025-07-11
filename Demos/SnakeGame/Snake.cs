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
    public class Snake : ISnake
    {
        private readonly Queue<ConsolePoint> segments = new ();

        /// <inheritdoc/>
        public ConsolePoint HeadPosition => this.segments.Count > 0 ? this.segments.Last() : new ConsolePoint(0, 0);

        /// <inheritdoc/>
        public ConsolePoint TailPosition => this.segments.Count > 0 ? this.segments.Peek() : this.HeadPosition;

        /// <inheritdoc/>
        public Direction CurrentDirection { get; set; }

        /// <inheritdoc/>
        public int Length => this.segments.Count;

        /// <inheritdoc/>
        public bool HasRunIntoOwnTail =>
            this.segments.Count > 1 && this.segments.Take(this.segments.Count - 1).Contains(this.HeadPosition);

        /// <inheritdoc/>
        public void Initialise(ConsolePoint initialPosition)
        {
            this.segments.Clear();
            this.segments.Enqueue(initialPosition);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void MoveForward()
        {
            var head = this.segments.Last();
            var newHeadPosition = this.CurrentDirection switch
            {
                Direction.Up => new ConsolePoint(head.X, head.Y - 1),
                Direction.Down => new ConsolePoint(head.X, head.Y + 1),
                Direction.Left => new ConsolePoint(head.X - 1, head.Y),
                Direction.Right => new ConsolePoint(head.X + 1, head.Y),
                _ => throw new InvalidOperationException("Invalid direction")
            };
            this.segments.Enqueue(newHeadPosition); // Add the new head segment
        }

        /// <inheritdoc/>
        public void TrimTail()
        {
            this.segments.Dequeue();
        }

        /// <inheritdoc/>
        public bool IsWithin(ConsoleRectangle playingSurface)
        {
            return playingSurface.Contains(this.HeadPosition);
        }

        /// <inheritdoc/>
        public bool OccupiesPoint(ConsolePoint point)
        {
            return this.segments.Contains(point);
        }
    }
}
