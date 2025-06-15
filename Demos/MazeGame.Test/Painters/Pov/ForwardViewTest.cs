// <copyright file="ForwardViewTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test.Painters.Pov
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Pov;
    using MPT = Sde.MazeGame.Models.MazePointType;

    /// <summary>
    /// Unit tests for the <see cref="ForwardView"/> class.
    /// </summary>
    public class ForwardViewTest
    {
        [SuppressMessage(
            "Style",
            "IDE1006:Naming Styles",
            Justification = "Contradicts SA1313 which is a warning rather than info")]
        private record ForwardViewTestCase(
            Maze maze,
            Player player,
            List<MPT> leftRow,
            List<MPT> middleRow,
            List<MPT> rightRow,
            int visibleDistance);

        private static readonly Maze Maze3By3 = new MazeFactory().CreateFromStringArray(
            [
                "###",
                "# #",
                "###",
            ]);

        private static readonly Maze Maze5By5 = new MazeFactory().CreateFromStringArray(
            [
                "#####",
                "#   #",
                "# # #",
                "#   #",
                "#####",
            ]);

        private static readonly Maze Maze9By9 = new MazeFactory().CreateFromStringArray(
            [
                "#########",
                "#       #",
                "# # ### #",
                "#     # #",
                "# # # # #",
                "# #     #",
                "# ### # #",
                "#       #",
                "#########",
            ]);

        /// <summary>
        /// Gets the names of the test cases.
        /// </summary>
        public static TheoryData<string> ForwardViewTestCaseNames
            => new (ForwardViewTestCases.Keys);

        /// <summary>
        /// Gets the test cases.
        /// </summary>
        private static Dictionary<string, ForwardViewTestCase> ForwardViewTestCases
            => new ()
            {
                ["Facing north into a top-left corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.North },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing north into a flat wall"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(2, 1), FacingDirection = Direction.North },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing north into a top-right corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(3, 1), FacingDirection = Direction.North },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing north into a wall with paths either side"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(2, 3), FacingDirection = Direction.North },
                    [MPT.Path, MPT.Path],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path],
                    2),
                ["Facing north into a dead end"] = new ForwardViewTestCase(
                    Maze3By3,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.North },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing north, side passages to right"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(1, 7), FacingDirection = Direction.North },
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    8),
                ["Facing north, side passages to left"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(7, 7), FacingDirection = Direction.North },
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    8),
                ["Facing north, side passages to both sides"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(5, 7), FacingDirection = Direction.North },
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall],
                    6),
                ["Facing south into a bottom-left corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(1, 3), FacingDirection = Direction.South },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing south into a flat wall"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(2, 3), FacingDirection = Direction.South },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing south into a bottom-right corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(3, 3), FacingDirection = Direction.South },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing south into a wall with paths either side"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(2, 1), FacingDirection = Direction.South },
                    [MPT.Path, MPT.Path],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path],
                    2),
                ["Facing south into a dead end"] = new ForwardViewTestCase(
                    Maze3By3,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.South },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing south, side passages to right"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(7, 1), FacingDirection = Direction.South },
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    8),
                ["Facing south, side passages to left"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.South },
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    8),
                ["Facing south, side passages to both sides"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(3, 1), FacingDirection = Direction.South },
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall],
                    6),
                ["Facing west into a top-left corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.West },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing west into a flat wall"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(1, 2), FacingDirection = Direction.West },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing west into a bottom-left corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(1, 3), FacingDirection = Direction.West },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing west into a wall with paths either side"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(3, 2), FacingDirection = Direction.West },
                    [MPT.Path, MPT.Path],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path],
                    2),
                ["Facing west into a dead end"] = new ForwardViewTestCase(
                    Maze3By3,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.West },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing west, side passages to right"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(7, 7), FacingDirection = Direction.West },
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall],
                    8),
                ["Facing west, side passages to left"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(7, 1), FacingDirection = Direction.West },
                    [MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    8),
                ["Facing west, side passages to both sides"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(7, 5), FacingDirection = Direction.West },
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    6),
                ["Facing east into a top-right corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(3, 1), FacingDirection = Direction.East },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing east into a flat wall"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(3, 2), FacingDirection = Direction.East },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    2),
                ["Facing east into a bottom-right corner"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(3, 3), FacingDirection = Direction.East },
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing east into a wall with paths either side"] = new ForwardViewTestCase(
                    Maze5By5,
                    new Player { Position = new ConsolePoint(1, 2), FacingDirection = Direction.East },
                    [MPT.Path, MPT.Path],
                    [MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path],
                    2),
                ["Facing east into a dead end"] = new ForwardViewTestCase(
                    Maze3By3,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.East },
                    [MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall],
                    2),
                ["Facing east, side passages to right"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(1, 1), FacingDirection = Direction.East },
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall],
                    8),
                ["Facing east, side passages to left"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(1, 7), FacingDirection = Direction.East },
                    [MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall, MPT.Wall],
                    8),
                ["Facing east, side passages to both sides"] = new ForwardViewTestCase(
                    Maze9By9,
                    new Player { Position = new ConsolePoint(1, 3), FacingDirection = Direction.East },
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Wall, MPT.Wall],
                    [MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Path, MPT.Wall],
                    [MPT.Path, MPT.Wall, MPT.Path, MPT.Wall, MPT.Path, MPT.Wall],
                    6),
            };

        /// <summary>
        /// Tests that the constructor instantiates the ForwardView correctly.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(ForwardViewTestCaseNames))]
        public void ForwardView_InstantiatesCorrectly(string testCaseName)
        {
            // Arrange
            var testCase = ForwardViewTestCases[testCaseName];

            // Act
            var forwardView = new ForwardView(testCase.maze, testCase.player, testCase.visibleDistance);

            // Assert
            forwardView.VisibleDistance.Should().Be(testCase.visibleDistance);
            forwardView.MiddleRow.Should().BeEquivalentTo(testCase.middleRow, options => options.WithStrictOrdering());
            forwardView.LeftRow.Should().BeEquivalentTo(testCase.leftRow, options => options.WithStrictOrdering());
            forwardView.RightRow.Should().BeEquivalentTo(testCase.rightRow, options => options.WithStrictOrdering());
        }
    }
}
