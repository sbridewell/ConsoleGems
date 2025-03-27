// <copyright file="ITextJustifier.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
{
    /// <summary>
    /// Interface for a class which justifies text and wraps it onto
    /// multiple lines if required.
    /// </summary>
    public interface ITextJustifier
    {
        /// <summary>
        /// Gets the justified text as a single string, with lines separated
        /// by <see cref="Environment.NewLine"/> characters.
        /// </summary>
        public string JustifiedText { get; }

        /// <summary>
        /// Gets the justified text as a list of strings.
        /// </summary>
        public List<string> JustifiedLines { get; }

        /// <summary>
        /// Justifies the supplied text and stores the result in the
        /// <see cref="JustifiedText"/> and <see cref="JustifiedLines"/>
        /// properties.
        /// </summary>
        /// <param name="text">The text to justify.</param>
        /// <param name="justification">The justification to apply.</param>
        /// <param name="availableWidth">
        /// The available width.
        /// If the text is longer than this width then it will wrap onto
        /// multiple lines.
        /// </param>
        /// <param name="paddingCharacter">
        /// The character used to left- or right-pad the text.
        /// Defaults to space.
        /// </param>
        public void Justify(
            string text,
            TextJustification justification,
            int availableWidth,
            char paddingCharacter = ' ');
    }
}
