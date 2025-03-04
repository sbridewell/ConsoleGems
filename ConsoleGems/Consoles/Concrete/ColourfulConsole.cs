// <copyright file="ColourfulConsole.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles.Concrete
{
    using System.Reflection;

    /// <summary>
    /// <see cref="IConsole"/> implementation which writes text
    /// to the console in different colours depending on the supplied
    /// <see cref="ConsoleOutputType"/>.
    /// </summary>
    public class ColourfulConsole(IConsoleColourManager consoleColourManager)
        : Console
    {
        /// <inheritdoc/>
        public override void Write(string textToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.SetColours(outputType);
            base.Write(textToWrite, outputType);
            consoleColourManager.SetColours(ConsoleColours.Default);
        }

        /// <inheritdoc/>
        public override void Write(char characterToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.SetColours(outputType);
            base.Write(characterToWrite, outputType);
            consoleColourManager.SetColours(ConsoleColours.Default);
        }

        /// <inheritdoc/>
        public override void WriteLine(string textToWrite = "", ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.SetColours(outputType);
            base.WriteLine(textToWrite, outputType);
            consoleColourManager.SetColours(ConsoleColours.Default);
        }

        private static PropertyInfo TryGetConsoleColoursProperty(string propertyName)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Static;
            var consoleColoursProperty = typeof(ConsoleColours).GetProperty(propertyName, bindingFlags);
            if (consoleColoursProperty is null)
            {
                var msg = $"The '{nameof(ConsoleColours)}' class does not have a public static "
                    + $"property called '{propertyName}'. "
                    + $"This is a code error - every member of the '{nameof(ConsoleOutputType)}' "
                    + $"must have a public static property with the same name, which gets a "
                    + $"'{nameof(ConsoleColours)}' instance.";
                throw new InvalidOperationException(msg);
            }

            return consoleColoursProperty;
        }

        [ExcludeFromCodeCoverage(Justification = "Cannot unit test this unless we want ConsoleColours to implement an interface")]
        private static ConsoleColours TryGetColours(PropertyInfo consoleColoursProperty)
        {
            var consoleColours = consoleColoursProperty.GetValue(null) as ConsoleColours;
            if (consoleColours is null)
            {
                var msg = $"The '{nameof(ConsoleOutputType)}' property '{consoleColoursProperty.Name}' "
                    + $"either is not of type '{nameof(ConsoleColours)}' or returned null. "
                    + $"This is a code error - every member of the '{nameof(ConsoleOutputType)}' "
                    + $"must have a public static property with the same name, which gets a "
                    + $"'{nameof(ConsoleColours)}' instance.";
                throw new InvalidOperationException(msg);
            }

            return consoleColours;
        }

        /// <summary>
        /// Sets the console's foreground and background colours to the colours
        /// defined in the <see cref="ConsoleColours"/> static property with the
        /// same name as the supplied <see cref="ConsoleOutputType"/>.
        /// </summary>
        /// <param name="outputType">
        /// The <see cref="ConsoleOutputType"/> member which determines the colours
        /// to use.
        /// </param>
        private void SetColours(ConsoleOutputType outputType)
        {
            var consoleColoursProperty = TryGetConsoleColoursProperty(outputType.ToString());
            var consoleColours = TryGetColours(consoleColoursProperty);
            consoleColourManager.SetColours(consoleColours);
        }
    }
}
