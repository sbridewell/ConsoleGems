// <copyright file="ICommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Interface representing a command used in console application.
    /// If the command requires user input then the Execute method
    /// is responsible for prompting the user for this input.
    /// If the command has any output then the command is responsible
    /// for displaying the output on the console or writing it to a
    /// file as appropriate.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
    }
}
