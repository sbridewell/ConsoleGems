// <copyright file="LockObjects.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    /// <summary>
    /// Objects used for locking to prevent race conditions in unit tests.
    /// </summary>
    public static class LockObjects
    {
        /// <summary>
        /// Gets an object which can be locked by unit tests which work with
        /// <see cref="System.Console"/> or <see cref="IConsole"/> implementations
        /// which are not mocked.
        /// </summary>
        public static object ConsoleLock { get; } = new ();

        /// <summary>
        /// Gets an object which can be locked by unit tests which work with
        /// <see cref="TextCopy.ClipboardService"/>.
        /// </summary>
        public static object ClipboardLock { get; } = new ();
    }
}
