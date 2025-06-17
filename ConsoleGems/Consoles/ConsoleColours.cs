// <copyright file="ConsoleColours.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles
{
    /// <summary>
    /// Holds a pair of console colours, foreground and background.
    /// </summary>
    /// <param name="foreground">The foreground colour.</param>
    /// <param name="background">The background colour.</param>
    [ExcludeFromCodeCoverage]
    public class ConsoleColours(ConsoleColor foreground, ConsoleColor background)
    {
        /// <summary>
        /// Gets or sets the default foreground and background colours.
        /// </summary>
        public static ConsoleColours Default { get; set; } = new ConsoleColours(ConsoleColor.White, ConsoleColor.Black);

        /// <summary>
        /// Gets or sets the foreground and background colours for prompts for user input.
        /// </summary>
        public static ConsoleColours Prompt { get; set; } = new ConsoleColours(ConsoleColor.Green, ConsoleColor.Black);

        /// <summary>
        /// Gets or sets the foreground and background colours for user input.
        /// </summary>
        public static ConsoleColours UserInput { get; set; } = new ConsoleColours(ConsoleColor.Blue, ConsoleColor.Black);

        /// <summary>
        /// Gets or sets the foreground and background colours for menu headers.
        /// </summary>
        public static ConsoleColours MenuHeader { get; set; } = new ConsoleColours(ConsoleColor.White, ConsoleColor.DarkBlue);

        /// <summary>
        /// Gets or sets the foreground and background colours for menus.
        /// </summary>
        public static ConsoleColours MenuBody { get; set; } = new ConsoleColours(ConsoleColor.Yellow, ConsoleColor.Black);

        /// <summary>
        /// Gets or sets the foreground and background colours for error messages.
        /// </summary>
        public static ConsoleColours Error { get; set; } = new ConsoleColours(ConsoleColor.Red, ConsoleColor.Black);

        public static ConsoleColours Red { get; } = new ConsoleColours(ConsoleColor.Red, ConsoleColor.Black);
        public static ConsoleColours Green { get; } = new ConsoleColours(ConsoleColor.Green, ConsoleColor.Black);
        public static ConsoleColours Yellow { get; } = new ConsoleColours(ConsoleColor.Yellow, ConsoleColor.Black);
        public static ConsoleColours Blue { get; } = new ConsoleColours(ConsoleColor.Blue, ConsoleColor.Black);


        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public ConsoleColor Foreground => foreground;

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public ConsoleColor Background => background;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Foreground: {this.Foreground}, Background: {this.Background}";
        }
    }
}
