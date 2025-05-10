// <copyright file="Maze3DCharProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.RayTracing
{
    public class Maze3DCharProvider(float visibleDistance)
    {
        //private readonly float visibleDistance;
        //private readonly float nearWall = this.visibleDisistance / 3.00f;

        //public Maze3DCharProvider(float visibleDistance)
        //{
        //    this.visibleDistance = visibleDistance;
        //}

        //public char GetChar(Ray ray, Maze3DCharType charType)
        //{
        //    return charType switch
        //    {
        //        Maze3DCharType.Wall => this.GetWallChar(ray),
        //        Maze3DCharType.Ceiling => this.GetCeilingChar(ray),
        //        Maze3DCharType.Floor => this.GetFloorChar(ray),
        //        Maze3DCharType.Exit => this.GetExitChar(ray),
        //        _ => throw new ArgumentOutOfRangeException(nameof(charType), charType, null)
        //    };
        //}

        public char GetWallChar(Ray ray)
        {
            // TODO: change Ray parameter to float distance?
            //if (ray.Distance < visibleDistance / 3.00f)
            //{
            //    return '║';
            //}
            //else if (ray.Distance < visibleDistance / 1.75f)
            //{
            //    return '┃';
            //}
            //else if (ray.Distance < visibleDistance / 1.00f)
            //{
            //    return '│';
            //}
            //else
            //{
            //    return ' '; // too far away to be visible
            //}
            var approximageDistance = this.ApproximateDistance(ray.Distance, visibleDistance);
            return approximageDistance switch
            {
                DistanceApproximation.Close => '║',
                DistanceApproximation.Medium => '┃',
                DistanceApproximation.Far => '│',
                _ => ' ', // too far away to be visible
            };
        }

        public char GetCeilingChar(int screenY, int screenHeight)
        {
            float b = 1.0f + (screenY - (screenHeight / 2.0f)) / (screenHeight / 2.0f);
            return b switch
            {
                > 0.40f => '─',
                > 0.20f => '━',
                _ => '═',
                //_ => '?',
            };
        }

        public char GetFloorChar(Ray ray, int screenY, int screenHeight)
        {
            float b
                = 1.0f
                - ((screenY - (screenHeight / 2.0f)) / (screenHeight / 2.0f));
            return b switch
            {
                < 0.20f => '═',
                < 0.40f => '━',
                _ => '─',
                //_ => '?',
            };
        }

        public char GetCornerChar(Ray ray)
        {
            if (ray.Distance >= visibleDistance)
            {
                return ' ';
            }

            return 'C';
        }

        private char GetExitChar(Ray ray)
        {
            throw new InvalidOperationException();
        }

        private DistanceApproximation ApproximateDistance(float distance, float visibleDistance)
        {
            return (distance / visibleDistance) switch
            {
                < 0.20f => DistanceApproximation.Close,
                < 0.40f => DistanceApproximation.Medium,
                < 0.60f => DistanceApproximation.Far,
                _ => DistanceApproximation.Invisible,
            };
        }
    }
}
