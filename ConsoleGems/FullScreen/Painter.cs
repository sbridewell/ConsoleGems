// <copyright file="Painter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Abstract base class for <see cref="IPainter"/> implementations.
    /// </summary>
    public abstract class Painter(IConsole console, IBorderPainter borderPainter)
        : IPainter
    {
        private readonly HashSet<int> dirtyLines = new ();
        private ConsoleSize innerSize;
        private ConsolePixel[][] screenBuffer = new ConsolePixel[0][];

        /// <inheritdoc/>
        public ConsolePoint Origin { get; set; }

        /// <inheritdoc/>
        public ConsoleSize InnerSize
        {
            get => this.innerSize;
            set
            {
                borderPainter.Painter = this;
                this.innerSize = value;
                this.screenBuffer = new ConsolePixel[value.Height][];
                for (var y = 0; y < value.Height; y++)
                {
                    this.dirtyLines.Add(y);
                    this.screenBuffer[y] = new ConsolePixel[value.Width];
                    for (var x = 0; x < value.Width; x++)
                    {
                        this.screenBuffer[y][x] = new ConsolePixel();
                    }
                }
            }
        }

        /// <inheritdoc/>
        public bool HasBorder { get; set; }

        /// <inheritdoc/>
        public ConsoleSize OuterSize => new (
            this.InnerSize.Width + (this.HasBorder ? 2 : 0),
            this.InnerSize.Height + (this.HasBorder ? 2 : 0));

        /// <inheritdoc/>
        public void Paint()
        {
            borderPainter.PaintTopBorderIfRequired();

            foreach (var y in this.dirtyLines.OrderBy(i => i))
            {
                console.CursorLeft = this.Origin.X + (this.HasBorder ? 1 : 0);
                console.CursorTop = y + this.Origin.Y + (this.HasBorder ? 1 : 0);
                borderPainter.PaintSideBorderIfRequired(isLeftBorder: true);
                var x = 0;
                while (x < this.innerSize.Width)
                {
                    var currentOutputType = this.screenBuffer[y][x].OutputType;
                    var start = x;
                    while (x < this.innerSize.Width && this.screenBuffer[y][x].OutputType == currentOutputType)
                    {
                        x++;
                    }

                    var chars = new char[x - start];
                    for (var i = start; i < x; i++)
                    {
                        chars[i - start] = this.screenBuffer[y][i].Character;
                        this.screenBuffer[y][i].IsDirty = false;
                    }

                    console.Write(new string(chars), currentOutputType);
                }

                borderPainter.PaintSideBorderIfRequired(isLeftBorder: false);
            }

            this.dirtyLines.Clear();
            borderPainter.PaintBottomBorderIfRequired();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Origin: {this.Origin}, InnerSize: {this.InnerSize}, HasBorder: {this.HasBorder}";
        }

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
        protected void WriteToScreenBuffer(int x, int y, char character, ConsoleOutputType outputType)
        {
            if (x < 0 || x >= this.InnerSize.Width)
            {
                var msg = $"X coordinate {x} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {this.InnerSize.Width - 1}.";
                throw new ArgumentOutOfRangeException(nameof(x), msg);
            }

            if (y < 0 || y >= this.InnerSize.Height)
            {
                var msg = $"Y coordinate {y} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {this.InnerSize.Height - 1}.";
                throw new ArgumentOutOfRangeException(nameof(y), msg);
            }

            this.screenBuffer[y][x] = new ConsolePixel
            {
                Character = character,
                OutputType = outputType,
                IsDirty = true,
            };
            this.dirtyLines.Add(y);
        }
    }
}
