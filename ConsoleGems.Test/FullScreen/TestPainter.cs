// <copyright file="TestPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using System.Reflection;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;

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
        /// Calls the protected <see cref="Painter.ClearScreenBuffer"/> method.
        /// </summary>
        public void PublicClearScreenBuffer() => this.ClearScreenBuffer();
    }
}
