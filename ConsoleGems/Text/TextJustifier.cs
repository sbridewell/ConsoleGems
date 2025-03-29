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
        private string? text;
        private TextJustification justification;
        private int availableWidth;
        private char paddingCharacter;
        private Lazy<List<StringBuilder>>? unjustifiedLines;
        private Lazy<List<string>>? justifiedLines;
        private Lazy<string>? justifiedText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextJustifier"/> class.
        /// </summary>
        public TextJustifier()
        {
            this.Initialise();
        }

        /// <inheritdoc/>
        public string JustifiedText => this.justifiedText!.Value;

        /// <inheritdoc/>
        public List<string> JustifiedLines => this.justifiedLines!.Value;

        // TODO: JustifiedTextBlock property, lazy initialised

        private List<StringBuilder> UnjustifiedLines => this.unjustifiedLines!.Value;

        /// <inheritdoc/>
        public void Justify(
            string text,
            TextJustification justification,
            int availableWidth,
            char paddingCharacter = ' ')
        {
            ArgumentNullException.ThrowIfNull(text);
            ValidateJustification(justification);
            this.Initialise();
            this.text = text.Trim();
            this.justification = justification;
            this.availableWidth = availableWidth;
            this.paddingCharacter = paddingCharacter;
        }

        private static void ValidateJustification(TextJustification justification)
        {
            if (!Enum.IsDefined(typeof(TextJustification), justification))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(justification),
                    justification,
                    $"The supplied value is not a member of the {nameof(TextJustification)} enum.");
            }
        }

        private void Initialise()
        {
            this.text = string.Empty;
            this.unjustifiedLines = new Lazy<List<StringBuilder>>(() => this.BuildLines());
            this.justifiedText = new Lazy<string>(() => this.BuildJustifiedText());
            this.justifiedLines = new Lazy<List<string>>(() => this.BuildJustifiedLines());
        }

        private List<StringBuilder> BuildLines()
        {
            var sb = new StringBuilder();
            var lines = new List<StringBuilder>();
            var words = this.text!.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (sb.Length + word.Length > this.availableWidth)
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

        private List<string> BuildJustifiedLines()
        {
            var justifiedLinesInternal = new List<string>();
            foreach (var lineSB in this.UnjustifiedLines)
            {
                var line = lineSB.ToString().Trim();
                var justified = this.justification switch
                {
                    TextJustification.Left => line.PadRight(this.availableWidth, this.paddingCharacter),
                    TextJustification.Centre => line
                        .PadLeft((this.availableWidth + line.Length) / 2, this.paddingCharacter)
                        .PadRight(this.availableWidth, this.paddingCharacter),
                    TextJustification.Right => line.PadLeft(this.availableWidth, this.paddingCharacter),
                    _ => line, // no justification
                };

                justifiedLinesInternal.Add(justified);
            }

            return justifiedLinesInternal;
        }

        private string BuildJustifiedText()
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

            return justifiedTextSB.ToString();
        }
    }
}
