// <copyright file="ConsoleOutputType.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles
{
    /// <summary>
    /// Enumeration of types of console output, which can be used
    /// to indicate to console implementations how to format the output.
    /// </summary>
    public enum ConsoleOutputType
    {
        /// <summary>
        /// Default.
        /// </summary>
        Default,

        /// <summary>
        /// A prompt for user input.
        /// </summary>
        Prompt,

        /// <summary>
        /// The text entered by the user.
        /// </summary>
        UserInput,

        /// <summary>
        /// An error message.
        /// </summary>
        Error,

        /// <summary>
        /// The header of a menu.
        /// </summary>
        MenuHeader,

        /// <summary>
        /// The body text of a menu.
        /// </summary>
        MenuBody,

        /// <summary>
        /// Red foreground, black background.
        /// </summary>
        Red,

        /// <summary>
        /// Yellow foreground, black background.
        /// </summary>
        Yellow,

        /// <summary>
        /// Blue foreground, black background.
        /// </summary>
        Blue,

        /// <summary>
        /// Green foreground, black background.
        /// </summary>
        Green,
    }
}
