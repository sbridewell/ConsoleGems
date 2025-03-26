// <copyright file="TextJustifier.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
{
    /// <summary>
    /// <see cref="ITextJustifier"/> implementation.
    /// </summary>
    public class TextJustifier : ITextJustifier
    {
        /// <inheritdoc/>
        public string JustifiedText { get; private set; } = string.Empty;

        /// <inheritdoc/>
        public List<string> JustifiedLines { get; private set; } = new ();

        /// <inheritdoc/>
        public void Justify(
            string text,
            TextJustification justification,
            int availableWidth,
            char paddingCharacter = ' ')
        {
            ArgumentNullException.ThrowIfNull(text);
            this.JustifiedText = string.Empty;
            this.JustifiedLines.Clear();
            text = text.Trim();
            var lines = BuildLines(text, availableWidth);
            this.BuildJustifiedLines(lines, justification, availableWidth, paddingCharacter);
            this.BuildJustifiedText();
        }

        private static List<StringBuilder> BuildLines(string text, int availableWidth)
        {
            var sb = new StringBuilder();
            var lines = new List<StringBuilder>();
            var words = text.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (sb.Length + word.Length > availableWidth)
                {
                    lines.Add(sb);
                    sb = new StringBuilder();
                }

                sb.Append(word);
                sb.Append(' '); // will be trimmed during justification if necessary
            }

            lines.Add(sb);
            return lines;
        }

        private void BuildJustifiedLines(
            List<StringBuilder> lines,
            TextJustification justification,
            int availableWidth,
            char paddingCharacter)
        {
            foreach (var lineSB in lines)
            {
                var line = lineSB.ToString().Trim();
                var justified = justification switch
                {
                    TextJustification.None => line,
                    TextJustification.Left => line.PadRight(availableWidth, paddingCharacter),
                    TextJustification.Centre => line
                        .PadLeft((availableWidth + line.Length) / 2, paddingCharacter)
                        .PadRight(availableWidth, paddingCharacter),
                    TextJustification.Right => line.PadLeft(availableWidth, paddingCharacter),
                    _ => throw new ArgumentOutOfRangeException(
                        nameof(justification),
                        justification,
                        $"The supplied value is not a member of the {nameof(TextJustification)} enum."),
                };

                this.JustifiedLines.Add(justified);
            }
        }

        private void BuildJustifiedText()
        {
            var justifiedTextSB = new StringBuilder();
            for (var i = 0; i < this.JustifiedLines.Count; i++)
            {
                if (i < this.JustifiedLines.Count - 1)
                {
                    justifiedTextSB.AppendLine(this.JustifiedLines[i]);
                }
                else
                {
                    justifiedTextSB.Append(this.JustifiedLines[i]);
                }
            }

            this.JustifiedText = justifiedTextSB.ToString();
        }
    }
}
