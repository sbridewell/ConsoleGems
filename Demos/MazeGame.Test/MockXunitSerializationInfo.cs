// <copyright file="MockXunitSerializationInfo.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using Xunit.Abstractions;

    /// <summary>
    /// Mock implementation suggested by Copilot.
    /// </summary>
    public class MockXunitSerializationInfo : IXunitSerializationInfo
    {
        private readonly Dictionary<string, object> data = new();

        public void AddValue(string key, object value, Type type = null)
        {
            data[key] = value;
        }

        public object GetValue(string key, Type type)
        {
            return this.data[key];
        }

        public T GetValue<T>(string key)
        {
            return (T)this.data[key];
        }
    }
}
