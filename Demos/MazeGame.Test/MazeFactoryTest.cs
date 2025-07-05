// <copyright file="MazeFactoryTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using FluentAssertions;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Unit tests for the <see cref="MazeFactory"/> class.
    /// </summary>
    public class MazeFactoryTest
    {
        /// <summary>
        /// Tests that CreateFromFile creates a maze with the correct dimensions and points.
        /// </summary>
        [Fact]
        public void CreateFromFile_CreatesCorrectMaze()
        {
            // Arrange
            var mazeData = new string[]
            {
                "#########",
                "#       #",
                "# ##### #",
                "# #   # #",
                "# # # # #",
                "#   #   #",
                "#########",
            };
            var mazeFilePath = Path.Combine("testMaze.txt");
            File.WriteAllLines(mazeFilePath, mazeData);
            var factory = new MazeFactory();

            // Act
            var maze = factory.CreateFromFile(mazeFilePath);

            // Assert
            Assert.NotNull(maze);
            Assert.Equal(7, maze.Height);
            Assert.Equal(9, maze.Width);
            for (var y = 0; y < maze.Height; y++)
            {
                for (var x = 0; x < maze.Width; x++)
                {
                    var expectedPointType = mazeData[y][x] == '#' ? MazePointType.Wall : MazePointType.Path;
                    maze.GetMazePoint(x, y).PointType.Should().Be(expectedPointType);
                }
            }
        }

        /// <summary>
        /// Tests that the CreateFromFile method throws the correct exception if the
        /// CreateFromStringArray method throws an exception.
        /// </summary>
        [Fact]
        public void CreateFromFile_DataIssue_ThrowsException()
        {
            // Arrange
            var mazeData = new string[]
            {
                "####",
                "#  #",
                "#  #",
                "####",
            };
            var mazeFilePath = "testMaze1.txt";
            File.WriteAllLines(mazeFilePath, mazeData);
            var factory = new MazeFactory();

            // Act
            var action = () => factory.CreateFromFile(mazeFilePath);

            // Assert
            var ex = Assert.Throws<ArgumentException>(action);
            ex.Message.Should().Contain($"The file '{mazeFilePath}' does not represent a valid maze");
            ex.Message.Should().Contain("Found a 2x2 square of corridors at (1, 1)");
            ex.Message.Should().Contain("This cannot be rendered correctly in the 3D view");
        }

        /// <summary>
        /// Tests that CreateFromStringArray creates a maze with the correct dimensions and points.
        /// </summary>
        [Fact]
        public void CreateFromStringArray_CreatesCorrectMaze()
        {
            // Arrange
            var mazeData = new string[]
            {
                "#########",
                "#       #",
                "# ##### #",
                "# #   # #",
                "# # # # #",
                "#   #   #",
                "#########",
            };
            var factory = new MazeFactory();

            // Act
            var maze = factory.CreateFromStringArray(mazeData);

            // Assert
            Assert.NotNull(maze);
            Assert.Equal(7, maze.Height);
            Assert.Equal(9, maze.Width);
            for (var y = 0; y < maze.Height; y++)
            {
                for (var x = 0; x < maze.Width; x++)
                {
                    var expectedPointType = mazeData[y][x] == '#' ? MazePointType.Wall : MazePointType.Path;
                    maze.GetMazePoint(x, y).PointType.Should().Be(expectedPointType);
                }
            }
        }

        /// <summary>
        /// Tests that CreateFromStringArray throws the correct exception if the lines in the input
        /// array are not all the same length..
        /// </summary>
        [Fact]
        public void CreateFromStringArray_LinesAreNotSameLength_Throws()
        {
            // Arrange
            var mazeData = new string[]
            {
                "#########",
                "#       #",
                "# ##### #",
                "# #   # #",
                "# # # # #",
                "#   #   ",
                "#########",
            };
            var factory = new MazeFactory();

            // Act
            var action = () => factory.CreateFromStringArray(mazeData);

            // Assert
            var ex = Assert.Throws<ArgumentException>(action);
            ex.Message.Should().Contain("All lines must be the same length");
            ex.Message.Should().Contain("Lengths of supplied lines are 9 9 9 9 9 8 9");
        }

        /// <summary>
        /// Tests that the CreateFromStringArray method throws the correct exception if the maze data
        /// contains too many adjacent corridors (empty spaces).
        /// </summary>
        [Fact]
        public void CreateFromStringArray_TooMuchEmptySpace_Throws()
        {
            // Arrange
            var mazeData = new string[]
            {
                "####",
                "#  #",
                "#  #",
                "####",
            };
            var factory = new MazeFactory();

            // Act
            var action = () => factory.CreateFromStringArray(mazeData);

            // Assert
            var ex = Assert.Throws<ArgumentException>(action);
            ex.Message.Should().Contain("Found a 2x2 square of corridors at (1, 1)");
            ex.Message.Should().Contain("This cannot be rendered correctly in the 3D view");
        }
    }
}
