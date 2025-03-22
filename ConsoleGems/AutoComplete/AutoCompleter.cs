// <copyright file="AutoCompleter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete
{
    /// <summary>
    /// Command line prompt with autocomplete functionality.
    /// </summary>
    /// <remarks>
    /// Based on code from <see href="https://codereview.stackexchange.com/q/139172">
    /// "Autocompleting console input" on Code Review Stack Exchange</see> and
    /// <see href="https://stackoverflow.com/q/39029847/16563198">
    /// "User input file path with tab auto complete" on Stack Overflow</see>.
    /// </remarks>
    public class AutoCompleter(
        IAutoCompleteKeyPressMappings autoCompleteKeyPressMappings,
        IAutoCompleteMatcher matcher,
        IConsole console)
        : IAutoCompleter
    {
        private int currentSuggestionIndex = -1;

        /// <inheritdoc/>
        public bool CursorIsAtHome => this.CursorPositionWithinUserInput == 0;

        /// <inheritdoc/>
        public bool CursorIsAtEnd => this.CursorPositionWithinUserInput == this.UserInputSB.Length;

        /// <inheritdoc/>
        public List<string> Suggestions { get; private set; } = new List<string>();

        /// <inheritdoc/>
        public int CursorPositionWithinUserInput { get; private set; }

        /// <inheritdoc/>
        public string UserInput => this.UserInputSB.ToString();

        /// <summary>
        /// Gets or sets a stringbuilder containing the text that the user has
        /// input or autocompleted so far.
        /// </summary>
        private StringBuilder UserInputSB { get; set; } = new StringBuilder();

        /// <inheritdoc/>
        public string ReadLine(List<string> suggestions, string prompt = "")
        {
            this.Suggestions = suggestions;
            this.UserInputSB = new ();
            this.CursorPositionWithinUserInput = 0;
            console.Write(prompt, ConsoleOutputType.Prompt);
            var keyInfo = console.ReadKey(intercept: true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                var handler = autoCompleteKeyPressMappings.GetHandler(keyInfo.Key);
                console.CursorVisible = false;
                handler.Handle(keyInfo, this);
                console.CursorVisible = true;
                keyInfo = console.ReadKey(intercept: true);
            }

            // Make sure any subsequent writes appear on a new line rather than overwriting
            // the prompt and user input.
            console.WriteLine();
            this.SelectNoSuggestion();
            return this.UserInputSB.ToString();
        }

        #region methods for updating the user input

        /// <inheritdoc/>
        public void InsertUserInput(string text)
        {
            foreach (var character in text)
            {
                this.InsertUserInput(character);
            }
        }

        /// <inheritdoc/>
        public void InsertUserInput(char character)
        {
            if (this.CursorIsAtEnd)
            {
                this.UserInputSB.Append(character);
                console.Write(character, ConsoleOutputType.UserInput);
                this.CursorPositionWithinUserInput++;
            }
            else
            {
                var left = console.CursorLeft;
                var top = console.CursorTop;
                this.UserInputSB.Insert(this.CursorPositionWithinUserInput, character);
                var replacementText = this.UserInput.Substring(this.CursorPositionWithinUserInput);
                console.Write(replacementText, ConsoleOutputType.UserInput);
                console.CursorLeft = left;
                console.CursorTop = top;
                this.MoveCursorRight();
           }
        }

        /// <inheritdoc/>
        public void RemoveCurrentCharacterFromUserInput()
        {
            if (!this.CursorIsAtEnd)
            {
                var left = console.CursorLeft;
                var top = console.CursorTop;
                this.UserInputSB.Remove(this.CursorPositionWithinUserInput, 1);
                var replacementText = $"{this.UserInput.Substring(this.CursorPositionWithinUserInput)} ";
                console.Write(replacementText, ConsoleOutputType.UserInput);
                console.CursorLeft = left;
                console.CursorTop = top;
            }
        }

        /// <inheritdoc/>
        public void RemovePreviousCharacterFromUserInput()
        {
            if (!this.CursorIsAtHome)
            {
                this.MoveCursorLeft();
                this.RemoveCurrentCharacterFromUserInput();
            }
        }

        /// <inheritdoc/>
        public void ReplaceUserInputWith(string text)
        {
            this.MoveCursorToHome();
            var left = console.CursorLeft;
            var top = console.CursorTop;
            var spaces = text.Length < this.UserInput.Length
                ? new string(' ', this.UserInput.Length - text.Length)
                : string.Empty;
            var replacementText = text + spaces;
            this.UserInputSB.Remove(0, this.UserInputSB.Length);
            this.UserInputSB.Append(text);
            console.Write(replacementText, ConsoleOutputType.UserInput);
            console.CursorLeft = left;
            console.CursorTop = top;
            this.CursorPositionWithinUserInput = 0;
            this.MoveCursorToEnd();
        }

        #endregion

        #region methods for moving the cursor

        /// <inheritdoc/>
        public void MoveCursorLeft(int numberOfCharacters)
        {
            for (var i = 0; i < numberOfCharacters; i++)
            {
                this.MoveCursorLeft();
            }
        }

        /// <inheritdoc/>
        public void MoveCursorLeft()
        {
            if (this.CursorPositionWithinUserInput > 0)
            {
                this.CursorPositionWithinUserInput--;
                if (console.CursorLeft == 0)
                {
                    console.CursorLeft = console.WindowWidth - 1;

                    // The following line doesn't completely solve the problem
                    // of user input scrolling off the top of the screen.
                    // It stops an error being thrown when trying to move the
                    // cursor back to the start of user input, but it means
                    // that rows of user input which have scrolled off the top
                    // of the screen can no longer be edited.
                    // The full user input, including any which can no longer
                    // be edited, is still returned by the ReadLine method.
                    // If this happens to you, your best bet for now is to
                    // make the console window bigger.
                    console.CursorTop = Math.Max(0, console.CursorTop - 1);
                }
                else
                {
                    console.CursorLeft--;
                }
            }
        }

        /// <inheritdoc/>
        public void MoveCursorRight(int numberOfCharacters)
        {
            for (var i = 0; i < numberOfCharacters; i++)
            {
                this.MoveCursorRight();
            }
        }

        /// <inheritdoc/>
        public void MoveCursorRight()
        {
            if (this.CursorPositionWithinUserInput < this.UserInputSB.Length)
            {
                this.CursorPositionWithinUserInput++;
                if (console.CursorLeft == console.WindowWidth - 1)
                {
                    console.CursorLeft = 0;
                    console.CursorTop = Math.Min(console.WindowHeight - 1, console.CursorTop + 1);
                }
                else
                {
                    console.CursorLeft++;
                }
            }
        }

        /// <inheritdoc/>
        public void MoveCursorToHome()
        {
            while (this.CursorPositionWithinUserInput > 0)
            {
                this.MoveCursorLeft();
            }
        }

        /// <inheritdoc/>
        public void MoveCursorToEnd()
        {
            while (this.CursorPositionWithinUserInput < this.UserInputSB.Length)
            {
                this.MoveCursorRight();
            }
        }

        #endregion

        #region methods for working with suggestions

        /// <inheritdoc/>
        public void SelectFirstMatchingSuggestion()
        {
            this.currentSuggestionIndex
                = matcher.FindMatch(this.UserInput, this.Suggestions);
        }

        /// <inheritdoc/>
        public void SelectNextSuggestion()
        {
            this.currentSuggestionIndex
                = (this.currentSuggestionIndex + 1)
                % this.Suggestions.Count;
        }

        /// <inheritdoc/>
        public void SelectPreviousSuggestion()
        {
            this.currentSuggestionIndex
                = (this.currentSuggestionIndex - 1 + this.Suggestions.Count)
                % this.Suggestions.Count;
        }

        /// <inheritdoc/>
        public void SelectNoSuggestion()
        {
            this.currentSuggestionIndex = -1;
        }

        /// <inheritdoc/>
        public string? GetCurrentSuggestion()
        {
            if (this.currentSuggestionIndex == -1 || !this.Suggestions.Any())
            {
                return null;
            }
            else
            {
                return this.Suggestions[this.currentSuggestionIndex];
            }
        }

        #endregion

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Suggestion index: {this.currentSuggestionIndex}, "
                + $"cursor position: {this.CursorPositionWithinUserInput}";
        }
    }
}