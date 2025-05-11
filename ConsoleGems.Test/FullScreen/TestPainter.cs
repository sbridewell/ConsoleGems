// <copyright file="TestPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// <see cref="IPainter"/> implementation for use in unit testing
    /// the abstract <see cref="Painter"/> class.
    /// </summary>
    public class TestPainter(IConsole console, ConsolePoint position, ConsoleSize innerSize, bool hasBorder)
        : Painter(console, position, innerSize, hasBorder)
    {
        /// <summary>
        /// Gets the value of the <see cref="Painter.ScreenBuffer"/> property.
        /// </summary>
        public IReadOnlyList<string> PublicScreenBuffer => this.ScreenBuffer;

        /// <summary>
        /// Calls the protected <see cref="Painter.WriteToScreenBuffer(int, string)"/> method.
        /// </summary>
        /// <param name="lineNumber">The line number to write to.</param>
        /// <param name="text">The text to write to the screen buffer.</param>
        public void PublicWriteToScreenBuffer(int lineNumber, string text)
            => this.WriteToScreenBuffer(lineNumber, text);
    }
}
