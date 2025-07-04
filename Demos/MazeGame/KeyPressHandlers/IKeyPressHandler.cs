// <copyright file="IKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.KeyPressHandlers
{
    /// <summary>
    /// Interface for handling key presses.
    /// </summary>
    public interface IKeyPressHandler
    {
        /// <summary>
        /// Handles the supplied key press.
        /// </summary>
        /// <param name="keyInfo">The key press to handle.</param>
        /// <param name="controller">The maze game controller.</param>
        void Handle(ConsoleKeyInfo keyInfo, IMazeGameController controller);
    }
}
