// <copyright file="MazePainterPovConstants.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    /// <summary>
    /// Constants used in painting the maze from the player's point of view in 3D.
    /// </summary>
    public static class MazePainterPovConstants
    {
        /// <summary>
        /// Height of the painter used to display the player's field of view in 3D.
        /// </summary>
        public static readonly int PainterInnerHeight = 24;

        /// <summary>
        /// Width of the painter used to display the player's field of view in 3D.
        /// </summary>
        public static readonly int PainterInnerWidth = 24;

        // TODO: make these characters configurable

        /// <summary>
        /// The character to use to represent the ceiling of the maze.
        /// </summary>
        public static readonly char CeilingChar = '●';

        /// <summary>
        /// The character to use to represent the floor of the maze.
        /// </summary>
        public static readonly char FloorChar = '○';

        /// <summary>
        /// The character to use to represent a wall which runs perpendicular to
        /// the direction the player is facing.
        /// </summary>
        public static readonly char PerpendicularWallChar = '▓';

        /// <summary>
        /// The character to use to represent a wall which runs parallel to the
        /// direction the player is facing.
        /// </summary>
        public static readonly char ParallelWallChar = '░';

        /// <summary>
        /// Gets the width of each of the sections of the painter.
        /// </summary>
        public static readonly int[] SectionWidths =
        [1, 4, 3, 2, 1, 1, 1]; // TODO: sections 0-6 although we only actually have sections 0-5

        /// <summary>
        /// Gets the left indent at which each section of the painter starts.
        /// </summary>
        public static readonly int[] SectionIndents =
        [0, 1, 5, 8, 10, 11, 12, 11];
    }
}
