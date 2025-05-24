// <copyright file="RayProxy.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using Sde.MazeGame.Painters.RayTracing;
    using Xunit.Abstractions;

    public class RayProxy : Ray, IXunitSerializable
    {
        public void Deserialize(IXunitSerializationInfo info)
        {
            this.Distance = info.GetValue<float>(nameof(Ray.Distance));
            this.Direction = info.GetValue<float>(nameof(Ray.Direction));
            this.HasHitAWall = info.GetValue<bool>(nameof(Ray.HasHitAWall));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(this.Direction), this.Direction);
            info.AddValue(nameof(this.HasHitAWall), this.HasHitAWall);
            info.AddValue(nameof(this.Distance), this.Distance);
        }
    }
}
