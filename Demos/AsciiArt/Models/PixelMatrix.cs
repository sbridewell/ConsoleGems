// <copyright file="PixelMatrix.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Encapsulates a 2D matrix of <see cref="Pixel"/> values and provides safe accessors.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PixelMatrix(List<List<Pixel>> rows)
    {
        /// <summary>
        /// Gets the width of the matrix.
        /// </summary>
        public int Width => rows.Count == 0 ? 0 : rows[0].Count;

        /// <summary>
        /// Gets the height of the matrix.
        /// </summary>
        public int Height => rows.Count;

        /// <summary>
        /// Gets the pixel at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The pixel at the specified coordinates.</returns>
        public Pixel GetPixel(int x, int y) => rows[y][x];

        /// <summary>
        /// Gets the row at the specified index.
        /// </summary>
        /// <param name="y">The row index.</param>
        /// <returns>The row of pixels at the specified index.</returns>
        public IReadOnlyList<Pixel> GetRow(int y) => rows[y];
    }
}
