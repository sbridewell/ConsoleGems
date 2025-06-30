// <copyright file="ConsolePointTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

using System.Reflection.Metadata;

namespace Sde.ConsoleGems.Test.Text
{
    /// <summary>
    /// Unit tests for the <see cref="ConsolePoint"/> struct.
    /// </summary>
    public class ConsolePointTest
    {
        private record ConsolePointTestCase(ConsolePoint first, ConsolePoint second, bool shouldBeEqual);

        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> ConsolePointTestCaseNames => new (ConsolePointTestCases.Keys);

        private static Dictionary<string, ConsolePointTestCase> ConsolePointTestCases => new ()
        {
            { "Equality1", new ConsolePointTestCase(new ConsolePoint(0, 1), new ConsolePoint(0, 1), true) },
            { "Equality2", new ConsolePointTestCase(new ConsolePoint(1, 0), new ConsolePoint(1, 0), true) },
            { "Inequality1", new ConsolePointTestCase(new ConsolePoint(0, 1), new ConsolePoint(1, 0), false) },
            { "Inequality2", new ConsolePointTestCase(new ConsolePoint(1, 0), new ConsolePoint(0, 1), false) },
            { "Inequality3", new ConsolePointTestCase(new ConsolePoint(0, 1), new ConsolePoint(0, 2), false) },
            { "Inequality4", new ConsolePointTestCase(new ConsolePoint(1, 0), new ConsolePoint(2, 0), false) },
        };

        /// <summary>
        /// Tests the Equals method, and also the == operator.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ConsolePointTestCaseNames))]
        public void EqualsTest(string testCaseName)
        {
            // Arrange
            var testCase = ConsolePointTestCases[testCaseName];
            var first = testCase.first;
            var second = testCase.second;

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().Be(testCase.shouldBeEqual);
        }

        /// <summary>
        /// Test case for the != operator.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ConsolePointTestCaseNames))]
        public void NotEqualsTest(string testCaseName)
        {
            // Arrange
            var testCase = ConsolePointTestCases[testCaseName];
            var first = testCase.first;
            var second = testCase.second;

            // Act
            var result = first != second;

            // Assert
            result.Should().Be(!testCase.shouldBeEqual);
        }

        /// <summary>
        /// Tests that the Equals method returns false if passed an object which is not a ConsolePoint.
        /// </summary>
        [Fact]
        public void Equals_OtherIsNotAConsolePoint_ReturnsFalse()
        {
            // Arrange
            var point = new ConsolePoint(0, 0);
            var other = new object();

            // Act
            var result = point.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }
}
