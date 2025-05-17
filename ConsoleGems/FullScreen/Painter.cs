// <copyright file="Painter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Abstract base class for <see cref="IPainter"/> implementations.
    /// </summary>
    public abstract class Painter(IConsole console, ConsolePoint origin, ConsoleSize innerSize, bool hasBorder)
        : IPainter
    {
        private readonly string[] screenBuffer = new string[innerSize.Height];

        /// <inheritdoc/>
        public ConsolePoint Origin => origin;

        /// <inheritdoc/>
        public ConsoleSize InnerSize => innerSize;

        /// <inheritdoc/>
        public ConsoleSize OuterSize => new (
            innerSize.Width + (hasBorder ? 2 : 0),
            innerSize.Height + (hasBorder ? 2 : 0));

        /// <inheritdoc/>
        public IReadOnlyList<string> ScreenBuffer => this.screenBuffer;

        /// <inheritdoc/>
        public void Paint()
        {
            console.CursorVisible = false;
            console.CursorLeft = origin.X;
            console.CursorTop = origin.Y;
            this.PaintTopBorderIfRequired();

            for (var y = 0; y < innerSize.Height; y++)
            {
                console.CursorLeft = origin.X;
                this.PaintSideBorderIfRequired();
                console.Write(this.ScreenBuffer[y]);
                this.PaintSideBorderIfRequired();
                if (console.CursorTop < console.WindowHeight - 1)
                {
                    console.CursorTop++;
                }
            }

            console.CursorLeft = origin.X;
            this.PaintBottomBorderIfRequired();
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
            if (lineNumber < 0 || lineNumber >= innerSize.Height)
            {
                var msg = $"Line number {lineNumber} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {innerSize.Height - 1}.";
                throw new ArgumentOutOfRangeException(nameof(lineNumber), msg);
            }

            if (text.Length != innerSize.Width)
            {
                var msg = $"The length of the supplied text ({text.Length}) does not match the "
                    + $"width of the painter's area ({innerSize.Width}).";
                throw new ArgumentOutOfRangeException(nameof(text), msg);
            }

            this.screenBuffer[lineNumber] = text;
        }

        private void PaintTopBorderIfRequired()
        {
            if (hasBorder)
            {
                console.Write("╭");
                for (var x = 0; x < innerSize.Width; x++)
                {
                    console.Write("─");
                }

                console.Write("╮");
                console.CursorTop++;
            }
        }

        private void PaintSideBorderIfRequired()
        {
            if (hasBorder)
            {
                console.Write("│");
            }
        }

        private void PaintBottomBorderIfRequired()
        {
            if (hasBorder)
            {
                console.CursorLeft = origin.X;
                console.Write("╰");
                for (var x = 0; x < innerSize.Width; x++)
                {
                    console.Write("─");
                }

                console.Write("╯");
            }
        }
    }
}
