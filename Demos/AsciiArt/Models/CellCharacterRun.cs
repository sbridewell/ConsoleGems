// <copyright file="CellCharacterRun.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Represents a run of identical characters and colours for cell-mapper batching.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class CellCharacterRun(string text, ConsoleColours colours)
    {
        /// <summary>
        /// Gets or sets the text for this run.
        /// </summary>
        public string Text { get; set; } = text;

        /// <summary>
        /// Gets or sets the colours for this run.
        /// </summary>
        public ConsoleColours Colours { get; set; } = colours;
    }
}
