// <copyright file="SimpleCellMapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CellMappers
{
    using Sde.AsciiArt.CharacterBlenders;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Models;

    /// <summary>
    /// Simple implementation of IAsciiArtCellMapper that reuses existing colour mappers and character blenders.
    /// Foreground and background are mapped using two IConsoleColourMapper instances, and the character is chosen
    /// using an ICharacterBlender based on a grayscale ratio.
    /// </summary>
    public class SimpleCellMapper : IAsciiArtCellMapper
    {
        private readonly ICharacterBlender characterBlender;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCellMapper"/> class.
        /// </summary>
        /// <param name="foregroundMapper">The mapper for the foreground colour.</param>
        /// <param name="backgroundMapper">The mapper for the background colour.</param>
        /// <param name="characterBlender">The character blender.</param>
        public SimpleCellMapper(
            IConsoleColourMapper foregroundMapper,
            IConsoleColourMapper backgroundMapper,
            ICharacterBlender characterBlender)
        {
            this.characterBlender = characterBlender;
        }

        /// <summary>
        /// Gets the character blender used by this cell mapper.
        /// </summary>
        public ICharacterBlender CharacterBlender => this.characterBlender;

        /// <inheritdoc/>
        public AsciiArtCell Map(Pixel pixel)
        {
            // Try all combinations of foreground/background colours and characters to best approximate the pixel colour.
            var consoleColors = Enum.GetValues<ConsoleColor>();
            double minDist = double.MaxValue;
            AsciiArtCell best = new(' ', ConsoleColor.Black, ConsoleColor.Black);

            // Try 5 character ratios (using the character blender)
            for (int i = 0; i <= 4; i++)
            {
                double ratio = i / 4.0; // 0.0, 0.25, 0.5, 0.75, 1.0
                char ch = this.characterBlender.GetCharacterForRatio(ratio);
                foreach (var fg in consoleColors)
                {
                    foreach (var bg in consoleColors)
                    {
                        var fgRgb = GetConsoleColorRgb(fg);
                        var bgRgb = GetConsoleColorRgb(bg);
                        int blendedR = (int)Math.Round((ratio * fgRgb.R) + ((1 - ratio) * bgRgb.R));
                        int blendedG = (int)Math.Round((ratio * fgRgb.G) + ((1 - ratio) * bgRgb.G));
                        int blendedB = (int)Math.Round((ratio * fgRgb.B) + ((1 - ratio) * bgRgb.B));
                        double dist = Math.Pow(pixel.Red - blendedR, 2) + Math.Pow(pixel.Green - blendedG, 2) + Math.Pow(pixel.Blue - blendedB, 2);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            best = new AsciiArtCell(ch, fg, bg);
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
