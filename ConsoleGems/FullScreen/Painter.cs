// <copyright file="Painter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Abstract base class for <see cref="IPainter"/> implementations.
    /// </summary>
    public abstract class Painter(IConsole console, ConsolePoint position, ConsoleSize size)
        : IPainter
    {
        /// <inheritdoc/>
        public ConsolePoint Position => position;

        /// <inheritdoc/>
        public ConsoleSize Size => size;

        /// <summary>
        /// Gets a screen buffer from which the content of the painter's
        /// area of the console window can be drawn.
        /// </summary>
        protected string[] ScreenBuffer { get; } = new string[size.Vertical];

        /// <summary>
        /// Paints the area of the console window that this painter is responsible
        /// for from its <see cref="ScreenBuffer"/> property.
        /// Concrete implementations are expected to have already populated the
        /// <see cref="ScreenBuffer"/> before this method is called.
        /// </summary>
        public void Paint()
        {
            console.CursorVisible = false;
            console.CursorLeft = position.X;
            console.CursorTop = position.Y;
            for (var y = 0; y < size.Vertical; y++)
            {
                console.WriteLine(this.ScreenBuffer[y]);
            }

            console.CursorVisible = true;
        }

        /// <summary>
        /// Writes the supplied line of text to the screen buffer at the
        /// supplied line number.
        /// Any content which was previously in the line will be overwritten.
        /// </summary>
        /// <param name="lineNumber">The zero-based line number to write to.</param>
        /// <param name="text">The text to write.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The supplied line number is outside the bounds of the area of the
        /// console window that the painter is responsible for, or the length of
        /// the supplied line of text does not match the width of the area of
        /// the console window that the painter is responsible for.
        /// </exception>
        protected void WriteToScreenBuffer(int lineNumber, string text)
        {
            // TODO: options to pad with spaces or to justify text?
            // TODO: replace \0 characters with spaces?
            if (lineNumber < 0 || lineNumber >= size.Vertical)
            {
                var msg = $"Line number {lineNumber} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {size.Vertical - 1}.";
                throw new ArgumentOutOfRangeException(nameof(lineNumber), msg);
            }

            if (text.Length != size.Horizontal)
            {
                var msg = $"The length of the supplied text ({text.Length}) does not match the "
                    + $"width of the painter's area ({size.Horizontal}).";
                throw new ArgumentOutOfRangeException(nameof(text), msg);
            }

            this.ScreenBuffer[lineNumber] = text;
        }
    }
}
