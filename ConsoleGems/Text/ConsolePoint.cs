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
    /// unneccessary increase in application size, and possble
    /// cross-platform compatibility issues.
    /// </remarks>
    /// <param name="x">The zero-based x co-ordinate.</param>
    /// <param name="y">The zer-based y co-ordinate.</param>
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
    }
}
