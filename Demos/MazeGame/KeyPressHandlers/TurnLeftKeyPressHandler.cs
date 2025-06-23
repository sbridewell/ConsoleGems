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
        public void Handle(ConsoleKeyInfo keyInfo, IGameController controller)
        {
            controller.TurnPlayerLeft();
        }
    }
}
