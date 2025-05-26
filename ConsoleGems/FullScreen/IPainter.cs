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
        /// Gets or sets the position of the area of the console window
        /// that the painter is responsible for.
        /// </summary>
        public ConsolePoint Origin { get; set; }

        /// <summary>
        /// Gets or sets the size of the area of the console window that
        /// the painter is responsible for, excluding any space taken up
        /// by a border.
        /// </summary>
        public ConsoleSize InnerSize { get; set; }

        /// <summary>
        /// Gets the size of the area of the console window that the painter
        /// is responsible for, including any space taken up by a border.
        /// This is the amount of space that the painter actually takes up
        /// in the console window.
        /// </summary>
        public ConsoleSize OuterSize { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the painter has a border.
        /// </summary>
        public bool HasBorder { get; set; }

        /// <summary>
        /// Gets the rectangle representing the space taken up by the painter,
        /// including any space taken up by a border.
        /// </summary>
        public ConsoleRectangle OuterBounds => new ConsoleRectangle(this.Origin, this.OuterSize);

        /// <summary>
        /// Paints the area of the console window that this painter is responsible
        /// for from its screen buffer.
        /// </summary>
        public void Paint();
    }
}
