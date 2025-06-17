// <copyright file="MazePainterPovProxy.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Painters.Pov
{
    using System.Reflection;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Pov;

    /// <summary>
    /// A proxy for the <see cref="MazePainterPov"/> class which exposes its protected
    /// methods for unit testing.
    /// </summary>
    public class MazePainterPovProxy(
        IConsole console,
        IBorderPainter borderPainter,
        ISectionRenderer sectionRenderer)
        : MazePainterPov(console, borderPainter, sectionRenderer)
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
        /// Calls the RenderForwardView method of the base class.
        /// </summary>
        /// <param name="forwardView">The ForwardView to render.</param>
        public void CallRenderForwardView(ForwardView forwardView, Direction playerFacingDirection)
        {
            this.RenderForwardView(forwardView, playerFacingDirection);
        }
    }
}
