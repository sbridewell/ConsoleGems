// <copyright file="PlayerProxy.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;
    using Xunit.Abstractions;

    public class PlayerProxy : Player, IXunitSerializable
    {
        /// <inheritdoc/>
        public void Deserialize(IXunitSerializationInfo info)
        {
            this.Position = info.GetValue<ConsolePoint>(nameof(this.Position));
            this.FacingDirection = info.GetValue<Direction>(nameof(this.FacingDirection));
        }

        /// <inheritdoc/>
        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(this.Position), this.Position);
            info.AddValue(nameof(this.FacingDirection), this.FacingDirection);
        }
    }
}
