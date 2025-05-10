// <copyright file="StatusPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// For writing status messages to the console window.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    public class StatusPainter(IConsole console)
        : IStatusPainter
    {
        private ConsolePoint origin;
        private int width;

        /// <inheritdoc/>
        public void SetOrigin(ConsolePoint origin)
        {
            this.origin = origin;
        }

        /// <inheritdoc/>
        public void SetWidth(int width)
        {
            this.width = width;
        }

        /// <inheritdoc/>
        public void Paint(string text, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            if (text == null)
            {
                return;
            }

            console.CursorLeft = this.origin.X;
            console.CursorTop = this.origin.Y;
            console.Write(new string(' ', this.width), ConsoleOutputType.Default);
            console.CursorLeft = this.origin.X;
            console.CursorTop = this.origin.Y;
            console.Write(new string(text.Take(this.width).ToArray()), outputType);
        }
    }
}
