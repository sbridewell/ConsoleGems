// <copyright file="ConsolePoint.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
{
    /// <summary>
    /// Represents a pair of co-ordinates within a console window.
    /// </summary>
    /// <remarks>
    /// Implemented in order to avoid bringing the System.Drawing
    /// namespace into the library, which could cause confusion,
    /// unnecessary increase in application size, and possible
    /// cross-platform compatibility issues.
    /// </remarks>
    /// <param name="x">The zero-based x co-ordinate.</param>
    /// <param name="y">The zero-based y co-ordinate.</param>
    public readonly struct ConsolePoint(int x, int y)
    {
        /// <summary>
        /// Gets the zero-based X co-ordinate.
        /// </summary>
        public int X => x;

        /// <summary>
        /// Gets the zero-based Y co-ordinate.
        /// </summary>
        public int Y => y;

        /// <summary>
        /// Determines whether two <see cref="ConsolePoint"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="ConsolePoint"/> to compare.</param>
        /// <param name="right">The second <see cref="ConsolePoint"/> to compare.</param>
        /// <returns>
        /// True if the <paramref name="left"/> and <paramref name="right"/> instances
        /// have the same X and Y coordinates, otherwise false.
        /// </returns>
        public static bool operator ==(ConsolePoint left, ConsolePoint right)
            => left.X == right.X && left.Y == right.Y;

        /// <summary>
        /// Determines whether two <see cref="ConsolePoint"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ConsolePoint"/> to compare.</param>
        /// <param name="right">The second <see cref="ConsolePoint"/> to compare.</param>
        /// <returns>
        /// True if the <paramref name="left"/> and <paramref name="right"/> instances
        /// are not equal, otherwise false.
        /// </returns>
        public static bool operator !=(ConsolePoint left, ConsolePoint right)
            => !(left == right);

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ConsolePoint"/> instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance. This can be null.</param>
        /// <returns>
        /// True if the specified object is a <see cref="ConsolePoint"/> and is equal to the current
        /// instance; otherwise, false.</returns>
        public override bool Equals(object? obj)
            => obj is ConsolePoint other && this == other;

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        /// <remarks>The hash code is computed based on the values of the <c>x</c> and <c>y</c> fields.
        /// This method is suitable for use in hashing algorithms and data structures such as hash tables.</remarks>
        /// <returns>An integer that represents the hash code for the current object.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
            => HashCode.Combine(x, y);

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{x},{y}";
        }
    }
}
