// <copyright file="RenderHelper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    using System;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Provides helper methods for rendering lines of ASCII art to the console.
    /// </summary>
    public static class RenderHelper
    {
        /// <summary>
        /// Renders lines of ASCII art to the console using a buffer construction strategy.
        /// </summary>
        /// <param name="consoleWidth">The width of the console output area.</param>
        /// <param name="consoleHeight">The height of the console output area.</param>
        /// <param name="bufferBuilder">A function that builds the buffer for each line.</param>
        /// <param name="console">The console to write to.</param>
        public static void RenderLines(int consoleWidth, int consoleHeight, Func<int, int, AsciiArtGenerator.CharacterColourRun[]> bufferBuilder, IConsole console)
        {
            for (int y = 0; y < consoleHeight; y++)
            {
                var buffer = bufferBuilder(y, consoleWidth);
                foreach (var run in buffer)
                {
                    console.Write(run.Text, run.Colours);
                }

                console.WriteLine();
            }
        }
    }
}
