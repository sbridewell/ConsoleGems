// <copyright file="RayTracer.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.RayTracing
{
    using Sde.MazeGame.Models;

    /// <summary>
    /// Class for determining whether a position within a two-dimensional
    /// space is visible to a player.
    /// </summary>
    public class RayTracer
    {
        /// <inheritdoc/>
        public Ray Trace(Player player, Maze maze, float visibleDistance, float rayAngle)
        {
            var rayDistance = 0.0f;
            var stepSize = 0.01f;
            var playerX = player.Position.X;
            var playerY = player.Position.Y;
            float eyeX = (float)Math.Cos(rayAngle);
            float eyeY = (float)Math.Sin(rayAngle);
            var outcome = new Ray { Direction = rayAngle };
            while (!outcome.HasHitAWall && rayDistance < visibleDistance)
            {
                rayDistance += stepSize;
                var testX = (int)Math.Round(playerX + (eyeX * rayDistance));
                var testY = (int)Math.Round(playerY + (eyeY * rayDistance));
                if (!maze.PositionIsWithinMaze(testX, testY))
                {
                    // TODO: if the test position is outside the maze then we can see the exit
                    outcome.HasHitAWall = true;
                    outcome.Distance = rayDistance;
                    return outcome;
                }
                else
                {
                    if (maze.GetMazePoint(testX, testY).PointType == MazePointType.Wall)
                    {
                        outcome.HasHitAWall = true;
                        outcome.Distance = rayDistance;
                    }
                }
            }

            outcome.Distance = rayDistance;
            return outcome;
        }
    }
}
