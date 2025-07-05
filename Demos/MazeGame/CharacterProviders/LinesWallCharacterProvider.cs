// <copyright file="LinesWallCharacterProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.CharacterProviders
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// This implementation returns different characters depending on whether
    /// the positions adjacent to the supplied position are also walls, providing
    /// a line representation of the maze.
    /// </summary>
    public class LinesWallCharacterProvider : IWallCharacterProvider
    {
        private readonly char downChar = '╷';
        private readonly char upChar = '╵';
        private readonly char leftChar = '╴';
        private readonly char rightChar = '╶';
        private readonly char verticalChar = '│';
        private readonly char horizontalChar = '─';
        private readonly char topLeftChar = '╭';
        private readonly char topRightChar = '╮';
        private readonly char bottomLeftChar = '╰';
        private readonly char bottomRightChar = '╯';
        private readonly char verticalAndRightChar = '├';
        private readonly char verticalAndLeftChar = '┤';
        private readonly char verticalAndHorizontalChar = '┼';
        private readonly char downAndHorizontalChar = '┬';
        private readonly char upAndHorizontalChar = '┴';
        private readonly char squareChar = '□';

        /// <inheritdoc/>
        public char GetWallChar(Maze maze, ConsolePoint position)
        {
            var wallAbove = maze.ThereIsAWallToTheNorthOf(position);
            var wallBelow = maze.ThereIsAWallToTheSouthOf(position);
            var wallLeft = maze.ThereIsAWallToTheWestOf(position);
            var wallRight = maze.ThereIsAWallToTheEastOf(position);

            if (wallAbove)
            {
                if (wallBelow)
                {
                    if (wallLeft)
                    {
                        return wallRight ? this.verticalAndHorizontalChar : this.verticalAndLeftChar;
                    }
                    else
                    {
                        return wallRight ? this.verticalAndRightChar : this.verticalChar;
                    }
                }
                else
                {
                    if (wallLeft)
                    {
                        return wallRight ? this.upAndHorizontalChar : this.bottomRightChar;
                    }
                    else
                    {
                        return wallRight ? this.bottomLeftChar : this.upChar;
                    }
                }
            }
            else
            {
                if (wallBelow)
                {
                    if (wallLeft)
                    {
                        return wallRight ? this.downAndHorizontalChar : this.topRightChar;
                    }
                    else
                    {
                        return wallRight ? this.topLeftChar : this.downChar;
                    }
                }
                else
                {
                    if (wallLeft)
                    {
                        return wallRight ? this.horizontalChar : this.leftChar;
                    }
                    else
                    {
                        return wallRight ? this.rightChar : this.squareChar;
                    }
                }
            }
        }
    }
}
