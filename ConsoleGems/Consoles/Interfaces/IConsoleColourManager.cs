// <copyright file="IConsoleColourManager.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles.Interfaces
{
    /// <summary>
    /// Interface for a colourful console.
    /// </summary>
    public interface IConsoleColourManager
    {
        /// <summary>
        /// Sets the foreground and background colours of the console window.
        /// </summary>
        /// <param name="colours">The colours to set the console to.</param>
        void SetColours(ConsoleColours colours);
    }
}
