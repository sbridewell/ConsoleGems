// <copyright file="IStatusPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Interface for writing status text to the console window.
    /// </summary>
    public interface IStatusPainter
    {
        /// <summary>
        /// Sets the top left corner of the status area within the console window.
        /// </summary>
        /// <param name="origin">The top left corner position of the status area.</param>
        void SetOrigin(ConsolePoint origin);

        /// <summary>
        /// Sets the width of the status area in characters.
        /// </summary>
        /// <param name="width">Width in characters.</param>
        void SetWidth(int width);

        /// <summary>
        /// Writes the supplied text to the status area.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="outputType">Output type, e.g. default, error.</param>
        void Paint(string text, ConsoleOutputType outputType = ConsoleOutputType.Default);
    }
}
