// <copyright file="ApplicationState.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems
{
    /// <summary>
    /// Static class to track information about the state of the application.
    /// </summary>
    public class ApplicationState
    {
        /// <summary>
        /// Gets or sets the current working directory.
        /// </summary>
        /// <remarks>
        /// I recommend setting this to an application-specific location in
        /// Program.cs.
        /// </remarks>
        public DirectoryInfo WorkingDirectory { get; set; }
            = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        /// <summary>
        /// Gets or sets the number of menus from the main menu to the
        /// menu the user is currently on.
        /// </summary>
        public int MenuDepth { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value indicating whether the application should
        /// exit the current menu and return to the previous menu.
        /// </summary>
        public bool ExitCurrentMenu { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the application should exit.
        /// </summary>
        public bool ExitProgram { get; set; } = false;
    }
}
