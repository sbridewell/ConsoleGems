// <copyright file="IPainterOrchestrator.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Interface for something which orchestrates multiple painters to
    /// position content within a console window.
    /// </summary>
    public interface IPainterOrchestrator
    {
        /// <summary>
        /// Gets the <see cref="IPainter"/> implementations orchestrated
        /// by this orchestrator.
        /// </summary>
        public List<IPainter> Painters { get; }

        /// <summary>
        /// Paints the entire console window by clearing it and then calling
        /// the Paint method of each of the painters.
        /// </summary>
        public void Paint();
    }
}
