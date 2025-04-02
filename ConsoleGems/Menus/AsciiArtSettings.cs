// <copyright file="AsciiArtSettings.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Controls the characters used in ASCII art such as borders.
    /// </summary>
    public class AsciiArtSettings
    {
        /// <summary>
        /// Gets or sets the character used for a horizontal outer border.
        /// </summary>
        public char OuterBorderHorizontal { get; set; } = '═';

        /// <summary>
        /// Gets or sets the character used for a vertical outer border.
        /// </summary>
        public char OuterBorderVertical { get; set; } = '║';

        /// <summary>
        /// Gets or sets the character used for the top left corner of an outer border.
        /// </summary>
        public char OuterBorderTopLeft { get; set; } = '╔';

        /// <summary>
        /// Gets or sets the character used for the top right corner of an outer border.
        /// </summary>
        public char OuterBorderTopRight { get; set; } = '╗';

        /// <summary>
        /// Gets or sets the character used for the bottom left corner of an outer border.
        /// </summary>
        public char OuterBorderBottomLeft { get; set; } = '╚';

        /// <summary>
        /// Gets or sets the character used for the bottom right corner of an outer border.
        /// </summary>
        public char OuterBorderBottomRight { get; set; } = '╝';

        /// <summary>
        /// Gets or sets the character used for a horizontal inner border.
        /// </summary>
        public char InnerBorderHorizontal { get; set; } = '─';

        /// <summary>
        /// Gets or sets the character used for a vertical inner border.
        /// </summary>
        public char InnerBorderVertical { get; set; } = '│';

        /// <summary>
        /// Gets or sets the character used for two inner broders crossing over.
        /// </summary>
        public char InnerBorderJoin { get; set; } = '┼';

        /// <summary>
        /// Gets or sets the character used for a top inner border joining a vertical border.
        /// </summary>
        public char InnerBorderJoinTop { get; set; } = '┬';

        /// <summary>
        /// Gets or sets the character used for a bottom inner border joining a vertical border.
        /// </summary>
        public char InnerBorderJoinBottom { get; set; } = '┴';

        /// <summary>
        /// Gets or sets the character used for a left outer border joining an inner border.
        /// </summary>
        public char OuterInnerJoinLeft { get; set; } = '╟';

        /// <summary>
        /// Gets or sets the character used for a right outer border joining an inner border.
        /// </summary>
        public char OuterInnerJoinRight { get; set; } = '╢';

        /// <summary>
        /// Gets or sets the character used for a top outer border joining an inner border.
        /// </summary>
        public char OuterInnerJoinTop { get; set; } = '╤';

        /// <summary>
        /// Gets or sets the character used for a bottom outer border joining an inner border.
        /// </summary>
        public char OuterInnerJoinBottom { get; set; } = '╧';

        /// <summary>
        /// Gets or sets the character used for light shading.
        /// </summary>
        public char LightShade { get; set; } = '░';

        /// <summary>
        /// Gets or sets the character used for medium shading.
        /// </summary>
        public char MediumShade { get; set; } = '▒';

        /// <summary>
        /// Gets or sets the character used for dark shading.
        /// </summary>
        public char DarkShade { get; set; } = '▓';
    }
}
