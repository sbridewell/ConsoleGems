// <copyright file="IMazePainter3D.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    public interface IMazePainter3D
    {
        public void SetOrigin(ConsolePoint origin);
        public void Render(Maze maze, Player player);
    }
}
