// <copyright file="QuitKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.KeyPressHandlers
{
    /// <summary>
    /// Quits the game.
    /// </summary>
    public class QuitKeyPressHandler : IKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IMazeGameController controller)
        {
            controller.Quit();
        }
    }
}
