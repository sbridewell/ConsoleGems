// <copyright file="MazeGameControllerTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Test
{
    using FluentAssertions;
    using Moq;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.KeyPressHandlers;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Map;
    using Sde.MazeGame.Painters.Pov;
    using Sde.MazeGame.Painters.Status;
    using Xunit.Sdk;

    /// <summary>
    /// Unit tests for the <see cref="GameController"/> class.
    /// </summary>
    public class MazeGameControllerTest
    {
        private record MoveForwardTestCase(Maze maze, ConsolePoint playerPosition, Direction facingDirection);

        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IStatusPainter> mockStatusPainter = new ();
        private readonly Mock<IMazePainterMap> mockMazePainterMap = new ();
        private readonly Mock<IMazePainterPov> mockMazePainterPov = new ();
        private readonly Mock<ILimitOfViewProvider> mockLimitOfViewProvider = new ();
        private readonly MazeVisibilityUpdater mazeVisibilityUpdater;
        private readonly Mock<IMazeGameRandomiser> mockMazeGameRandomiser = new ();
        private readonly MazeGameOptions options = new MazeGameOptions()
            .WithMapViewOrigin(0, 0)
            .WithPovViewOrigin(1, 1)
            .WithStatusOrigin(2, 2)
            .WithMazeDataFile(@"MazeData\40x20.maze.txt");

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGameControllerTest"/> class.
        /// </summary>
        public MazeGameControllerTest()
        {
            this.mockLimitOfViewProvider.SetupGet(p => p.VisibleDistance).Returns(5);
            this.mockLimitOfViewProvider.SetupGet(p => p.LimitOfView).Returns(new List<ConsolePointOffset>()
            {
                new (1, 0),
                new (0, 1),
                new (-1, 0),
                new (0, -1),
                new (1, 1),
                new (-1, -1),
                new (1, -1),
                new (-1, 1),
            });
            this.mazeVisibilityUpdater = new MazeVisibilityUpdater(this.mockLimitOfViewProvider.Object);
        }

        /// <summary>
        /// Gets the names of the test cases for the player being unable to move forward.
        /// </summary>
        public static TheoryData<string> PlayerCannotMoveForwardTestCaseNames
            => new (PlayerCannotMoveForwardTestData.Keys.ToList());

        /// <summary>
        /// Gets the names of the test cases for the player being able to move forward.
        /// </summary>
        public static TheoryData<string> PlayerCanMoveForwardTestCaseNames
            => new (PlayerCanMoveForwardTestData.Keys.ToList());

        private static Dictionary<string, MoveForwardTestCase> PlayerCannotMoveForwardTestData =>
            new Dictionary<string, MoveForwardTestCase>
            {
                ["Player facing North"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(1, 1),
                    Direction.North),
                ["Player facing South"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(1, 1),
                    Direction.South),
                ["Player facing East"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(1, 1),
                    Direction.East),
                ["Player facing West"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(1, 1),
                    Direction.West),
            };

        private static Dictionary<string, MoveForwardTestCase> PlayerCanMoveForwardTestData =>
            new Dictionary<string, MoveForwardTestCase>
            {
                ["Player facing North"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(38, 2),
                    Direction.North),
                ["Player facing South"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(38, 1),
                    Direction.South),
                ["Player facing East"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(1, 1),
                    Direction.East),
                ["Player facing West"] = new MoveForwardTestCase(
                    new MazeFactory().CreateFromFile(@"MazeData\3x3.maze.txt"),
                    new ConsolePoint(1, 1),
                    Direction.West),
            };

        /// <summary>
        /// Tests that the Initialise method sets up the controller correctly.
        /// </summary>
        [Fact]
        public void Initialise_ShouldInitialiseControllerCorrectly()
        {
            // Arrange
            var controller = this.InstantiateController();

            // Act
            controller.Initialise(this.options);

            // TODO: Assert
            this.mockConsole.Verify(m => m.Clear(), Times.Once);
            controller.CurrentGame.Should().NotBeNull();
            controller.CurrentGame.Player.Should().NotBeNull();
            controller.CurrentGame.Maze.Should().NotBeNull();
            controller.CurrentGame.Maze.Width.Should().Be(40);
            controller.CurrentGame.Maze.Height.Should().Be(20);
            this.mockMazePainterMap.VerifySet(m => m.Origin = this.options.MapViewOrigin, Times.Once);
            this.mockMazePainterMap.VerifySet(m => m.InnerSize = new ConsoleSize(40, 20), Times.Once);
            this.mockMazePainterMap.VerifySet(m => m.HasBorder = true, Times.Once);
            this.mockMazePainterPov.VerifySet(m => m.Origin = this.options.PovViewOrigin, Times.Once);
            this.mockMazePainterPov.VerifySet(m => m.HasBorder = true, Times.Once);
            this.mockStatusPainter.VerifySet(m => m.Origin = this.options.StatusOrigin, Times.Once);
            this.mockStatusPainter.VerifySet(m => m.InnerSize = new ConsoleSize(this.mockConsole.Object.WindowWidth - this.options.StatusOrigin.X - 2, 1), Times.Once);
            this.mockStatusPainter.VerifySet(m => m.HasBorder = true, Times.Once);
            this.mockMazePainterMap.Verify(m => m.Paint(controller.CurrentGame.Maze, controller.CurrentGame.Player), Times.Exactly(2));
            this.mockMazePainterPov.Verify(m => m.Render(controller.CurrentGame.Maze, controller.CurrentGame.Player), Times.Once);
            this.mockConsole.VerifySet(m => m.CursorVisible = false, Times.Once);
        }

        /// <summary>
        /// Exercises all the main paths through the Play method.
        /// </summary>
        [Fact]
        public void Play_HappyPath()
        {
            // Arrange
            this.mockMazeGameRandomiser.Setup(m => m.GetPosition(It.IsAny<Maze>())).Returns(new ConsolePoint(4, 4));
            this.mockConsole.SetupSequence(m => m.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.A, false, false, false)) // unrecognised key
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false)) // recognised key
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.Q, false, false, false)); // quit key
            var controller = this.InstantiateController();
            controller.Initialise(this.options);

            // Act
            controller.Play();

            // Assert
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Use left and right arrows to turn, up arrow to move forward and Q to quit.")),
                    ConsoleOutputType.Error), Times.Once);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Find your way out of the maze! You are facing " + controller.CurrentGame!.Player.FacingDirection)),
                    ConsoleOutputType.Default), Times.AtLeastOnce);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Thank you for playing")),
                    ConsoleOutputType.Default), Times.Once);
        }

        /// <summary>
        /// Tests that the TurnPlayerLeft method updates the player's direction correctly.
        /// </summary>
        /// <param name="originalDirection">The direction that the player starts out facing.</param>
        /// <param name="expectedNewDirection">The direction that the player should be facing after turning left.</param>
        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        [InlineData((Direction)99, (Direction)99)] // Test with an invalid direction
        public void TurnPlayerLeft_ChangePlayerDirectionAndWritesStatusMessage(Direction originalDirection, Direction expectedNewDirection)
        {
            // Arrange
            this.mockMazeGameRandomiser.Setup(m => m.GetDirection()).Returns(originalDirection);
            var controller = this.InstantiateController();
            controller.Initialise(this.options);
            var game = controller.CurrentGame!;

            // Act
            controller.TurnPlayerLeft();

            // Assert
            game.Player.FacingDirection.Should().Be(expectedNewDirection);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Find your way out of the maze! You are facing " + expectedNewDirection)),
                    ConsoleOutputType.Default),
                Times.Exactly((int)originalDirection == 99 ? 2 : 1));
            this.mockMazePainterPov.Verify(m => m.Render(It.IsAny<Maze>(), It.IsAny<Player>()), Times.Exactly(2));
            this.mockMazePainterMap.Verify(m => m.Paint(It.IsAny<Maze>(), It.IsAny<Player>()), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that the TurnPlayerRight method updates the player's direction correctly.
        /// </summary>
        /// <param name="originalDirection">The direction that the player starts out facing.</param>
        /// <param name="expectedNewDirection">The direction that the player should be facing after turning right.</param>
        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.West, Direction.North)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData((Direction)99, (Direction)99)] // Test with an invalid direction
        public void TurnPlayerRight_ChangePlayerDirectionAndWritesStatusMessage(Direction originalDirection, Direction expectedNewDirection)
        {
            // Arrange
            this.mockMazeGameRandomiser.Setup(m => m.GetDirection()).Returns(originalDirection);
            var controller = this.InstantiateController();
            controller.Initialise(this.options);
            var game = controller.CurrentGame!;

            // Act
            controller.TurnPlayerRight();

            // Assert
            game.Player.FacingDirection.Should().Be(expectedNewDirection);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Find your way out of the maze! You are facing " + expectedNewDirection)),
                    ConsoleOutputType.Default),
                Times.Exactly((int)originalDirection == 99 ? 2 : 1));
            this.mockMazePainterPov.Verify(m => m.Render(It.IsAny<Maze>(), It.IsAny<Player>()), Times.Exactly(2));
            this.mockMazePainterMap.Verify(m => m.Paint(It.IsAny<Maze>(), It.IsAny<Player>()), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that the TryToMovePlayerForward method doesn't move the player when a wall is in the way.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(PlayerCannotMoveForwardTestCaseNames))]
        public void TryToMovePlayerForward_WallInTheWay_DoesNotMove(string testCaseName)
        {
            // Arrange
            var testCase = PlayerCannotMoveForwardTestData[testCaseName];
            var controller = this.InstantiateController();
            var myOptions = new MazeGameOptions()
                .WithMapViewOrigin(0, 2)
                .WithPovViewOrigin(10, 2)
                .WithStatusOrigin(0, 0)
                .WithMazeDataFile(@"MazeData\3x3.maze.txt");
            controller.Initialise(myOptions);
            controller.CurrentGame!.Player.Position = testCase.playerPosition;
            controller.CurrentGame.Player.FacingDirection = testCase.facingDirection;

            // Act
            controller.TryToMovePlayerForward();

            // Assert
            controller.CurrentGame.Player.Position.Should().Be(testCase.playerPosition);
            controller.CurrentGame.Player.FacingDirection.Should().Be(testCase.facingDirection);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("You can't move forward, there's a wall in the way.")),
                    ConsoleOutputType.Error),
                Times.Once);
        }

        /// <summary>
        /// Tests that when the player is facing in an invalid direction, the TryToMovePlayerForward
        /// method does not move the player.
        /// </summary>
        [Fact]
        public void TryToMovePlayerForward_InvalidDirection_DoesNotMove()
        {
            // Arrange
            var controller = this.InstantiateController();
            controller.Initialise(this.options);
            controller.CurrentGame!.Player.Position = new ConsolePoint(1, 1);
            controller.CurrentGame.Player.FacingDirection = (Direction)99; // Invalid direction

            // Act
            controller.TryToMovePlayerForward();

            // Assert
            controller.CurrentGame.Player.Position.Should().Be(new ConsolePoint(1, 1));
            controller.CurrentGame.Player.FacingDirection.Should().Be((Direction)99);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Find your way out of the maze! You are facing 99")),
                    ConsoleOutputType.Default),
                Times.Once);
        }

        /// <summary>
        /// Tests that the TryToMovePlayerForward method moves the player when no wall is in the way.
        /// </summary>
        /// <param name="testCaseName">Name of the test case.</param>
        [Theory]
        [MemberData(nameof(PlayerCanMoveForwardTestCaseNames))]
        public void TryToMovePlayerForward_Success_MovesPlayer(string testCaseName)
        {
            // Arrange
            var testCase = PlayerCanMoveForwardTestData[testCaseName];
            var controller = this.InstantiateController();
            controller.Initialise(this.options);
            controller.CurrentGame!.Player.Position = testCase.playerPosition;
            controller.CurrentGame.Player.FacingDirection = testCase.facingDirection;

            // Act
            controller.TryToMovePlayerForward();

            // Assert
            controller.CurrentGame.Player.Position.Should().NotBe(testCase.playerPosition);
            controller.CurrentGame.Player.Position.X.Should().BeInRange(testCase.playerPosition.X - 1, testCase.playerPosition.X + 1);
            controller.CurrentGame.Player.Position.Y.Should().BeInRange(testCase.playerPosition.Y - 1, testCase.playerPosition.Y + 1);
            controller.CurrentGame.Player.FacingDirection.Should().Be(testCase.facingDirection);
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Find your way out of the maze! You are facing " + testCase.facingDirection.ToString())),
                    ConsoleOutputType.Default),
                Times.AtLeastOnce);
        }

        /// <summary>
        /// Tests that the game ends when the TryToMoveForward method results in the player escaping the maze.
        /// </summary>
        [Fact]
        public void TryToMovePlayerForward_EscapesMaze_GameEnds()
        {
            // Arrange
            this.mockConsole.Setup(m => m.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.Enter, false, false, false));
            var controller = this.InstantiateController();
            controller.Initialise(this.options);
            controller.CurrentGame!.Player.Position = new ConsolePoint(0, 1);
            controller.CurrentGame.Player.FacingDirection = Direction.West;

            // Act
            controller.TryToMovePlayerForward();

            // Assert
            this.mockStatusPainter.Verify(
                m => m.Paint(
                    It.Is<string>(s => s.Contains("Congratulations, you have escaped the maze!")),
                    ConsoleOutputType.Prompt),
                Times.Once);
            this.mockConsole.Verify(m => m.Clear(), Times.Once);
            this.mockMazePainterMap.Verify(m => m.Reset(), Times.Once);
            this.mockMazePainterMap.Verify(m => m.Reset(), Times.Once);
            this.mockStatusPainter.Verify(m => m.Reset(), Times.Once);
        }

        /// <summary>
        /// Tests that the Quit method exits the game.
        /// </summary>
        [Fact]
        public void Quit_ExitsGame()
        {
            // Arrange
            var controller = this.InstantiateController();
            controller.Initialise(this.options);

            // Act
            controller.Quit();

            // Assert
            this.mockConsole.VerifySet(m => m.CursorTop = controller.CurrentGame!.Maze.Height + 1); // TODO: probably not the right height
            this.mockStatusPainter.Verify(m => m.Paint("Thank you for playing!", ConsoleOutputType.Default));
            controller.CurrentGame!.Status.Should().Be(MazeGameStatus.Lost);
            this.mockMazePainterMap.Verify(m => m.Reset(), Times.Once);
            this.mockMazePainterPov.Verify(m => m.Reset(), Times.Once);
            this.mockStatusPainter.Verify(m => m.Reset(), Times.Once);
        }

        /// <summary>
        /// Tests that the Play method throws the correct exception if the game has not been initialised.
        /// </summary>
        [Fact]
        public void Play_GameNotInitialised_Throws()
        {
            // Arrange
            var controller = this.InstantiateController();

            // Act
            Action act = () => controller.Play();

            // Assert
            var ex = act.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Contain("The game has not been initialised.");
        }

        /// <summary>
        /// Tests that the TurnPlayerLeft method throws the correct exception if the game has not been initialised.
        /// </summary>
        [Fact]
        public void TurnPlayerLeft_GameNotInitialised_Throws()
        {
            // Arrange
            var controller = this.InstantiateController();

            // Act
            Action act = () => controller.TurnPlayerLeft();

            // Assert
            var ex = act.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Contain("The game has not been initialised.");
        }

        /// <summary>
        /// Tests that the TurnPlayerRight method throws the correct exception if the game has not been initialised.
        /// </summary>
        [Fact]
        public void TurnPlayerRight_GameNotInitialised_Throws()
        {
            // Arrange
            var controller = this.InstantiateController();

            // Act
            Action act = () => controller.TurnPlayerRight();

            // Assert
            var ex = act.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Contain("The game has not been initialised.");
        }

        /// <summary>
        /// Tests that the TryToMovePlayerForward method throws the correct exception if the game has not been initialised.
        /// </summary>
        [Fact]
        public void TryToMovePlayerForward_GameNotInitialised_Throws()
        {
            // Arrange
            var controller = this.InstantiateController();

            // Act
            Action act = () => controller.TryToMovePlayerForward();

            // Assert
            var ex = act.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Contain("The game has not been initialised.");
        }

        /// <summary>
        /// Tests that the Quit method throws the correct exception if the game has not been initialised.
        /// </summary>
        [Fact]
        public void Quit_GameNotInitialised_Throws()
        {
            // Arrange
            var controller = this.InstantiateController();

            // Act
            Action act = () => controller.Quit();

            // Assert
            var ex = act.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Contain("The game has not been initialised.");
        }

        private GameController InstantiateController()
        {
            var controller = new GameController(
                this.mockConsole.Object,
                this.mockStatusPainter.Object,
                this.mockMazePainterMap.Object,
                this.mockMazePainterPov.Object,
                this.mazeVisibilityUpdater,
                new MazeGameKeyPressMappings(),
                this.mockMazeGameRandomiser.Object);
            return controller;
        }
    }
}
