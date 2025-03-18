// <copyright file="ThrowExceptionCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using Sde.ConsoleGems.Commands;

    /// <summary>
    /// Command which throws an exception, purely for demonstrating
    /// exception handling within the menu system.
    /// </summary>
    public class ThrowExceptionCommand : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            throw new InvalidOperationException("You wanted to throw an exception, so here it is :-)");
        }
    }
}
