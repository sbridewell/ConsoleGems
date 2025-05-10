// <copyright file="RayTracingDebugPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.RayTracing
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;

    public class RayTracingDebugPainter
    {
        private readonly ConsolePoint origin = new ConsolePoint(60, 1);

        public void Paint(IConsole console, List<Ray> rays)
        {
            console.CursorLeft = this.origin.X;
            console.CursorTop = this.origin.Y;
            foreach (var ray in rays)
            {
                console.Write($"Ray: {ray.ToString()}");
                console.CursorTop++;
                console.CursorLeft = this.origin.X;
            }
        }
    }
}
