// <copyright file="ConsoleColorMapperTestData.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test.ColourMappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    /// Provides test data for ConsoleColorMapper_MapsRgbToConsoleColor_AllValuesAndEdgeCases.
    /// </summary>
    public class ConsoleColorMapperTestData : IEnumerable<object[]>
    {
        /// <summary>
        /// Returns an enumerator that iterates through the test data.
        /// </summary>
        /// <returns>An enumerator for the test data.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            // Red shades
            yield return new object[] { 0, 0, 0, ConsoleColor.Black };
            yield return new object[] { 8, 0, 0, ConsoleColor.Black };
            yield return new object[] { 16, 0, 0, ConsoleColor.Black };
            yield return new object[] { 24, 0, 0, ConsoleColor.Black };
            yield return new object[] { 32, 0, 0, ConsoleColor.Black };
            yield return new object[] { 40, 0, 0, ConsoleColor.Black };
            yield return new object[] { 48, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 56, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 64, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 72, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 80, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 88, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 96, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 104, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 112, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 120, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 128, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 136, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 144, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 152, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 160, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 168, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 176, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 184, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 192, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 200, 0, 0, ConsoleColor.DarkRed };
            yield return new object[] { 208, 0, 0, ConsoleColor.Red };
            yield return new object[] { 216, 0, 0, ConsoleColor.Red };
            yield return new object[] { 224, 0, 0, ConsoleColor.Red };
            yield return new object[] { 232, 0, 0, ConsoleColor.Red };
            yield return new object[] { 240, 0, 0, ConsoleColor.Red };
            yield return new object[] { 248, 0, 0, ConsoleColor.Red };
            yield return new object[] { 255, 0, 0, ConsoleColor.Red };

            // Green shades
            yield return new object[] { 0, 24, 0, ConsoleColor.Black };
            yield return new object[] { 0, 32, 0, ConsoleColor.Black };
            yield return new object[] { 0, 40, 0, ConsoleColor.Black };
            yield return new object[] { 0, 48, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 56, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 64, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 72, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 80, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 88, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 96, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 104, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 112, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 120, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 128, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 136, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 144, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 152, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 160, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 168, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 176, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 184, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 192, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 200, 0, ConsoleColor.DarkGreen };
            yield return new object[] { 0, 208, 0, ConsoleColor.Green };
            yield return new object[] { 0, 216, 0, ConsoleColor.Green };
            yield return new object[] { 0, 224, 0, ConsoleColor.Green };
            yield return new object[] { 0, 232, 0, ConsoleColor.Green };
            yield return new object[] { 0, 240, 0, ConsoleColor.Green };
            yield return new object[] { 0, 248, 0, ConsoleColor.Green };
            yield return new object[] { 0, 255, 0, ConsoleColor.Green };

            // Blue shades
            yield return new object[] { 0, 0, 32, ConsoleColor.Black };
            yield return new object[] { 0, 0, 40, ConsoleColor.Black };
            yield return new object[] { 0, 0, 48, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 56, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 64, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 72, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 80, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 88, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 96, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 104, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 112, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 120, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 128, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 136, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 144, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 152, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 160, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 168, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 176, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 184, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 192, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 200, ConsoleColor.DarkBlue };
            yield return new object[] { 0, 0, 208, ConsoleColor.Blue };
            yield return new object[] { 0, 0, 216, ConsoleColor.Blue };
            yield return new object[] { 0, 0, 224, ConsoleColor.Blue };
            yield return new object[] { 0, 0, 232, ConsoleColor.Blue };
            yield return new object[] { 0, 0, 240, ConsoleColor.Blue };
            yield return new object[] { 0, 0, 248, ConsoleColor.Blue };
            yield return new object[] { 0, 0, 255, ConsoleColor.Blue };

            // Existing edge cases and secondary colors
            yield return new object[] { 220, 10, 10, ConsoleColor.Red };
            yield return new object[] { 180, 20, 20, ConsoleColor.DarkRed };
            yield return new object[] { 10, 220, 10, ConsoleColor.Green };
            yield return new object[] { 20, 180, 20, ConsoleColor.DarkGreen };
            yield return new object[] { 10, 10, 220, ConsoleColor.Blue };
            yield return new object[] { 20, 20, 180, ConsoleColor.DarkBlue };
            yield return new object[] { 255, 255, 0, ConsoleColor.Yellow };
            yield return new object[] { 255, 255, 10, ConsoleColor.Yellow };
            yield return new object[] { 0, 255, 255, ConsoleColor.Cyan };
            yield return new object[] { 10, 255, 255, ConsoleColor.Cyan };
            yield return new object[] { 255, 0, 255, ConsoleColor.Magenta };
            yield return new object[] { 255, 10, 255, ConsoleColor.Magenta };
            yield return new object[] { 30, 32, 28, ConsoleColor.Black };
            yield return new object[] { 100, 110, 105, ConsoleColor.DarkGray };
            yield return new object[] { 150, 150, 150, ConsoleColor.Gray };
            yield return new object[] { 220, 225, 230, ConsoleColor.White };
            yield return new object[] { 255, 255, 255, ConsoleColor.White };
            yield return new object[] { -10, 0, 0, ConsoleColor.Black };
            yield return new object[] { 0, 300, 0, ConsoleColor.Green };
        }

        /// <summary>
        /// Returns an enumerator that iterates through the test data (non-generic).
        /// </summary>
        /// <returns>An enumerator for the test data.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
