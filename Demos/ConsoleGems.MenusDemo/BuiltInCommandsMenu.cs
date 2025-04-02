// <copyright file="BuiltInCommandsMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Commands.Demo;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// A menu showing commands that are built in to the ConsoleGems library.
    /// </summary>
    public class BuiltInCommandsMenu(
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState,
        ChangeWorkingDirectoryCommand cwdCommand,
        DirCommand dirCommand,
        PrintWorkingDirectoryCommand pwdCommand,
        SelectAFileCommand selectAFileCommand,
        SelectAFolderCommand selectAFolderCommand,
        ThrowExceptionCommand throwExceptionCommand)
        : AbstractMenu(
            autoCompleter,
            menuWriter,
            console,
            applicationState)
    {
        /// <inheritdoc/>
        public override string Title => "Built-in commands";

        /// <inheritdoc/>
        public override string Description => "These commands are built in to the ConsoleGems library.";

        /// <inheritdoc/>
        public override List<MenuItem> MenuItems =>
        [
            new () { Key = "pwd", Description = "Print the working directory", Command = pwdCommand },
            new () { Key = "cwd", Description = "Change the working directory", Command = cwdCommand },
            new () { Key = "dir", Description = "List the contents of the working directory", Command = dirCommand },
            new () { Key = "file", Description = "Select a file", Command = selectAFileCommand },
            new () { Key = "folder", Description = "Select a folder", Command = selectAFolderCommand },
            new () { Key = "throw", Description = "Throw an exception", Command = throwExceptionCommand }
        ];
    }
}
