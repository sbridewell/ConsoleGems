// <copyright file="PainterOrchestrator.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    public class PainterOrchestrator : IPainterOrchestrator
    {
        /// <inheritdoc/>
        public List<IPainter> Painters { get; } = new ();

        /// <inheritdoc/>
        public void Paint()
        {
            foreach (var painter in this.Painters)
            {
                painter.Paint();
            }
        }
    }
}
