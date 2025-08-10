// <copyright file="CursorHelper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Generators
{
    using System;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Provides helper methods for managing console cursor visibility.
    /// </summary>
    public static class CursorHelper
    {
        /// <summary>
        /// Hides the cursor for the duration of the specified action, then restores its previous visibility.
        /// </summary>
        /// <param name="console">The console whose cursor visibility will be managed.</param>
        /// <param name="action">The action to perform while the cursor is hidden.</param>
        public static void WithCursorHidden(IConsole console, Action action)
        {
            bool prevVisible = console.CursorVisible;
            console.CursorVisible = false;
            try
            {
                action();
            }
            finally
            {
                console.CursorVisible = prevVisible;
            }
        }
    }
}
