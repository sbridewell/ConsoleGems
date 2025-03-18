// <copyright file="ExitProgramCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command for exiting the program normally.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExitProgramCommand(IConsole console, ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            console.WriteLine("Bye!");
            applicationState.ExitProgram = true;
        }
    }
}
