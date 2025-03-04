// <copyright file="IPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Prompters
{
    /// <summary>
    /// Interface for prompting the user to enter a value into the console window.
    /// </summary>
    /// <typeparam name="T">The type of the value to enter.</typeparam>
    public interface IPrompter<T>
    {
        /// <summary>
        /// Prompts the user to enter a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="defaultValue">
        /// Optional default value to use if the user enters an empty string.
        /// </param>
        /// <returns>The entered value, or the default value if applicable.</returns>
        public T Prompt(string prompt, T? defaultValue = default);
    }
}
