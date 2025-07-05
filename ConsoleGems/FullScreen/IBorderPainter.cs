// <copyright file="IBorderPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Interface for painting a border around an <see cref="IPainter"/>.
    /// </summary>
    public interface IBorderPainter
    {
        /// <summary>
        /// Gets or sets the <see cref="IPainter"/> to draw the border around.
        /// </summary>
        IPainter? Painter { get; set; }

        /// <summary>
        /// Paints a border around the painter, if it should have a border
        /// and the border has not already been painter.
        /// </summary>
        void PaintBorderIfRequired();

        /// <summary>
        /// Resets the flag which tracks whether the border has already been painted.
        /// </summary>
        void Reset();
    }
}
