// <copyright file="IPainter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.FullScreen
{
    /// <summary>
    /// Interface for classes which take responsibility for writing content
    /// to a rectangular area of the console window.
    /// </summary>
    public interface IPainter
    {
        /// <summary>
        /// Gets the position of the area of the console window that the
        /// painter is responsible for.
        /// </summary>
        public ConsolePoint Position { get; }

        /// <summary>
        /// Gets the size of the area of the console window that the painter
        /// is responsible for, excluding any space taken up by a border.
        /// </summary>
        public ConsoleSize InnerSize { get; }

        /// <summary>
        /// Gets the size of the area of the console window that the painter
        /// is responsible for, including any space taken up by a border.
        /// This is the amount of space that the painter actually takes up
        /// in the console window.
        /// </summary>
        public ConsoleSize OuterSize { get; }

        /// <summary>
        /// Gets the screen buffer for the area of the console window that
        /// the painter is responsible for.
        /// </summary>
        public IReadOnlyList<string> ScreenBuffer { get; }

        /// <summary>
        /// Paints the area of the console window that this painter is responsible
        /// for from its <see cref="ScreenBuffer"/> property.
        /// Concrete implementations are expected to have already populated the
        /// <see cref="ScreenBuffer"/> before this method is called.
        /// </summary>
        public void Paint();
    }
}
