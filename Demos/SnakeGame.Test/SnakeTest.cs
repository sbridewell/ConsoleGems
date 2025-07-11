// <copyright file="SnakeTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace SnakeGame.Test
{
    using FluentAssertions;
    using Sde.ConsoleGems.Text;
    using Sde.SnakeGame;

    /// <summary>
    /// Unit tests for the <see cref="Snake"/> class.
    /// </summary>
    public class SnakeTest
    {
        /// <summary>
        /// Tests that the <see cref="Snake.HeadPosition"/> property returns a zero position when the snake has no
        /// segments.
        /// </summary>
        [Fact]
        public void HeadPosition_SnakeHasNoSegments_ReturnsZeroPosition()
        {
            // Arrange
            var snake = new Snake();

            // Act
            var headPosition = snake.HeadPosition;

            // Assert
            headPosition.Should().Be(new ConsolePoint(0, 0));
        }

        /// <summary>
        /// Tests that the <see cref="Snake.TailPosition"/> property returns a zero position when the snake has no
        /// segments.
        /// </summary>
        [Fact]
        public void TailPosition_SnakeHasNoSegments_ReturnsZeroPosition()
        {
            // Arrange
            var snake = new Snake();

            // Act
            var tailPosition = snake.TailPosition;

            // Assert
            tailPosition.Should().Be(new ConsolePoint(0, 0));
        }

        /// <summary>
        /// Test case for the Initialise method.
        /// </summary>
        [Fact]
        public void Initialise_HappyPath()
        {
            // Arrange
            var snake = new Snake();
            var initialPosition = new ConsolePoint(5, 5);

            // Act
            snake.Initialise(initialPosition);

            // Assert
            snake.Length.Should().Be(1);
            snake.HeadPosition.Should().Be(initialPosition);
            snake.TailPosition.Should().Be(initialPosition);
        }

        /// <summary>
        /// Happy path test for changing the snake's direction of travel.
        /// </summary>
        /// <param name="direction">The new direction.</param>
        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]
        public void ChangeDirection_HappyPath(Direction direction)
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = (Direction)99;

            // Act
            snake.ChangeDirection(direction);

            // Assert
            snake.CurrentDirection.Should().Be(direction);
        }

        /// <summary>
        /// Tests that the ChangeDirection method does not change the direction
        /// to the opposite of the current direction.
        /// </summary>
        /// <param name="originalDirection">The original direction.</param>
        /// <param name="newDirection">The new direction.</param>
        [Theory]
        [InlineData(Direction.Up, Direction.Down)]
        [InlineData(Direction.Down, Direction.Up)]
        [InlineData(Direction.Left, Direction.Right)]
        [InlineData(Direction.Right, Direction.Left)]
        public void ChangeDirection_OppositeDirection_ShouldNotChangeDirection(Direction originalDirection, Direction newDirection)
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = originalDirection;

            // Act
            snake.ChangeDirection(newDirection);

            // Assert
            snake.CurrentDirection.Should().Be(originalDirection);
        }

        /// <summary>
        /// Happy path test for moving the snake forward in the current direction.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        /// <param name="newX">The expected new x coordinate.</param>
        /// <param name="newY">The expected new y coordinate.</param>
        [Theory]
        [InlineData(Direction.Up, 5, 4)]
        [InlineData(Direction.Down, 5, 6)]
        [InlineData(Direction.Left, 4, 5)]
        [InlineData(Direction.Right, 6, 5)]
        public void MoveForward_HappyPath(Direction direction, int newX, int newY)
        {
            // Arrange
            var snake = new Snake();
            var initialPosition = new ConsolePoint(5, 5);
            snake.Initialise(initialPosition);
            snake.CurrentDirection = direction;
            var expectedNewPosition = new ConsolePoint(newX, newY);

            // Act
            snake.MoveForward();

            // Assert
            snake.HeadPosition.Should().Be(expectedNewPosition);
            snake.Length.Should().Be(2);
            snake.TailPosition.Should().Be(initialPosition);
        }

        /// <summary>
        /// Tests that the MoveForward method throws an exception when the direction is invalid.
        /// </summary>
        [Fact]
        public void MoveForward_InvalidDirection_Throws()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = (Direction)99; // Invalid direction

            // Act
            var action = () => snake.MoveForward();

            // Assert
            var ex = action.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Be("Invalid direction");
        }

        /// <summary>
        /// Tests that the TrimTail method removes the trailing segment of the snake.
        /// </summary>
        [Fact]
        public void TrimTail_TrimsTailSegment()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = Direction.Up;
            snake.MoveForward(); // Move to (5, 4)
            snake.MoveForward(); // Move to (5, 3)

            // Act
            snake.TrimTail();

            // Assert
            snake.Length.Should().Be(2); // Initial position + 1 move
            snake.TailPosition.Should().Be(new ConsolePoint(5, 4)); // Tail should be at (5, 4)
        }

        /// <summary>
        /// Test case for the IsWithin method to check if the snake is within a rectangle.
        /// </summary>
        [Fact]
        public void IsWithin_ReturnsCorrectValue()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = Direction.Up;
            snake.MoveForward(); // Move to (5, 4)
            var rectangle = new ConsoleRectangle(new ConsolePoint(4, 3), new ConsoleSize(3, 3)); // Rectangle covering (4, 3) to (6, 5)

            // Act
            var result = snake.IsWithin(rectangle);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Test case for the OccupiesPoint method to check if the snake occupies a specific point.
        /// </summary>
        [Fact]
        public void OccupiesPoint_ReturnsCorrectValue()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = Direction.Up;
            snake.MoveForward(); // Move to (5, 4)
            var pointToTest = new ConsolePoint(5, 4);

            // Act
            var result = snake.OccupiesPoint(pointToTest);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the length property returns the correct number of segments in the snake.
        /// </summary>
        [Fact]
        public void Length_ReturnsNumberOfSegments()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.MoveForward(); // Move to (5, 4)
            snake.MoveForward(); // Move to (5, 3)

            // Act
            var length = snake.Length;

            // Assert
            length.Should().Be(3); // Initial position + 2 moves
        }

        /// <summary>
        /// Tests that the HasRunIntoOwnTail property returns true when the snake runs into its own tail.
        /// </summary>
        [Fact]
        public void HasRunIntoOwnTail_ReturnsTrue_WhenRunningIntoTail()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = Direction.Up;
            snake.MoveForward(); // Move to (5, 4)
            snake.MoveForward(); // Move to (5, 3)
            snake.ChangeDirection(Direction.Right);
            snake.MoveForward(); // Move to (6, 3)
            snake.ChangeDirection(Direction.Down);
            snake.MoveForward(); // Move to (6, 4)
            snake.ChangeDirection(Direction.Left);
            snake.MoveForward(); // Move to (5, 4) - runs into its own tail

            // Act
            var hasRunIntoTail = snake.HasRunIntoOwnTail;

            // Assert
            hasRunIntoTail.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the HasRunIntoOwnTail property returns false when the snake is not running into its own tail.
        /// </summary>
        [Fact]
        public void HasRunIntoOwnTail_ReturnsFalse_WhenNotRunningIntoTail()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));
            snake.CurrentDirection = Direction.Up;
            snake.MoveForward(); // Move to (5, 4)
            snake.MoveForward(); // Move to (5, 3)

            // Act
            var hasRunIntoTail = snake.HasRunIntoOwnTail;

            // Assert
            hasRunIntoTail.Should().BeFalse();
        }

        /// <summary>
        /// Tests that the HasRunIntoOwnTail property returns false when the snake has only one segment.
        /// </summary>
        [Fact]
        public void HasRunIntoOwnTail_ReturnsFalse_WhenSnakeHasOnlyOneSegment()
        {
            // Arrange
            var snake = new Snake();
            snake.Initialise(new ConsolePoint(5, 5));

            // Act
            var hasRunIntoTail = snake.HasRunIntoOwnTail;

            // Assert
            hasRunIntoTail.Should().BeFalse();
        }
    }
}
