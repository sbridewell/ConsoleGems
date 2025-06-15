// <copyright file="TurnRightKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.KeyPressHandlers
{
    /// <summary>
    /// Turns the player right.
    /// </summary>
    public class TurnRightKeyPressHandler
        : IKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, GameController controller)
        {
            controller.TurnPlayerRight();
        }
    }
}
