// <copyright file="MoveForwardKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.KeyPressHandlers
{
    /// <summary>
    /// Moves the player forward, if they can move forward.
    /// </summary>
    public class MoveForwardKeyPressHandler : IKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, GameController controller)
        {
            controller.TryToMovePlayerForward();
        }
    }
}
