// <copyright file="SnakeGamePainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.SnakeGame
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;

    /// <summary>
    /// Paints the snake game to the console window.
    /// </summary>
    public class SnakeGamePainter(IConsole console, IBorderPainter borderPainter)
        : Painter(console, borderPainter),
        ISnakeGamePainter
    {
    }
}
