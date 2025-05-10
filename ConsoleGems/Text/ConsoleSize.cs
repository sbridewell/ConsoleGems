// <copyright file="ConsoleSize.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
{
    /// <summary>
    /// Represents the size of a rectangle in a console window.
    /// </summary>
    /// <remarks>
    /// Implemented in order to avoid bringing the System.Drawing
    /// namespace into the library, which could cause confusion,
    /// unnecessary increase in application size, and possible
    /// cross-platform compatibility issues.
    /// </remarks>
    /// <param name="horizontal">The zero-based horizontal size in characters.</param>
    /// <param name="vertical">The zero-based vertical size in characters.</param>
    public struct ConsoleSize(int horizontal, int vertical)
    {
        /// <summary>
        /// Gets the zero-based horizontal size in characters.
        /// </summary>
        public int Horizontal => horizontal;

        /// <summary>
        /// Gets the zero-based vertical size in characters.
        /// </summary>
        public int Vertical => vertical;

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"ConsoleSize Horizontal:{horizontal} Vertical:{vertical}";
        }
    }
}
