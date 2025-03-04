// <copyright file="ConsoleKeyInfoWrapper.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete
{
    using Xunit.Abstractions;

    /// <summary>
    /// Wrapper class for the <see cref="ConsoleKeyInfo"/> struct which is
    /// serializable by xUnit.
    /// </summary>
    public class ConsoleKeyInfoWrapper(char character, ConsoleKey key, bool shift = false, bool alt = false, bool control = false)
        : IXunitSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleKeyInfoWrapper"/> class.
        /// </summary>
        /// <remarks>
        /// Parameterless constructor is required for xUnit to be able to serialize and
        /// deserialize instances of this class which are in the test data, in order for
        /// them to be displayed correctly in the Visual Studio test runner.
        /// </remarks>
        public ConsoleKeyInfoWrapper()
            : this(' ', ConsoleKey.None, false, false, false)
        {
        }

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        public char Character { get; set; } = character;

        /// <summary>
        /// Gets or sets the ConsoleKey.
        /// </summary>
        public ConsoleKey Key { get; set; } = key;

        /// <summary>
        /// Gets or sets a value indicating whether the shift key is pressed.
        /// </summary>
        public bool Shift { get; set; } = shift;

        /// <summary>
        /// Gets or sets a value indicating whether the alt key is pressed.
        /// </summary>
        public bool Alt { get; set; } = alt;

        /// <summary>
        /// Gets or sets a value indicating whether the ctrl key is pressed.
        /// </summary>
        public bool Control { get; set; } = control;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ConsoleKeyInfoWrapper: '{this.Character}', Key = {this.Key}"
                + (this.Shift ? ", Shift" : string.Empty)
                + (this.Control ? ", Ctrl" : string.Empty)
                + (this.Alt ? ", Alt" : string.Empty)
                + "]";
        }

        /// <inheritdoc/>
        public void Deserialize(IXunitSerializationInfo info)
        {
            this.Character = info.GetValue<char>(nameof(this.Character));
            this.Key = Enum.Parse<ConsoleKey>(info.GetValue<string>(nameof(this.Key)));
            this.Shift = info.GetValue<bool>(nameof(this.Shift));
            this.Alt = info.GetValue<bool>(nameof(this.Alt));
            this.Control = info.GetValue<bool>(nameof(this.Control));
        }

        /// <inheritdoc/>
        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(this.Character), this.Character);
            info.AddValue(nameof(this.Key), this.Key.ToString());
            info.AddValue(nameof(this.Shift), this.Shift);
            info.AddValue(nameof(this.Alt), this.Alt);
            info.AddValue(nameof(this.Control), this.Control);
        }
    }
}
