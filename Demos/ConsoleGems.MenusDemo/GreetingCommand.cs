// <copyright file="GreetingCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Consoles.Interfaces;

    /// <summary>
    /// A command which displays a different greeting depending
    /// on the time of day.
    /// </summary>
    public class GreetingCommand(IConsole console)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var now = DateTime.Now;
            var hourOfDay = now.Hour;
            var greeting = hourOfDay switch
            {
                < 12 => "Good morning",
                < 18 => "Good afternoon",
                _ => "Good evening",
            };
            console.WriteLine($"{greeting}, and welcome to the ConsoleGems menus demo.");
        }
    }
}
