// <copyright file="TurnLeftKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.KeyPressHandlers
{
    /// <summary>
    /// Turns the player left.
    /// </summary>
    public class TurnLeftKeyPressHandler
        : IKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, GameController controller)
        {
            // TODO: shouldn't need to pass the controller to its own methods
            ArgumentNullException.ThrowIfNull(controller.CurrentGame);
            controller.TurnPlayerLeft(controller.CurrentGame.Maze, controller.CurrentGame.Player);
        }
    }
}
