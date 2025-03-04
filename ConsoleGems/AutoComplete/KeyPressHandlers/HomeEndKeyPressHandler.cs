// <copyright file="HomeEndKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation to handle
    /// the home and end keys.
    /// </summary>
    public class HomeEndKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (keyInfo.Key == ConsoleKey.Home)
            {
                autoCompleter.MoveCursorToHome();
            }
            else if (keyInfo.Key == ConsoleKey.End)
            {
                autoCompleter.MoveCursorToEnd();
            }
        }
    }
}
