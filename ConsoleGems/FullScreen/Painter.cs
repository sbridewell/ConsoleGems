﻿// <copyright file="Painter.cs" company="Simon Bridewell">
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
        private ConsoleSize innerSize;
        private ScreenBuffer? screenBuffer;

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
                this.screenBuffer = new ScreenBuffer(console, value.Width, value.Height);
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
            if (this.screenBuffer == null)
            {
                throw new InvalidOperationException("ScreenBuffer is not initialised. Set InnerSize before calling Paint.");
            }

            borderPainter.PaintBorderIfRequired();
            this.screenBuffer.PaintTo(this);
        }

        /// <inheritdoc/>
        public void Reset()
        {
            borderPainter.Reset();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Origin: {this.Origin}, InnerSize: {this.InnerSize}, HasBorder: {this.HasBorder}";
        }

        /// <inheritdoc/>
        public void WriteToScreenBuffer(ConsolePoint position, char character, ConsoleOutputType outputType)
        {
            this.WriteToScreenBuffer(position.X, position.Y, character, outputType);
        }

        /// <inheritdoc/>
        public void WriteToScreenBuffer(int x, int y, char character, ConsoleOutputType outputType)
        {
            if (this.screenBuffer == null)
            {
                throw new InvalidOperationException("ScreenBuffer is not initialised. Set InnerSize before writing to the screen buffer.");
            }

            this.screenBuffer.Write(x, y, character, outputType);
        }

        /// <summary>
        /// Initialises all the characters in the screen buffer to spaces.
        /// </summary>
        protected void ClearScreenBuffer()
        {
            for (var y = 0; y < this.innerSize.Height; y++)
            {
                for (var x = 0; x < this.innerSize.Width; x++)
                {
                    this.WriteToScreenBuffer(x, y, ' ', ConsoleOutputType.Default);
                }
            }
        }
    }
}
