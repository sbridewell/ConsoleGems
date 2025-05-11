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
        private readonly string[] screenBuffer = new string[size.Height];

        /// <inheritdoc/>
        public ConsolePoint Position => position;

        /// <inheritdoc/>
        public ConsoleSize Size => size;

        /// <inheritdoc/>
        public IReadOnlyList<string> ScreenBuffer => this.screenBuffer;

        /// <inheritdoc/>
        public void Paint()
        {
            console.CursorVisible = false;
            console.CursorLeft = position.X;
            console.CursorTop = position.Y;
            for (var y = 0; y < size.Height; y++)
            {
                console.Write(this.ScreenBuffer[y]);
                console.CursorLeft = position.X;
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
            if (lineNumber < 0 || lineNumber >= size.Height)
            {
                var msg = $"Line number {lineNumber} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {size.Height - 1}.";
                throw new ArgumentOutOfRangeException(nameof(lineNumber), msg);
            }

            if (text.Length != size.Width)
            {
                var msg = $"The length of the supplied text ({text.Length}) does not match the "
                    + $"width of the painter's area ({size.Width}).";
                throw new ArgumentOutOfRangeException(nameof(text), msg);
            }

            this.screenBuffer[lineNumber] = text;
        }
    }
}
