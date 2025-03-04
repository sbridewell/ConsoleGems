// <copyright file="IAutoCompleter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete
{
    /// <summary>
    /// Interface for a command line prompt with autocomplete functionality.
    /// </summary>
    public interface IAutoCompleter
    {
        /// <summary>
        /// Gets the zero-based horizontal cursor position within the user input.
        /// This is the position where the next character to be typed will be inserted.
        /// </summary>
        int CursorPositionWithinUserInput { get; }

        /// <summary>
        /// Gets a value indicating whether the cursor is at the start of the user input.
        /// </summary>
        bool CursorIsAtHome { get; }

        /// <summary>
        /// Gets a value indicating whether the cursor is at the end of the user input.
        /// </summary>
        bool CursorIsAtEnd { get; }

        /// <summary>
        /// Gets a list of suggestions that the autocomplete can offer.
        /// </summary>
        List<string> Suggestions { get; }

        /// <summary>
        /// Gets the text that the user has entered or autocompleted so far.
        /// </summary>
        string UserInput { get; }

        /// <summary>
        /// Prompts the user to enter a string, and provides autocomplete suggestions
        /// when the tab key is pressed.
        /// </summary>
        /// <returns>The string entered by the user.</returns>
        /// <param name="suggestions">List of autocomplete suggestions.</param>
        /// <param name="prompt">The prompt to display to the user.</param>
        string ReadLine(List<string> suggestions, string prompt = "");

        #region methods for updating the user input

        /// <summary>
        /// Inserts the supplied text into the user input at the current cursor position.
        /// </summary>
        /// <param name="text">The text to insert.</param>
        void InsertUserInput(string text);

        /// <summary>
        /// Inserts the supplied character into the user input at the current cursor position.
        /// </summary>
        /// <param name="character">The character to insert.</param>
        void InsertUserInput(char character);

        /// <summary>
        /// Removes the current character from the user input.
        /// </summary>
        void RemoveCurrentCharacterFromUserInput();

        /// <summary>
        /// Removes the character before the current character from the user input.
        /// </summary>
        void RemovePreviousCharacterFromUserInput();

        /// <summary>
        /// Replaces the whole of the user input with the supplied text.
        /// </summary>
        /// <param name="text">The replacement text.</param>
        void ReplaceUserInputWith(string text);

        #endregion

        #region methods for moving the cursor

        /// <summary>
        /// Moves the cursor left by the supplied number of characters.
        /// </summary>
        /// <param name="numberOfCharacters">
        /// The number of characters to move by.
        /// </param>
        void MoveCursorLeft(int numberOfCharacters);

        /// <summary>
        /// Moves the cursor left by 1 character.
        /// </summary>
        void MoveCursorLeft();

        /// <summary>
        /// Moves the cursor right by the supplied number of characters.
        /// </summary>
        /// <param name="numberOfCharacters">
        /// The number of characters to move by.
        /// </param>
        void MoveCursorRight(int numberOfCharacters);

        /// <summary>
        /// Moves the cursor right by 1 character.
        /// </summary>
        void MoveCursorRight();

        /// <summary>
        /// Moves the cursor to the start of the user input.
        /// </summary>
        void MoveCursorToHome();

        /// <summary>
        /// Moves the cursor to the end of the user input.
        /// </summary>
        void MoveCursorToEnd();

        #endregion

        #region methods for working with suggestions

        /// <summary>
        /// Selects the first suggestion which starts with the current user input.
        /// </summary>
        void SelectFirstMatchingSuggestion();

        /// <summary>
        /// Selects the next suggestion in the list.
        /// If the final suggestion is selected, the first suggestion will be selected.
        /// </summary>
        void SelectNextSuggestion();

        /// <summary>
        /// Selects the previous suggestion in the list.
        /// If the first suggestion is selected, the final suggestion will be selected.
        /// </summary>
        void SelectPreviousSuggestion();

        /// <summary>
        /// Sets none of the suggestions as selected.
        /// </summary>
        void SelectNoSuggestion();

        /// <summary>
        /// Gets the currently selected suggestion.
        /// </summary>
        /// <returns>
        /// The currently selected suggestion, or unll if none is selected.
        /// </returns>
        string? GetCurrentSuggestion();

        #endregion
    }
}
