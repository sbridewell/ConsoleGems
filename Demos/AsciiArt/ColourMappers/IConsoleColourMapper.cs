// <copyright file="IConsoleColourMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.ColourMappers
{
    using System;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Defines a contract for mapping a pixel to a ConsoleColor.
    /// </summary>
    public interface IConsoleColourMapper
    {
        /// <summary>
        /// Maps a pixel to the closest ConsoleColor.
        /// </summary>
        /// <param name="pixel">The pixel to map.</param>
        /// <returns>The closest ConsoleColor.</returns>
        ConsoleColor MapToConsoleColor(Pixel pixel);
    }
}
