// <copyright file="KeyPressHandlerTestBase.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Abstract base class for unit tests for <see cref="IAutoCompleteKeyPressHandler"/>
    /// implementations.
    /// </summary>
    public abstract class KeyPressHandlerTestBase
    {
        /// <summary>
        /// Gets the <see cref="IAutoCompleteKeyPressHandler"/> instance under test.
        /// </summary>
        public abstract IAutoCompleteKeyPressHandler HandlerUnderTest { get; }

        /// <summary>
        /// Gets a dummy implementation of the rewriteUserInput action.
        /// </summary>
        /// <remarks>
        /// This is temporary until we can remove the call to RewriteUserInput.
        /// </remarks>
        protected Action<bool> RewriteUserInput { get; } = (preserveCursorPosition) => { };

        /// <summary>
        /// Gets some suggestions which can be used by autocomplete.
        /// </summary>
        protected List<string> Suggestions { get; } =
            ["apples", "bananas", "cheese"];

        /// <summary>
        /// A quick way of creating a <see cref="ConsoleKeyInfo"/> instance.
        /// </summary>
        /// <param name="key">Represents the keypress.</param>
        /// <param name="shift">True to add the shift modifier.</param>
        /// <param name="alt">True to add the alt modifier.</param>
        /// <param name="control">True to add the control modifier.</param>
        /// <returns>The requested ConsoleKeyInfo instance.</returns>
        protected static ConsoleKeyInfo CreateKey(ConsoleKey key, bool shift = false, bool alt = false, bool control = false)
        {
            return new ConsoleKeyInfo('\0', key, shift, alt, control);
        }

        /// <summary>
        /// A quick way of creating a <see cref="ConsoleKeyInfo"/> instance.
        /// </summary>
        /// <param name="keyChar">The character resulting from the keypress.</param>
        /// <param name="shift">True to add the shift modifier.</param>
        /// <param name="alt">True to add the alt modifier.</param>
        /// <param name="control">True to add the control modifier.</param>
        /// <returns>The requested ConsoleKeyInfo instance.</returns>
        protected static ConsoleKeyInfo CreateKey(char keyChar, bool shift = false, bool alt = false, bool control = false)
        {
            var keyInfo = new ConsoleKeyInfo(keyChar, ConsoleKey.None, shift, alt, control);
            return keyInfo;
        }

        /// <summary>
        /// A quick way of creating a <see cref="ConsoleKeyInfo"/> instance.
        /// </summary>
        /// <param name="keyChar">The character resulting from the keypress.</param>
        /// <param name="key">Represents the keypress.</param>
        /// <param name="shift">True to add the shift modifier.</param>
        /// <param name="alt">True to add the alt modifier.</param>
        /// <param name="control">True to add the control modifier.</param>
        /// <returns>The requested ConsoleKeyInfo instance.</returns>
        protected static ConsoleKeyInfo CreateKey(char keyChar, ConsoleKey key, bool shift = false, bool alt = false, bool control = false)
        {
            var keyInfo = new ConsoleKeyInfo(keyChar, key, shift, alt, control);
            return keyInfo;
        }

        /// <summary>
        /// A quick way of creating an internally consistent
        /// <see cref="AutoCompleter"/> instance.
        /// </summary>
        /// <param name="enteredText">
        /// The text which has been entered into the console.
        /// </param>
        /// <param name="withSuggestions">
        /// True to include autocomplete suggestions.
        /// Defaults to false.
        /// </param>
        /// <returns>The requested <see cref="AutoCompleter"/> instance.</returns>
        [Obsolete("Use Mock<IAutoCompleter> instead")]
        protected IAutoCompleter CreateAutoCompleter(string enteredText, bool withSuggestions = false)
        {
            var suggestions = withSuggestions ? this.Suggestions : new ();
            var mockMappings = new Mock<IAutoCompleteKeyPressMappings>();
            mockMappings.Setup(m => m.GetHandler(It.IsAny<ConsoleKey>())).Returns(this.HandlerUnderTest);
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter.Setup(m => m.Suggestions).Returns(suggestions);
            return mockAutoCompleter.Object;
        }
    }
}
