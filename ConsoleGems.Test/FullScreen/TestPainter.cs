// <copyright file="TestPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using System.Reflection;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// <see cref="IPainter"/> implementation for use in unit testing
    /// the abstract <see cref="Painter"/> class.
    /// </summary>
    public class TestPainter(IConsole console, IBorderPainter borderPainter)
        : Painter(console, borderPainter)
    {
        /// <summary>
        /// Gets the value of the screen buffer.
        /// </summary>
        public ScreenBuffer PublicScreenBuffer
        {
            get
            {
                var type = typeof(Painter);
                var fieldInfo = type.GetField("screenBuffer", BindingFlags.NonPublic | BindingFlags.Instance);
                var screenBuffer = fieldInfo!.GetValue(this) as ScreenBuffer;
                return screenBuffer!;
            }
        }

        /// <summary>
        /// Calls the protected <see cref="Painter.WriteToScreenBuffer"/> method.
        /// </summary>
        /// <param name="x">
        /// The horizontal coordinate of the character to write, relative to the left edge of the painter.
        /// </param>
        /// <param name="y">
        /// The vertical coordinate of the character to write, relative to the top edge of the painter.
        /// </param>
        /// <param name="character">The character to write to the sceen buffer.</param>
        /// <param name="outputType">The <see cref="ConsoleOutputType"/> to use when writing the character.</param>"/>
        public void PublicWriteToScreenBuffer(int x, int y, char character, ConsoleOutputType outputType)
            => this.WriteToScreenBuffer(x, y, character, outputType);

        /// <summary>
        /// Calls the protected <see cref="Painter.ClearScreenBuffer"/> method.
        /// </summary>
        public void PublicClearScreenBuffer() => this.ClearScreenBuffer();
    }
}
