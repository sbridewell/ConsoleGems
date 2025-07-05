// <copyright file="ForwardView.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Pov
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Representing the current field of view of the player, consisting of the
    /// positions directly ahead of the player up to and including the first wall,
    /// plus the positions to the left and right of the player, also up to the
    /// same distance.
    /// </summary>
    public class ForwardView
    {
        private readonly List<MazePointType> leftRow = new ();
        private readonly List<MazePointType> middleRow = new ();
        private readonly List<MazePointType> rightRow = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardView"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="player">The player.</param>
        /// <param name="visibleDistance">The maximum distance that the player can see.</param>
        public ForwardView(Maze maze, Player player, int visibleDistance)
        {
            var mazeX = player.Position.X;
            var mazeY = player.Position.Y;
            while (mazeX >= 0 && mazeX < maze.Width && mazeY >= 0 && mazeY < maze.Height && this.VisibleDistance < visibleDistance)
            {
                this.AddNextPoints(maze, player.FacingDirection, ref mazeX, ref mazeY);
                if (maze.GetMazePoint(mazeX, mazeY).PointType == MazePointType.Wall)
                {
                    this.AddNextPoints(maze, player.FacingDirection, ref mazeX, ref mazeY);
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the row of maze points which is one position to the left of the player
        /// and stretches forward to the current limit of visibility.
        /// </summary>
        public IReadOnlyList<MazePointType> LeftRow => this.leftRow;

        /// <summary>
        /// Gets the row of maze points which is in line with the player and stretches
        /// forward to the current limit of visibility, including the player's current
        /// position.
        /// </summary>
        public IReadOnlyList<MazePointType> MiddleRow => this.middleRow;

        /// <summary>
        /// Gets the row of maze points which is one position to the right of the player
        /// and stretches forward to the current limit of visibility.
        /// </summary>
        public IReadOnlyList<MazePointType> RightRow => this.rightRow;

        /// <summary>
        /// Gets how far the player can currently see.
        /// </summary>
        public int VisibleDistance { get; private set; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Player's position is at the bottom of this ForwardView");
            for (var i = this.VisibleDistance - 1; i >= 0; i--)
            {
                sb.AppendLine(
                    (this.leftRow[i] == MazePointType.Path ? " " : "#") +
                    (this.middleRow[i] == MazePointType.Path ? " " : "#") +
                    (this.rightRow[i] == MazePointType.Path ? " " : "#"));
            }

            return sb.ToString();
        }

        private void AddNextPoints(Maze maze, Direction playerDirection, ref int mazeX, ref int mazeY)
        {
            this.VisibleDistance++;
            switch (playerDirection)
            {
                case Direction.North:
                    this.leftRow.Add(maze.GetMazePoint(mazeX - 1, mazeY).PointType);
                    this.rightRow.Add(maze.GetMazePoint(mazeX + 1, mazeY).PointType);
                    this.middleRow.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                    mazeY--;
                    break;

                case Direction.East:
                    this.leftRow.Add(maze.GetMazePoint(mazeX, mazeY - 1).PointType);
                    this.rightRow.Add(maze.GetMazePoint(mazeX, mazeY + 1).PointType);
                    this.middleRow.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                    mazeX++;
                    break;

                case Direction.South:
                    this.leftRow.Add(maze.GetMazePoint(mazeX + 1, mazeY).PointType);
                    this.rightRow.Add(maze.GetMazePoint(mazeX - 1, mazeY).PointType);
                    this.middleRow.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                    mazeY++;
                    break;

                case Direction.West:
                    this.leftRow.Add(maze.GetMazePoint(mazeX, mazeY + 1).PointType);
                    this.rightRow.Add(maze.GetMazePoint(mazeX, mazeY - 1).PointType);
                    this.middleRow.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                    mazeX--;
                    break;
            }
        }
    }
}
