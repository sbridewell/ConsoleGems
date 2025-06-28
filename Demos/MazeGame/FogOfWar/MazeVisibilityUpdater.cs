// <copyright file="MazeVisibilityUpdater.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.FogOfWar
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    // TODO: IMazeVisibilityUpdater? Relates to moving methods out of the Maze model class

    /// <summary>
    /// Sets the Explored property of the points in the maze which are
    /// visible to the player, taking into account whether or not there
    /// is a wall in the way.
    /// </summary>
    /// <param name="limitOfViewProvider">
    /// Provides the points on the limit of the player's field of view,
    /// without taking into account whether or not there is a wall in the
    /// way.
    /// </param>
    public class MazeVisibilityUpdater(ILimitOfViewProvider limitOfViewProvider)
    {
        /// <summary>
        /// Checks whether the player can now see any points in the maze which
        /// they couldn't previously see.
        /// </summary>
        /// <param name="maze">The maze that the player is in.</param>
        /// <param name="player">The player.</param>
        /// <returns>
        /// True if a repaint is required because points which were not previously
        /// visible are now visible.
        /// </returns>
        [SuppressMessage(
            "Minor Code Smell",
            "S3267:Loops should be simplified with \"LINQ\" expressions",
            Justification = "Not really a simplification")]
        public bool UpdateVisibility(Maze maze, Player player)
        {
            var repaintRequired = false;
            foreach (var pointOnLimit in limitOfViewProvider.LimitOfView)
            {
                var direction = pointOnLimit.Direction;
                var visibleDistance = limitOfViewProvider.VisibleDistance;
                double dx = 0;
                double dy = 0;
                for (var i = 0; i <= visibleDistance; i++)
                {
                    dx += direction.dx;
                    dy += direction.dy;
                    var xToCheck = (int)Math.Round((double)player.Position.X + dx);
                    var yToCheck = (int)Math.Round((double)player.Position.Y + dy);
                    var pointToCheck = new ConsolePoint(xToCheck, yToCheck);
                    if (maze.PositionIsWithinMaze(pointToCheck))
                    {
                        var mazePoint = maze.GetMazePoint(pointToCheck);
                        if (!mazePoint.Explored)
                        {
                            maze.GetMazePoint(pointToCheck).Explored = true;
                            repaintRequired = true;
                        }

                        if (mazePoint.PointType == MazePointType.Wall)
                        {
                            // Stop if we hit a wall
                            break;
                        }
                    }
                }
            }

            return repaintRequired;
        }
    }
}
