//// <copyright file="ConsolePointOffsetProxy.cs" company="Simon Bridewell">
//// Copyright (c) Simon Bridewell.
//// Released under the MIT license - see LICENSE.txt in the repository root.
//// </copyright>

//namespace Sde.MazeGame.Test
//{
//    using Sde.MazeGame.Models;
//    using Xunit.Abstractions;

//    [Serializable]
//    public class ConsolePointOffsetProxy(int x, int y)
//        : ConsolePointOffset(x, y), IXunitSerializable
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="ConsolePointOffsetProxy"/> class.
//        /// </summary>
//        public ConsolePointOffsetProxy()
//            : this(0, 0)
//        {
//        }

//        public void Serialize(IXunitSerializationInfo info)
//        {
//            info.AddValue(nameof(this.DX), this.DX);
//            info.AddValue(nameof(this.DY), this.DY);
//        }

//        public void Deserialize(IXunitSerializationInfo info)
//        {
//            this.DX = info.GetValue<int>(nameof(this.DX));
//            this.DY = info.GetValue<int>(nameof(this.DY));
//        }
//    }
//}
