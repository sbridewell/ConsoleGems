// <copyright file="F2KeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoCompleteDemo
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.AutoComplete.KeyPressHandlers;

    /// <summary>
    /// Handles the F2 key by moving the cursor one character to the right,
    /// but only if the shift key is held down.
    /// </summary>
    public class F2KeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift))
            {
                autoCompleter.MoveCursorRight();
            }
            else
            {
                var literalKeyHandler = new LiteralKeyPressHandler();
                literalKeyHandler.Handle(keyInfo, autoCompleter);
            }
        }
    }
}
