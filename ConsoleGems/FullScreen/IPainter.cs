// <copyright file="IPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Interface for classes which take responsibility for writing content
    /// to a rectangular area of the console window.
    /// </summary>
    public interface IPainter
    {
        /// <summary>
        /// Gets the position of the area of the console window that the
        /// painter is responsible for.
        /// </summary>
        public ConsolePoint Position { get; }

        /// <summary>
        /// Gets the size of the area of the console window that the painter
        /// is responsible for.
        /// </summary>
        public ConsoleSize Size { get; }
    }
}
