// <copyright file="BooleanPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Prompters
{
    /// <summary>
    /// Class for prompting the user to enter a boolean value into the console window.
    /// </summary>
    public class BooleanPrompter(
        IConsoleErrorWriter consoleErrorWriter,
        IAutoCompleter autoCompleter,
        IConsole console)
        : IBooleanPrompter
    {
        /// <inheritdoc/>
        public bool? Prompt(string prompt, bool? defaultValue = null)
        {
            // TODO: make validationMessage an optional parameter?
            var validationMessage = defaultValue == null
                ? "{0} is not a valid value. Please enter true or false."
                : "{0} is not a valid value. Please enter true, false or an empty string.";
            var suggestions = new List<string> { "true", "false", string.Empty };
            bool? returnValue = null;
            while (true)
            {
                var userInput = autoCompleter.ReadLine(suggestions, prompt);
                if (string.IsNullOrEmpty(userInput) && defaultValue != null)
                {
                    returnValue = defaultValue;
                    break;
                }

                if (bool.TryParse(userInput, out var result))
                {
                    returnValue = result;
                    break;
                }

                consoleErrorWriter.WriteError(string.Format(validationMessage, userInput));
            }

            console.WriteLine($"Returning {returnValue.Value}");
            return returnValue.Value;
        }
    }

#pragma warning disable SA1201 // Elements should appear in the correct order
    /// <summary>
    /// Interface for prompting the user to enter a boolean value into
    /// the console window.
    /// </summary>
    public interface IBooleanPrompter : IPrompter
    {
        /// <summary>
        /// Prompts the user for a boolean value.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="defaultValue">
        /// Optional default value to use if the user enters an empty string.
        /// </param>
        /// <returns>The entered value, or the default value if applicable.</returns>
        bool? Prompt(string prompt, bool? defaultValue = null);
    }
#pragma warning restore SA1201 // Elements should appear in the correct order
}
