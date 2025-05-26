// <copyright file="StatusPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;

    /// <summary>
    /// For writing status messages to the console window.
    /// </summary>
    public class StatusPainter(IConsole console, IBorderPainter borderPainter)
        : Painter(console, borderPainter), IStatusPainter
    {
        /// <inheritdoc/>
        public void Paint(string text, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            if (text == null)
            {
                return;
            }

            for (var i = 0; i < this.InnerSize.Width; i++)
            {
                this.WriteToScreenBuffer(i, 0, ' ', outputType);
            }

            for (var i = 0; i < text.Length; i++)
            {
                this.WriteToScreenBuffer(i, 0, text[i], outputType);
            }

            this.Paint();
        }
    }
}
