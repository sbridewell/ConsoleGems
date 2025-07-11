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
        /// Writes a character to the screen buffer at the specified coordinates.
        /// The console window is not written to until the Paint method is called.
        /// </summary>
        /// <param name="x">
        /// The horizontal coordinate of the character, relative to the left edge
        /// of the painter.
        /// </param>
        /// <param name="y">
        /// The vertical coordinate of the character, relative to the top edge
        /// of the painter.
        /// </param>
        /// <param name="character">The character to paint.</param>
        /// <param name="outputType">
        /// The <see cref="ConsoleOutputType"/> to use to render the character.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The X or Y coordinates are outside the bounds of the painter's area.
        /// </exception>
        public void WriteToScreenBuffer(int x, int y, char character, ConsoleOutputType outputType);

        /// <summary>
        /// Writes a character to the screen buffer at the specified coordinates.
        /// The console window is not written to until the Paint method is called.
        /// </summary>
        /// <param name="position">
        /// The position to write at, relative to the top-left corner of the painter.
        /// </param>
        /// <param name="character">The character to paint.</param>
        /// <param name="outputType">
        /// The <see cref="ConsoleOutputType"/> to use to render the character.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The X or Y coordinates are outside the bounds of the painter's area.
        /// </exception>
        public void WriteToScreenBuffer(ConsolePoint position, char character, ConsoleOutputType outputType);

        /// <summary>
        /// Paints the area of the console window that this painter is responsible
        /// for from its screen buffer.
        /// </summary>
        public void Paint();

        /// <summary>
        /// Resets the painter to ensure that it is initialised correctly the next
        /// time it is used.
        /// </summary>
        public void Reset();
    }
}
