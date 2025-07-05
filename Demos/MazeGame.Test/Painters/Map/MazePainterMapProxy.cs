// <copyright file="MazePainterMapProxy.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Painters.Map
{
    using System.Reflection;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.Painters.Map;

    /// <summary>
    /// Proxy for the <see cref="MazePainterMap"/> class which exposes its protected ScreenBuffer
    /// property for unit testing.
    /// </summary>
    public class MazePainterMapProxy(
        IConsole console,
        IBorderPainter borderPainter,
        IWallCharacterProvider wallCharacterProvider,
        IPlayerCharacterProvider playerCharacterProvider)
        : MazePainterMap(console, borderPainter, wallCharacterProvider, playerCharacterProvider)
    {
        /// <summary>
        /// Gets the value of the ScreenBuffer protected property.
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
    }
}
