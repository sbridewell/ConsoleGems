// <copyright file="ConsoleRectangle.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
{
    /// <summary>
    /// Represents a rectangle in a console window.
    /// </summary>
    public struct ConsoleRectangle(ConsolePoint origin, ConsoleSize size)
    {
        /// <summary>
        /// Gets the top-left character position of the rectangle relative to
        /// the console window.
        /// </summary>
        public readonly ConsolePoint Origin => origin;

        /// <summary>
        /// Gets the size of the rectangle in characters.
        /// </summary>
        public readonly ConsoleSize Size => size;

        /// <summary>
        /// Gets the left character position of the rectangle relative to the
        /// console window.
        /// </summary>
        public readonly int X => this.Origin.X;

        /// <summary>
        /// Gets the top character position of the rectangle relative to the
        /// console window.
        /// </summary>
        public readonly int Y => this.Origin.Y;

        /// <summary>
        /// Gets the width of the rectangle in characters.
        /// </summary>
        public readonly int Width => this.Size.Width;

        /// <summary>
        /// Gets the height of the rectangle in characters.
        /// </summary>
        public readonly int Height => this.Size.Height;

        /// <summary>
        /// Gets the right character position of the rectangle relative to the
        /// console window.
        /// </summary>
        public readonly int Right => this.X + this.Width - 1;

        /// <summary>
        /// Gets the bottom character position of the rectangle relative to the
        /// console window.
        /// </summary>
        public readonly int Bottom => this.Y + this.Height - 1;

        /// <summary>
        /// Determines whether the current rectangle contains the supplied point.
        /// </summary>
        /// <param name="pointToTest">The point to test.</param>
        /// <returns>
        /// True if the rectangle contains the supplied point, otherwise false.
        /// </returns>
        public readonly bool Contains(ConsolePoint pointToTest)
        {
            return pointToTest switch
            {
                _ when pointToTest.X < this.X => false,
                _ when pointToTest.Y < this.Y => false,
                _ when pointToTest.X >= this.X + this.Width => false,
                _ when pointToTest.Y >= this.Y + this.Height => false,
                _ => true
            };
        }

        /// <summary>
        /// Determines whether the current rectangle overlaps with another rectangle.
        /// </summary>
        /// <param name="otherRectangle">The other rectangle.</param>
        /// <returns>True if the rectangles overlap, otherwise false.</returns>
        /// <remarks>
        /// We determine whether or not the rectangles overlap by testing whether
        /// either of them contains any of the corners of the other rectangle.
        /// </remarks>
        public readonly bool OverlapsWith(ConsoleRectangle otherRectangle)
        {
            return otherRectangle switch
            {
                // Top left and bottom right corners
                _ when this.Contains(otherRectangle.Origin) => true,
                _ when otherRectangle.Contains(this.Origin) => true,

                // Top right and bottom left corners
                _ when this.Contains(new ConsolePoint(otherRectangle.Right, otherRectangle.Y)) => true,
                _ when otherRectangle.Contains(new ConsolePoint(this.Right, this.Y)) => true,

                // Neither rectangle contains any of the corners of the other, so the rectangles
                // don't overlap.
                _ => false
            };
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Origin: {origin}, Size: {size}";
        }
    }
}
