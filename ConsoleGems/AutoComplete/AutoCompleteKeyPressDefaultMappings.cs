// <copyright file="AutoCompleteKeyPressDefaultMappings.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete
{
    /// <summary>
    /// Gets the default mappings of console keys to handlers.
    /// </summary>
    public class AutoCompleteKeyPressDefaultMappings : IAutoCompleteKeyPressMappings
    {
        private readonly IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> mappings = new Dictionary<ConsoleKey, IAutoCompleteKeyPressHandler>
        {
            { ConsoleKey.LeftArrow, new ArrowKeyPressHandler() },
            { ConsoleKey.RightArrow, new ArrowKeyPressHandler() },
            { ConsoleKey.Backspace, new BackspaceKeyPressHandler() },
            { ConsoleKey.Delete, new DeleteKeyPressHandler() },
            { ConsoleKey.Home, new HomeEndKeyPressHandler() },
            { ConsoleKey.End, new HomeEndKeyPressHandler() },
            { ConsoleKey.Tab, new TabKeyPressHandler() },
            { ConsoleKey.V, new VKeyPressHandler() },
        };

        /// <inheritdoc/>
        public IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> Mappings => this.mappings;

        /// <inheritdoc/>
        public IAutoCompleteKeyPressHandler DefaultHandler => new LiteralKeyPressHandler();
    }
}
