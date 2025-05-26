// <copyright file="IStatusPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;

    /// <summary>
    /// Interface for writing status text to the console window.
    /// </summary>
    public interface IStatusPainter : IPainter
    {
        /// <summary>
        /// Writes the supplied text to the status area.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="outputType">Output type, e.g. default, error.</param>
        void Paint(string text, ConsoleOutputType outputType = ConsoleOutputType.Default);
    }
}
