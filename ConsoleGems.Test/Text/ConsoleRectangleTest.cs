// <copyright file="ConsoleRectangleTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Text
{
    /// <summary>
    /// Unit tests for the <see cref="ConsoleRectangle"/> struct.
    /// </summary>
    public class ConsoleRectangleTest
    {
        private record PropertiesTestCase(
            ConsoleSize size,
            ConsolePoint origin,
            int expectedWidth,
            int expectedHeight,
            int expectedX,
            int expectedY,
            int expectedRight,
            int expectedBottom);

        private record ContainsTestCase(
            ConsolePoint origin,
            ConsoleSize size,
            ConsolePoint pointToTest,
            bool expectedResult);

        private record OverlapsWithTestCase(ConsoleRectangle rect1, ConsoleRectangle rect2, bool expectedResult);

        /// <summary>
        /// Gets the names of the test cases for the properties test.
        /// </summary>
        public static TheoryData<string> PropertyTestCases => new (PropertiesTestData.Keys);

        /// <summary>
        /// Gets the names of the test cases for the Contains test.
        /// </summary>
        public static TheoryData<string> ContainsTestCases => new (ContainsTestData.Keys);

        /// <summary>
        /// Gets the names of the test cases for the OverlapsWith test.
        /// </summary>
        public static TheoryData<string> OverlapsWithTestCases => new (OverlapsWithTestData.Keys);

        /// <summary>
        /// Gets a dictionary of test case names and test data for the properties test.
        /// </summary>
        private static Dictionary<string, PropertiesTestCase> PropertiesTestData
        {
            get
            {
                return new ()
                {
                    { "10x5", new (new ConsoleSize(10, 5), new ConsolePoint(0, 0), 10, 5, 0, 0, 9, 4) },
                    { "20x10", new (new ConsoleSize(20, 10), new ConsolePoint(5, 5), 20, 10, 5, 5, 24, 14) },
                    { "15x7", new (new ConsoleSize(15, 7), new ConsolePoint(2, 3), 15, 7, 2, 3, 16, 9) },
                    { "30x15", new (new ConsoleSize(30, 15), new ConsolePoint(10, 10), 30, 15, 10, 10, 39, 24) },
                };
            }
        }

        /// <summary>
        /// Gets a dictionary of test case names and test data for the Contains test.
        /// </summary>
        private static Dictionary<string, ContainsTestCase> ContainsTestData
        {
            get
            {
                return new ()
                {
                    { "0,0", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(0, 0), false) },
                    { "0,1", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(0, 1), false) },
                    { "0,2", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(0, 2), false) },
                    { "0,3", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(0, 3), false) },
                    { "1,0", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(1, 0), false) },
                    { "1,1", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(1, 1), true) },
                    { "1,2", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(1, 2), true) },
                    { "1,3", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(1, 3), false) },
                    { "2,0", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(2, 0), false) },
                    { "2,1", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(2, 1), true) },
                    { "2,2", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(2, 2), true) },
                    { "2,3", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(2, 3), false) },
                    { "3,0", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(3, 0), false) },
                    { "3,1", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(3, 1), false) },
                    { "3,2", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(3, 2), false) },
                    { "3,3", new (new ConsolePoint(1, 1), new ConsoleSize(2, 2), new ConsolePoint(3, 3), false) },
                };
            }
        }

        /// <summary>
        /// Gets a dictionary of test case names and test data for the OverlapsWith test.
        /// </summary>
        [SuppressMessage(
            "StyleCop.CSharp.ReadabilityRules",
            "SA1123:Do not place regions within elements",
            Justification = "It's a long list so breaking it into regions makes it more readable")]
        private static Dictionary<string, OverlapsWithTestCase> OverlapsWithTestData
        {
            get
            {
                return new ()
                {
                    #region test cases for adjacent rectangles
                    {
                        /*   22
                         * 1122
                         * 11   */
                        "Adjacent1",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(3, 0), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 1122
                         * 1122 */
                        "Adjacent2",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(3, 1), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11
                         * 1122
                         *   22 */
                        "Adjacent3",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(3, 2), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11
                         * 11
                         *  22
                         *  22 */
                        "Adjacent4",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(2, 3), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11
                         * 11
                         * 22
                         * 22 */
                        "Adjacent5",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(1, 3), new ConsoleSize(2, 2)),
                            false)
                    },

                    #endregion

                    #region test cases for rectangles separated by 1 character
                    {
                        /*    22
                         * 11 22
                         * 11    */
                        "1Apart1",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(3, 0), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11 22
                         * 11 22 */
                        "1Apart2",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(3, 1), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11
                         * 11 22
                         *    22 */
                        "1Apart3",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(3, 2), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11
                         * 11
                         *
                         *  22
                         *  22 */
                        "1Apart4",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(1, 3), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /* 11
                         * 11
                         *
                         * 22
                         * 22 */
                        "1Apart5",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(0, 3), new ConsoleSize(2, 2)),
                            false)
                    },
                    {
                        /*  11
                         *  11
                         *
                         * 22
                         * 22 */
                        "1Apart6",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 0), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(0, 3), new ConsoleSize(2, 2)),
                            false)
                    },

                    #endregion

                    #region test cases for overlapping rectangles
                    {
                        /*  22
                         * 1#2
                         * 11 */
                        "Overlap1",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(1, 0), new ConsoleSize(2, 2)),
                            true)
                    },
                    {
                        /* 1#2
                         * 1#2 */
                        "Overlap2",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 0), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(1, 0), new ConsoleSize(2, 2)),
                            true)
                    },
                    {
                        /* 11
                         * 1#2
                         *  22 */
                        "Overlap3",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 0), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(1, 1), new ConsoleSize(2, 2)),
                            true)
                    },
                    {
                        /* 11
                         * ##
                         * 22 */
                        "Overlap4",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 0), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            true)
                    },
                    {
                        /*  11
                         * 2#1
                         * 22  */
                        "Overlap5",
                        new (
                            new ConsoleRectangle(new ConsolePoint(1, 0), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(0, 1), new ConsoleSize(2, 2)),
                            true)
                    },
                    {
                        "Full overlap",
                        new (
                            new ConsoleRectangle(new ConsolePoint(0, 0), new ConsoleSize(2, 2)),
                            new ConsoleRectangle(new ConsolePoint(0, 0), new ConsoleSize(2, 2)),
                            true)
                    },

                    #endregion
                };
            }
        }

        /// <summary>
        /// Tests that the constructor sets the property values correctly.
        /// </summary>
        /// <param name="testCaseName">The name of the test case.</param>
        [Theory]
        [MemberData(nameof(PropertyTestCases))]
        public void Constructor_SetsPropertiesCorrectly(string testCaseName)
        {
            // Arrange
            var testCase = PropertiesTestData[testCaseName];

            // Act
            var rect = new ConsoleRectangle(testCase.origin, testCase.size);

            // Assert
            rect.Size.Should().Be(testCase.size);
            rect.Origin.Should().Be(testCase.origin);
            rect.Width.Should().Be(testCase.expectedWidth);
            rect.Height.Should().Be(testCase.expectedHeight);
            rect.X.Should().Be(testCase.expectedX);
            rect.Y.Should().Be(testCase.expectedY);
            rect.Right.Should().Be(testCase.expectedRight);
            rect.Bottom.Should().Be(testCase.expectedBottom);
        }

        /// <summary>
        /// Tests that the Contains method returns the expected result.
        /// </summary>
        /// <param name="testCaseName">The name of the test case.</param>
        [Theory]
        [MemberData(nameof(ContainsTestCases))]
        public void Contains_ReturnsCorrectResult(string testCaseName)
        {
            // Arrange
            var testCase = ContainsTestData[testCaseName];
            var rect = new ConsoleRectangle(testCase.origin, testCase.size);

            // Act
            var result = rect.Contains(testCase.pointToTest);

            // Assert
            result.Should().Be(testCase.expectedResult);
        }

        /// <summary>
        /// Tests that the OverlapsWith method returns the expected result.
        /// </summary>
        /// <param name="testCaseName">The name of the test case.</param>
        [Theory]
        [MemberData(nameof(OverlapsWithTestCases))]
        public void OverlapsWith_ReturnsCorrectResult(string testCaseName)
        {
            // Arrange
            var testCase = OverlapsWithTestData[testCaseName];

            // Act
            var result1 = testCase.rect1.OverlapsWith(testCase.rect2);
            var result2 = testCase.rect2.OverlapsWith(testCase.rect1);

            // Assert
            result1.Should().Be(testCase.expectedResult);
            result2.Should().Be(testCase.expectedResult);
        }

        /// <summary>
        /// Validates that the OverlapsWith test cases don't contain duplicates.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Two test cases have the same value.
        /// </exception>
        [Fact]
        public void Validate_OverlapsWith_TestCases()
        {
            // Arrange
            var dict = new Dictionary<OverlapsWithTestCase, string>();

            // Act
            foreach (var kvp in OverlapsWithTestData)
            {
                try
                {
                    dict.Add(kvp.Value, kvp.Key);
                }
                catch (ArgumentException ex)
                {
                    var firstName = dict[kvp.Value];
                    var msg = $"The test case '{kvp.Key}' is a duplicate of '{firstName}'";
                    throw new ArgumentException(msg, ex);
                }
            }

            // Assert
            true.Should().Be(true);
        }
    }
}
