// <copyright file="AsciiArtCell.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the result of mapping the colour of a pixel in a source
    /// image to a character to be written to the console, with a combination
    /// of foreground colour, background colour and character to maximise the
    /// number of perceived colours displayed in the console from the different
    /// combinations of foreground colour, background colour and the ratio of
    /// the two colours controlled by the character.
    /// </summary>
    /// <param name="Character">
    /// The character which controls the ratio of foreground to background colour.
    /// </param>
    /// <param name="Foreground">The foreground colour.</param>
    /// <param name="Background">The background colour.</param>
    [ExcludeFromCodeCoverage]
    public record AsciiArtCell(char Character, ConsoleColor Foreground, ConsoleColor Background)
    {
    }
}
