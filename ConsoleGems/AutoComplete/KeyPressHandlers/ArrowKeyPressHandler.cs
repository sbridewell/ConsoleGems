// <copyright file="ArrowKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation for handling
    /// arrow keys.
    /// </summary>
    public class ArrowKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    autoCompleter.MoveCursorLeft();
                    break;

                case ConsoleKey.RightArrow:
                    autoCompleter.MoveCursorRight();
                    break;
            }
        }
    }
}
