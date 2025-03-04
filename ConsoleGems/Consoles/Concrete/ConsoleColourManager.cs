// <copyright file="ConsoleColourManager.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles.Concrete
{
    /// <summary>
    /// Sets the foreground and background colours of the console window.
    /// </summary>
    /// <remarks>
    /// Excluded from code coverage because setting the ForegroundColor
    /// and BackgroundColour properties of <see cref="Console"/> when
    /// console output is redirected (e.g. when running a unit test)
    /// does not appear to have any effect on the property values.
    /// This very small class exists so that unit tests can mock the
    /// <see cref="IConsoleColourManager"/> interface and verify that
    /// the correct calls to set colours have been made.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class ConsoleColourManager : IConsoleColourManager
    {
        /// <summary>
        /// Sets the foreground and background colours of the console window.
        /// </summary>
        /// <param name="colours">The colours to set the console to.</param>
        public void SetColours(ConsoleColours colours)
        {
            System.Console.ForegroundColor = colours.Foreground;
            System.Console.BackgroundColor = colours.Background;
        }
    }
}
