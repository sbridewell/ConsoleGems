// <copyright file="MazeGameKeyPressMappings.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.KeyPressHandlers
{
    /// <summary>
    /// Maps key presses to their handlers.
    /// </summary>
    public class MazeGameKeyPressMappings
    {
        /// <summary>
        /// Gets the mapping of key presses to handlers.
        /// </summary>
        public Dictionary<ConsoleKey, IKeyPressHandler> Mappings { get; } = new ()
        {
            { ConsoleKey.LeftArrow, new TurnLeftKeyPressHandler() },
            { ConsoleKey.RightArrow, new TurnRightKeyPressHandler() },
            { ConsoleKey.UpArrow, new MoveForwardKeyPressHandler() },
            { ConsoleKey.Q, new QuitKeyPressHandler() },
        };
    }
}
