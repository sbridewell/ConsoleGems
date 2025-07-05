// <copyright file="ScreenBuffer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Represents a screen buffer which holds characters to be written to a console window,
    /// but which allows characters to be updated without rewriting to the console window
    /// every time.
    /// </summary>
    public class ScreenBuffer
    {
        private readonly IConsole console;
        private readonly ConsolePixel[,] screenBuffer;
        private readonly HashSet<int> dirtyLines = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenBuffer"/> class with the specified console, width, and height.
        /// </summary>
        /// <param name="console">The console to write to.</param>
        /// <param name="width">Width of the buffer in characters.</param>
        /// <param name="height">Height of the buffer in characters.</param>
        public ScreenBuffer(IConsole console, int width, int height)
        {
            this.console = console;
            this.Width = width;
            this.Height = height;
            this.screenBuffer = new ConsolePixel[width, height];
            for (var y = 0; y < height; y++)
            {
                this.dirtyLines.Add(y);
                for (var x = 0; x < this.Width; x++)
                {
                    this.screenBuffer[x, y] = new ConsolePixel();
                }
            }
        }

        /// <summary>
        /// Gets the width of the screen buffer in characters.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the screen buffer in characters.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Paints the contents of the screen buffer to the console using the specified painter.
        /// </summary>
        /// <param name="painter">The painter to use.</param>
        public void PaintTo(IPainter painter)
        {
            foreach (var y in this.dirtyLines.OrderBy(i => i))
            {
                this.console.CursorLeft = painter.Origin.X + (painter.HasBorder ? 1 : 0);
                this.console.CursorTop = y + painter.Origin.Y + (painter.HasBorder ? 1 : 0);
                var x = 0;
                while (x < painter.InnerSize.Width)
                {
                    var currentOutputType = this.screenBuffer[x, y].OutputType;
                    var start = x;
                    while (x < painter.InnerSize.Width && this.screenBuffer[x, y].OutputType == currentOutputType)
                    {
                        x++;
                    }

                    var chars = new char[x - start];
                    for (var i = start; i < x; i++)
                    {
                        chars[i - start] = this.screenBuffer[i, y].Character;
                        this.screenBuffer[i, y].IsDirty = false;
                    }

                    this.console.Write(new string(chars), currentOutputType);
                }
            }

            this.dirtyLines.Clear();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < this.Height; y++)
            {
                for (var x = 0; x < this.Width; x++)
                {
                    sb.Append(this.screenBuffer[x, y].Character);
                }

                sb.AppendLine();
            }

            sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);
            return sb.ToString();
        }

        /// <summary>
        /// Gets a string array representation of the current instance,
        /// with one element in the array representing each line in the
        /// screen buffer.
        /// </summary>
        /// <returns>A string array representation of the current instance.</returns>
        public string[] ToStringArray()
        {
            return this.ToString().Split(Environment.NewLine);
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
        public void Write(int x, int y, char character, ConsoleOutputType outputType)
        {
            if (x < 0 || x >= this.Width)
            {
                var msg = $"X coordinate {x} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {this.Width - 1}.";
                throw new ArgumentOutOfRangeException(nameof(x), msg);
            }

            if (y < 0 || y >= this.Height)
            {
                var msg = $"Y coordinate {y} is outside the bounds of the painter's area. "
                    + $"Must be between zero and {this.Height - 1}.";
                throw new ArgumentOutOfRangeException(nameof(y), msg);
            }

            this.screenBuffer[x, y] = new ConsolePixel
            {
                Character = character,
                OutputType = outputType,
                IsDirty = true,
            };
            this.dirtyLines.Add(y);
        }
    }
}
