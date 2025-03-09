// <copyright file="RollDiceCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// A command which simulates rolling two dice.
    /// </summary>
    public class RollDiceCommand(IConsole console)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var random = Random.Shared;
            var throw1 = random.Next(1, 7);
            var throw2 = random.Next(1, 7);
            console.WriteLine($"You threw a '{throw1}' and a '{throw2}'.");
        }
    }
}
