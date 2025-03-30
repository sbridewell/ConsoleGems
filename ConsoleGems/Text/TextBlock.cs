// <copyright file="TextBlock.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
{
    /// <summary>
    /// Represents a rectangular block of text.
    /// </summary>
    /// <param name="width">Width of the text block in characters.</param>
    public class TextBlock(int width)
    {
        /// <summary>
        /// Represents the text in the block.
        /// </summary>
        private readonly List<string> lines = new ();

        /// <summary>
        /// Gets the width of the text block in characters.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height of the text block in characters.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Inserts the supplied text into the current <see cref="TextBlock"/>
        /// starting at the top-left corner, and allowing the text to flow
        /// and wrap, using using the full width of the <see cref="TextBlock"/>.
        /// </summary>
        /// <param name="text">The text to insert.</param>
        public void InsertText(string text)
        {
            var linesToInsert = text.Chunk(width).ToList();
            for (var i = 0; i < linesToInsert.Count; i++)
            {
                if (i < linesToInsert.Count - 1)
                {
                    this.lines.Add(new string(linesToInsert[i]));
                }
                else
                {
                    this.lines.Add(new string(linesToInsert[i]).PadRight(width));
                }
            }

            if (this.lines.Count > this.Height)
            {
                this.Height = this.lines.Count;
            }
        }

        /// <summary>
        /// Inserts the supplied <see cref="TextBlock"/> into the current
        /// <see cref="TextBlock"/> at the specified position.
        /// </summary>
        /// <param name="block">The <see cref="TextBlock"/> to insert.</param>
        /// <param name="insertAt">
        /// The zero-based position to insert at.
        /// </param>
        public void InsertBlock(TextBlock block, ConsolePoint insertAt)
        {
            if (insertAt.X < 0 || insertAt.Y < 0)
            {
                var msg = "The co-ordinates must be non-negative. "
                    + $"The co-ordinates you supplied are {insertAt.X},{insertAt.Y}.";
                throw new ArgumentOutOfRangeException(nameof(insertAt), msg);
            }

            if (insertAt.X + block.Width > this.Width)
            {
                var msg = $"Block with width {block.Width} "
                    + "is too wide to insert at horizontal position "
                    + $"{insertAt.X} of a block with width {this.Width}.";
                throw new ArgumentException(msg, nameof(block));
            }

            while (insertAt.Y + block.Height > this.Height)
            {
                this.lines.Add(new string(' ', this.Width));
                this.Height++;
            }

            for (var i = 0; i < block.lines.Count; i++)
            {
                var targetLineIndex = insertAt.Y + i;
                var blockLine = block.lines[i];

                var targetLineChars = this.lines[targetLineIndex].ToCharArray();
                for (int j = 0; j < blockLine.Length; j++)
                {
                    targetLineChars[insertAt.X + j] = blockLine[j];
                }

                this.lines[targetLineIndex] = new string(targetLineChars);
            }
        }

        /// <summary>
        /// Returns the text in the block.
        /// </summary>
        /// <returns>The text in the block.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.Height < 1)
            {
                return string.Empty;
            }

            foreach (var line in this.lines.Take(this.lines.Count - 1))
            {
                sb.AppendLine(line);
            }

            sb.Append(this.lines.Last());
            return sb.ToString();
        }
    }
}
