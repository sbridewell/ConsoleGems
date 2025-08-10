// <copyright file="BlendingCellMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CellMappers
{
    using System;
    using System.Linq;
    using Sde.AsciiArt.CharacterBlenders;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Cell mapper that finds the best combination of foreground, background, and character to approximate the pixel colour.
    /// </summary>
    public class BlendingCellMapper : IAsciiArtCellMapper
    {
        private static readonly (ConsoleColor Color, (int R, int G, int B) Rgb)[] ConsoleColors =
            Enum.GetValues<ConsoleColor>()
                .Select(c => (c, GetConsoleColorRgb(c)))
                .ToArray();

        private readonly ICharacterBlender characterBlender;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlendingCellMapper"/> class.
        /// </summary>
        /// <param name="characterBlender">The character blender to use for blending.</param>
        public BlendingCellMapper(ICharacterBlender characterBlender)
        {
            this.characterBlender = characterBlender;
        }

        /// <inheritdoc/>
        public AsciiArtCell Map(Pixel pixel)
        {
            var target = (R: pixel.Red, G: pixel.Green, B: pixel.Blue);
            double minDist = double.MaxValue;
            AsciiArtCell best = new(' ', ConsoleColor.Black, ConsoleColor.Black);

            foreach (var (fgColor, fgRgb) in ConsoleColors)
            {
                foreach (var (bgColor, bgRgb) in ConsoleColors)
                {
                    for (double ratio = 0.0; ratio <= 1.0; ratio += 0.05)
                    {
                        int blendedR = (int)Math.Round((ratio * fgRgb.R) + ((1 - ratio) * bgRgb.R));
                        int blendedG = (int)Math.Round((ratio * fgRgb.G) + ((1 - ratio) * bgRgb.G));
                        int blendedB = (int)Math.Round((ratio * fgRgb.B) + ((1 - ratio) * bgRgb.B));
                        double dist = Math.Pow(target.R - blendedR, 2) + Math.Pow(target.G - blendedG, 2) + Math.Pow(target.B - blendedB, 2);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            char ch = this.characterBlender.GetCharacterForRatio(ratio);
                            best = new AsciiArtCell(ch, fgColor, bgColor);
                        }
                    }
                }
            }

            return best;
        }

        private static (int R, int G, int B) GetConsoleColorRgb(ConsoleColor color)
        {
            // Standard Windows console palette
            return color switch
            {
                ConsoleColor.Black => (0, 0, 0),
                ConsoleColor.DarkBlue => (0, 0, 128),
                ConsoleColor.DarkGreen => (0, 128, 0),
                ConsoleColor.DarkCyan => (0, 128, 128),
                ConsoleColor.DarkRed => (128, 0, 0),
                ConsoleColor.DarkMagenta => (128, 0, 128),
                ConsoleColor.DarkYellow => (128, 128, 0),
                ConsoleColor.Gray => (192, 192, 192),
                ConsoleColor.DarkGray => (128, 128, 128),
                ConsoleColor.Blue => (0, 0, 255),
                ConsoleColor.Green => (0, 255, 0),
                ConsoleColor.Cyan => (0, 255, 255),
                ConsoleColor.Red => (255, 0, 0),
                ConsoleColor.Magenta => (255, 0, 255),
                ConsoleColor.Yellow => (255, 255, 0),
                ConsoleColor.White => (255, 255, 255),
                _ => (0, 0, 0),
            };
        }
    }
}
