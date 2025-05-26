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
        /// Paints a top border, if the painter requires a border and the border
        /// hasn't already been painted.
        /// </summary>
        void PaintTopBorderIfRequired();

        /// <summary>
        /// Paints a line of left or right border, if the painter requires
        /// a border and the border hasn't already been painted.
        /// </summary>
        void PaintSideBorderIfRequired();

        /// <summary>
        /// Paints the bottom border, if the painter requires a border and
        /// the border hasn't already been painted.
        /// Also sets a flag to indicate that the border has been painted
        /// now, so we don't need to paint it again.
        /// </summary>
        void PaintBottomBorderIfRequired();
    }
}
