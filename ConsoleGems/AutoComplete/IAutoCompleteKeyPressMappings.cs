// <copyright file="IAutoCompleteKeyPressMappings.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete
{
    /// <summary>
    /// Interface for mapping <see cref="ConsoleKey"/> values to
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementations.
    /// </summary>
    public interface IAutoCompleteKeyPressMappings
    {
        /// <summary>
        /// Gets a dictionary of mappings of keypresses to the corresponding
        /// <see cref="IAutoCompleteKeyPressHandler"/> implementations.
        /// </summary>
        IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> Mappings { get; }

        /// <summary>
        /// Gets the <see cref="IAutoCompleteKeyPressHandler"/> to handle any
        /// keypresses which are not found in the <see cref="Mappings"/> dictionary.
        /// </summary>
        IAutoCompleteKeyPressHandler DefaultHandler { get; }

        /// <summary>
        /// Gets the handler for the supplied key.
        /// </summary>
        /// <param name="key">
        /// The ConsoleKey to get the handler for.
        /// </param>
        /// <returns>
        /// The handler for the supplied ConsoleKey.
        /// </returns>
        IAutoCompleteKeyPressHandler GetHandler(ConsoleKey key)
        {
            var found = this.Mappings.TryGetValue(key, out var handler);
            return found ? handler! : this.DefaultHandler;
        }
    }
}
