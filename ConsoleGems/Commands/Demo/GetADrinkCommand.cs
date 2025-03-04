// <copyright file="GetADrinkCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands.Demo
{
    /// <summary>
    /// Command to demonstrate autocomplete behaviour in console apps.
    /// </summary>
    public class GetADrinkCommand(
        IAutoCompleter autoCompleteConsole,
        IConsole console)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            List<string> suggestions =
            [
                "coffee", "tea", "water", "beer", "wine", "whisky"
            ];
            var prompt = "What would you like to drink? ";
            var drink = autoCompleteConsole.ReadLine(suggestions, prompt);
            console.WriteLine($"Enjoy your {drink}!");
        }
    }
}
