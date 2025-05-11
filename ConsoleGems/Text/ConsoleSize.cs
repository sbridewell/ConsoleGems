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
    /// <param name="width">The zero-based width in characters.</param>
    /// <param name="height">The zero-based height in characters.</param>
    public struct ConsoleSize(int width, int height)
    {
        /// <summary>
        /// Gets the zero-based width in characters.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the zero-based height in characters.
        /// </summary>
        public int Height => height;

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"ConsoleSize Width:{width} Height:{height}";
        }
    }
}
