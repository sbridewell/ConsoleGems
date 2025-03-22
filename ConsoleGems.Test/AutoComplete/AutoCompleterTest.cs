// <copyright file="AutoCompleterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete
{
    /// <summary>
    /// Unit tests for the <see cref="AutoCompleter"/> class.
    /// </summary>
    public class AutoCompleterTest
    {
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IAutoCompleteMatcher> mockMatcher = new ();
        private readonly DummyKeyPressMappings dummyAutoCompleteKeyPressMappings = new ();

        private readonly List<string> suggestions =
        [
            "apple",
            "banana",
            "cherry",
            "date",
            "elderberry",
            "fig",
            "grape",
            "honeydew",
            "kiwi",
            "lemon",
            "mango",
            "nectarine",
            "orange",
            "pear",
            "quince",
            "raspberry",
            "strawberry",
            "tangerine",
            "ugli",
            "vanilla",
            "watermelon",
            "ximenia",
            "yuzu",
            "zucchini",
        ];

        /// <summary>
        /// Gets sequences of console keypresses, including special keys, to use
        /// as test data, along with any text which should be in the clipboard prior
        /// to the test and the user input that should be returned by the console.
        /// </summary>
        public static TheoryData<ConsoleKeyInfoWrapper[], string, string> SpecialKeysTestData
        {
            get
            {
                var theoryData = new TheoryData<ConsoleKeyInfoWrapper[], string, string>
                {
                    // An example of basic typing
                    {
                        [
                            new ('a', ConsoleKey.NoName),
                            new ('p', ConsoleKey.NoName),
                            new ('p', ConsoleKey.NoName),
                            new ('l', ConsoleKey.NoName),
                            new ('e', ConsoleKey.NoName),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty, // clipboard
                        "apple" // expected user input
                    },

                    // Entering a capital letter
                    {
                        [
                            new ('B', ConsoleKey.None),
                            new ('a', ConsoleKey.None),
                            new ('n', ConsoleKey.None),
                            new ('a', ConsoleKey.None),
                            new ('n', ConsoleKey.None),
                            new ('a', ConsoleKey.None),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "Banana"
                    },

                    // Use left arrow to add a missing letter
                    {
                        [
                            new ('c', ConsoleKey.None),
                            new ('t', ConsoleKey.None),
                            new (' ', ConsoleKey.LeftArrow),
                            new ('a', ConsoleKey.None),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "cat"
                    },

                    // Use left and right arrow to add a missing letter
                    {
                        [
                            new ('d', ConsoleKey.None),
                            new ('g', ConsoleKey.None),
                            new (' ', ConsoleKey.LeftArrow),
                            new (' ', ConsoleKey.LeftArrow),
                            new (' ', ConsoleKey.RightArrow),
                            new ('o', ConsoleKey.None),
                            new (' ', ConsoleKey.RightArrow),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "dog"
                    },

                    // Using home and right arrow to add a missing letter
                    {
                        [
                            new ('e', ConsoleKey.None),
                            new ('d', ConsoleKey.None),
                            new (' ', ConsoleKey.Home),
                            new (' ', ConsoleKey.RightArrow),
                            new ('n', ConsoleKey.None),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "end"
                    },

                    // Using left arrow, end and left arrow to add a missing character
                    {
                        [
                            new ('f', ConsoleKey.None),
                            new ('i', ConsoleKey.None),
                            new ('h', ConsoleKey.None),
                            new (' ', ConsoleKey.LeftArrow),
                            new (' ', ConsoleKey.End),
                            new (' ', ConsoleKey.LeftArrow),
                            new ('s', ConsoleKey.None),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "fish"
                    },

                    // Using left arrow, and backspace to correct a character
                    {
                        [
                            new ('g', ConsoleKey.None),
                            new ('r', ConsoleKey.None),
                            new ('a', ConsoleKey.None),
                            new ('o', ConsoleKey.None),
                            new ('e', ConsoleKey.None),
                            new (' ', ConsoleKey.LeftArrow),
                            new (' ', ConsoleKey.Backspace),
                            new ('p', ConsoleKey.None),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "grape"
                    },

                    // Using left arrow and delete to correct a character
                    {
                        [
                            new ('h', ConsoleKey.None),
                            new ('o', ConsoleKey.None),
                            new ('n', ConsoleKey.None),
                            new ('e', ConsoleKey.None),
                            new ('y', ConsoleKey.None),
                            new ('d', ConsoleKey.None),
                            new ('r', ConsoleKey.None),
                            new ('w', ConsoleKey.None),
                            new (' ', ConsoleKey.LeftArrow),
                            new (' ', ConsoleKey.LeftArrow),
                            new (' ', ConsoleKey.Delete),
                            new ('e', ConsoleKey.None),
                            new (' ', ConsoleKey.Enter),
                        ],
                        string.Empty,
                        "honeydew"
                    },

                    // Using control-v to paste text
                    {
                        [
                            new ('l', ConsoleKey.None, false, false, false),
                            new ('e', ConsoleKey.None, false, false, false),
                            new ('n', ConsoleKey.None, false, false, false),
                            new (' ', ConsoleKey.LeftArrow, false, false, false),
                            new (' ', ConsoleKey.V, false, false, control: true),
                            new (' ', ConsoleKey.Enter, false, false, false),
                        ],
                        "mo", // clipboard
                        "lemon" // expected user input
                    },

                    // Using tab with some previous input to select next suggestion
                    {
                        [
                            new ('m', ConsoleKey.None, false, false, false),
                            new (' ', ConsoleKey.Tab, false, false, false),
                            new (' ', ConsoleKey.Enter, false, false, false),
                        ],
                        string.Empty, // clipboard
                        "mango" // expected user input
                    },

                    // Using tab twice with some previous input to select next suggestion but one
                    {
                        [
                            new ('m', ConsoleKey.None, false, false, false),
                            new (' ', ConsoleKey.Tab, false, false, false),
                            new (' ', ConsoleKey.Tab, false, false, false),
                            new (' ', ConsoleKey.Enter, false, false, false),
                        ],
                        string.Empty, // clipboard
                        "nectarine" // expected user input
                    },

                    // Using tab then shift-tab with no previous input to select last suggestion
                    {
                        [
                            new ('p', ConsoleKey.None, false, false, false),
                            new (' ', ConsoleKey.Tab, false, false, false),
                            new (' ', ConsoleKey.Tab, shift: true, false, false),
                            new (' ', ConsoleKey.Enter, false, false, false),
                        ],
                        string.Empty, // clipboard
                        "orange" // expected user input
                    },
                };

                return theoryData;
            }
        }

        /// <summary>
        /// Gets a mock <see cref="IConsole"/>.
        /// </summary>
        protected Mock<IConsole> MockConsole => this.mockConsole;

        #region public test methods

        /// <summary>
        /// Tests that supplying a sequence of keypresses to the console results in
        /// the correct user input being returned.
        /// This is an integration test rather than a unit test, because it is
        /// dependent on the behaviour of the keypress handlers.
        /// </summary>
        /// <param name="keys">The keypresses to send to the console.</param>
        /// <param name="clipboardText">Text to be pasted from the clipboard.</param>
        /// <param name="expectedOutput">The expected user input.</param>
        //[Theory(Skip = "mark this as an integration test, skip it when measuring code coverage but still check that it passes")]
        [Theory]
        [Trait("Category", "Integration")]
        [MemberData(nameof(SpecialKeysTestData))]
        [SuppressMessage(
            "Minor Code Smell",
            "S6608:Prefer indexing instead of \"Enumerable\" methods on types implementing \"IList\"",
            Justification = "Easier to read, and performance is not an issue here")]
        public void IntegrationTest_BuildsCorrectUserInput(ConsoleKeyInfoWrapper[] keys, string clipboardText, string expectedOutput)
        {
            if (keys.Last().Key != ConsoleKey.Enter)
            {
                throw new ArgumentException($"The last keypress must be Enter otherwise the {nameof(AutoCompleter)} gets stuck in a loop. Last keypress was {keys.Last().Key}");
            }

            // Arrange
            this.mockConsole.Setup(c => c.WindowWidth).Returns(120);
            lock (LockObjects.ClipboardLock)
            {
                if (!string.IsNullOrEmpty(clipboardText))
                {
                    // Only set clipboard text if there's something to set,
                    // to avoid wiping the clipboard text set by another test
                    // which might be running at the same time.
                    TextCopy.ClipboardService.SetText(clipboardText);
                }

                this.SendKeysToConsole(keys);
                var keyPressMappings = new AutoCompleteKeyPressDefaultMappings();
                var matcher = new StartsWithMatcher();
                var autoCompleter = new AutoCompleter(keyPressMappings, matcher, this.mockConsole.Object);

                // Act
                var actualOutput = autoCompleter.ReadLine(this.suggestions, "Type something: ");

                // Assert
                actualOutput.Should().Be(expectedOutput);
            }
        }

        #region property tests

        /// <summary>
        /// Tests that the CursorIsAtHome property returns true when the cursor is at the home position.
        /// </summary>
        [Fact]
        public void CursorIsAtHome_CursorIsAtHome_ReturnsTrue()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToEnd();

            // Act
            autoCompleter.MoveCursorToHome();

            // Assert
            autoCompleter.CursorIsAtHome.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the CursorIsAtHome property returns false when the cursor is not at the home position.
        /// </summary>
        [Fact]
        public void CursorIsAtHome_CursorIsNotAtHome_ReturnsFalse()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToHome();

            // Act
            autoCompleter.MoveCursorRight();

            // Assert
            autoCompleter.CursorIsAtHome.Should().BeFalse();
        }

        /// <summary>
        /// Tests that the CursorIsAtEnd property returns true when the cursor is at the end of the user input.
        /// </summary>
        [Fact]
        public void CursorIsAtEnd_CursorIsAtEnd_ReturnsTrue()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToHome();

            // Act
            autoCompleter.MoveCursorToEnd();

            // Assert
            autoCompleter.CursorIsAtEnd.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the CursorIsAtEnd property returns false when the cursor is not at the end of the user input.
        /// </summary>
        [Fact]
        public void CursorIsAtEnd_CursorIsNotAtEnd_ReturnsFalse()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToEnd();

            // Act
            autoCompleter.MoveCursorLeft();

            // Assert
            autoCompleter.CursorIsAtEnd.Should().BeFalse();
        }

        #endregion

        #region ReadLine tests

        /// <summary>
        /// Tests that if the user input is long enough to wrap onto a second
        /// line, it is not truncated.
        /// </summary>
        [Fact]
        public void ReadLine_UserInputWraps_UserInputIsNotTruncated()
        {
            // Arrange
            this.mockConsole.Setup(m => m.WindowWidth).Returns(10);
            var prompt = "12345";
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = '6', Key = ConsoleKey.NoName },
                new () { Character = '7', Key = ConsoleKey.NoName },
                new () { Character = '8', Key = ConsoleKey.NoName },
                new () { Character = '9', Key = ConsoleKey.NoName },
                new () { Character = '0', Key = ConsoleKey.NoName },
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            var expectedOutput = "67890"; // last keypress has not been ignored
            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            this.SendKeysToConsole(userInput);

            // Act
            var actualOutput = autoCompleter.ReadLine([], prompt);

            // Assert
            actualOutput.Should().Be(expectedOutput);
        }

        /// <summary>
        /// Tests that the correct calls to keypress handlers are made.
        /// </summary>
        [Fact]
        public void ReadLine_CorrectKeyPressHandlerCallsAreMade()
        {
            // Arrange
            this.mockConsole.Setup(c => c.WindowWidth).Returns(120);
            var keys = new List<ConsoleKeyInfoWrapper>
            {
                new ('a', ConsoleKey.A),
                new ('b', ConsoleKey.B),
                new ('q', ConsoleKey.Q),
                new (' ', ConsoleKey.Enter),
            };
            var autoCompleter = new AutoCompleter(
                this.dummyAutoCompleteKeyPressMappings,
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var prompt = "What is your command? "; // note trailing space
            this.SendKeysToConsole(keys);

            // Act
            _ = autoCompleter.ReadLine(this.suggestions, prompt);

            // Assert
            this.mockConsole.Verify(c => c.ReadKey(It.IsAny<bool>()), Times.Exactly(keys.Count));

            var mockDefaultKeyPressHandler = this.dummyAutoCompleteKeyPressMappings.MockDefaultKeyPressHandler;
            var mockDummyKeyPressHandler = this.dummyAutoCompleteKeyPressMappings.MockDummyKeyPressHandler;

            mockDefaultKeyPressHandler.Verify(
                dkh => dkh.Handle(It.IsAny<ConsoleKeyInfo>(), autoCompleter),
                Times.Exactly(2));
            mockDummyKeyPressHandler.Verify(
                dkh => dkh.Handle(It.IsAny<ConsoleKeyInfo>(), autoCompleter),
                Times.Exactly(1));

            mockDefaultKeyPressHandler.Verify(
                dkh => dkh.Handle(
                    It.Is<ConsoleKeyInfo>(cki => cki.KeyChar == 'a'),
                    autoCompleter),
                Times.Exactly(1));
            mockDefaultKeyPressHandler.Verify(
                dkh => dkh.Handle(
                    It.Is<ConsoleKeyInfo>(cki => cki.KeyChar == 'b'),
                    autoCompleter),
                Times.Exactly(1));
            mockDummyKeyPressHandler.Verify(
                dkh => dkh.Handle(
                    It.Is<ConsoleKeyInfo>(cki => cki.Key == ConsoleKey.Q),
                    autoCompleter),
                Times.Exactly(1));
        }

        /// <summary>
        /// Tests that the ReadLine method writes the prompt to the console.
        /// </summary>
        [Fact]
        public void ReadLine_WritesPromptToConsole()
        {
            // Arrange
            this.mockConsole.Setup(m => m.WindowWidth).Returns(100);
            var keyPressMappings = this.dummyAutoCompleteKeyPressMappings;
            var autoCompleter = new AutoCompleter(keyPressMappings, this.mockMatcher.Object, this.mockConsole.Object);
            var prompt = "What is the meaning of life? ";
            var userInput = new List<ConsoleKeyInfoWrapper> { new () { Character = ' ', Key = ConsoleKey.Enter } };
            this.SendKeysToConsole(userInput);

            // Act
            autoCompleter.ReadLine(this.suggestions, prompt);

            // Assert
            this.mockConsole.Verify(c => c.Write(prompt, ConsoleOutputType.Prompt), Times.Once);
        }

        /// <summary>
        /// Tests that the ReadLine method writes the appropriate content to the console.
        /// </summary>
        [Fact]
        public void ReadLine_WritesCorrectOutputToConsole()
        {
            // Arrange
            var consoleWidth = 120;
            this.mockConsole.Setup(c => c.WindowWidth).Returns(consoleWidth);
            var prompt = "Who watches the watcher? ";

            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = 's', Key = ConsoleKey.NoName },
                new () { Character = 'o', Key = ConsoleKey.NoName },
                new () { Character = 'm', Key = ConsoleKey.NoName },
                new () { Character = 'e', Key = ConsoleKey.NoName },
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            this.SendKeysToConsole(userInput);

            // Act
            autoCompleter.ReadLine(this.suggestions, prompt);

            // Assert
            autoCompleter.UserInput.Should().Be("some");
            this.mockConsole.VerifySet(m => m.CursorVisible = false, Times.Exactly(4));
            this.mockConsole.Verify(c => c.Write('s', ConsoleOutputType.UserInput), Times.Once);
            this.mockConsole.Verify(c => c.Write('o', ConsoleOutputType.UserInput), Times.Once);
            this.mockConsole.Verify(c => c.Write('m', ConsoleOutputType.UserInput), Times.Once);
            this.mockConsole.Verify(c => c.Write('e', ConsoleOutputType.UserInput), Times.Once);
            this.mockConsole.Verify(m => m.WriteLine(string.Empty, ConsoleOutputType.Default), Times.Once);
            this.mockConsole.VerifySet(m => m.CursorVisible = true, Times.Exactly(4));
        }

        #endregion

        #region tests for updating the user input

        /// <summary>
        /// Tests that when the cursor is not at the end of the user input,
        /// the InsertUserInput method inserts text at the current cursor
        /// position and the cursor is moved to the end of the inserted
        /// text.
        /// </summary>
        [Fact]
        public void InsertUserInput_CursorIsNotAtEnd_InsertsUserInput()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorLeft();

            // Act
            autoCompleter.InsertUserInput("abc");

            // Assert
            autoCompleter.UserInput.Should().Be("12abc3");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(5);
        }

        /// <summary>
        /// Tests that when the cursor is at the end of the user input, the
        /// InsertUserInput method appends text to the end of the user input
        /// and the cursor is moved to the end of the inserted text.
        /// </summary>
        [Fact]
        public void InsertUserInput_CursorIsAtEnd_AppendsUserInput()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.InsertUserInput("abc");

            // Assert
            autoCompleter.UserInput.Should().Be("123abc");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(6);
        }

        /// <summary>
        /// Tests that when the cursor is not at the end of the user input, the
        /// RemoveCurrentCharacterFromUserInput method removes the character at
        /// the current cursor position, and the cursor remains in the same
        /// position.
        /// </summary>
        [Fact]
        public void RemoveCurrentCharacterFromUserInput_CursorIsNotAtEnd_RemovesCurrentCharacter()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorLeft();

            // Act
            autoCompleter.RemoveCurrentCharacterFromUserInput();

            // Assert
            autoCompleter.UserInput.Should().Be("12");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(2);
        }

        /// <summary>
        /// Tests that when the cursor is at the end of the user input, the
        /// RemoveCurrentCharacterFromUserInput method does nothing.
        /// </summary>
        [Fact]
        public void RemoveCurrentCharacterFromUserInput_CursorIsAtEnd_DoesNothing()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.RemoveCurrentCharacterFromUserInput();

            // Assert
            autoCompleter.UserInput.Should().Be("123");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(3);
        }

        /// <summary>
        /// Tests that when the cursor is not at the home position, the
        /// RemovePreviousCharacterFromUserInput method removes the character immediately
        /// before the cursor position and moves the cursor left by one position.
        /// </summary>
        [Fact]
        public void RemovePreviousCharacterFromUserInput_CursorIsNotAtHome_RemovesPreviousCharacter()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorLeft();

            // Act
            autoCompleter.RemovePreviousCharacterFromUserInput();

            // Assert
            autoCompleter.UserInput.Should().Be("13");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(1);
        }

        /// <summary>
        /// Tests that when the cursor is at the home position, the
        /// RemovePreviousCharacterFromUserInput method does nothing.
        /// </summary>
        [Fact]
        public void RemovePreviousCharacterFromUserInput_CursorIsAtHome_DoesNothing()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToHome();

            // Act
            autoCompleter.RemovePreviousCharacterFromUserInput();

            // Assert
            autoCompleter.UserInput.Should().Be("123");
            autoCompleter.CursorPositionWithinUserInput.Should().Be(0);
        }

        /// <summary>
        /// Tests that when the user input is replaced with a shorter string,
        /// the replacement text is displayed with trailing spaces to pad it
        /// out to the length of the replaced text, to ensure that all of the
        /// replaced text is overwritten.
        /// </summary>
        [Fact]
        public void ReplaceUserInputWith_ReplacementTextIsShorterThanUserInput_ReplacementTextIsPaddedWithSpaces()
        {
            // Arrange
            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = '1', Key = ConsoleKey.NoName },
                new () { Character = '2', Key = ConsoleKey.NoName },
                new () { Character = '3', Key = ConsoleKey.NoName },
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            this.SendKeysToConsole(userInput);
            autoCompleter.ReadLine(this.suggestions, string.Empty);

            // Act
            autoCompleter.ReplaceUserInputWith("12");

            // Assert
            autoCompleter.UserInput.Should().Be("12");
            this.mockConsole.Verify(
                mockConsole => mockConsole.Write("12 ", ConsoleOutputType.UserInput),
                Times.Once);
        }

        /// <summary>
        /// Tests that when the user input is replaced with a string which
        /// is not shorter than the user input, no extra spaces are displayed.
        /// </summary>
        [Fact]
        public void ReplaceUserInputWith_ReplacementTextNotShorterThanUserInput_ReplacementTextIsNotPadded()
        {
            // Arrange
            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = '1', Key = ConsoleKey.NoName },
                new () { Character = '2', Key = ConsoleKey.NoName },
                new () { Character = '3', Key = ConsoleKey.NoName },
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            this.SendKeysToConsole(userInput);
            autoCompleter.ReadLine(this.suggestions, string.Empty);

            // Act
            autoCompleter.ReplaceUserInputWith("abc");

            // Assert
            autoCompleter.UserInput.Should().Be("abc");
            this.mockConsole.Verify(
                mockConsole => mockConsole.Write("abc", ConsoleOutputType.UserInput),
                Times.Once);
        }

        #endregion

        #region tests for moving the cursor

        /// <summary>
        /// Tests that the MoveCursorLeft method moves the cursor
        /// left by 1 character.
        /// </summary>
        [Fact]
        public void MoveCursorLeft_1Character_MovesCursorBy1Character()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(3);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.MoveCursorLeft(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = 2, Times.Once);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is already at the home position,
        /// the MoveCursorLeft method does not move the cursor.
        /// </summary>
        [Fact]
        public void MoveCursorLeft_CursorIsAtHome_DoesNotMoveCursor()
        {
            // Arrange
            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            this.SendKeysToConsole(userInput);
            autoCompleter.ReadLine(this.suggestions, string.Empty);

            // Act
            autoCompleter.MoveCursorLeft(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = It.IsAny<int>(), Times.Never);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is in column zero, the
        /// MoveCursorLeft method moves the cursor to the end
        /// of the previous line.
        /// </summary>
        [Fact]
        public void MoveCursorLeft_AtColumnZero_MovesCursorToPreviousLine()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(0);
            this.mockConsole.Setup(m => m.CursorTop).Returns(1);
            this.mockConsole.Setup(m => m.WindowWidth).Returns(4);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.MoveCursorLeft(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = 3, Times.Once);
            this.mockConsole.VerifySet(m => m.CursorTop = 0, Times.Once);
        }

        /// <summary>
        /// Tests that the MoveCursorRight method moves the cursor
        /// right by 1 character.
        /// </summary>
        [Fact]
        public void MoveCursorRight_1Character_MovesCursorBy1Character()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(2);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToHome();

            // Act
            autoCompleter.MoveCursorRight(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = 3, Times.Once);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is at the end of user input,
        /// the MoveCursorRight method does not move the cursor.
        /// </summary>
        [Fact]
        public void MoveCursorRight_CursorIsAtEnd_DoesNotMoveCursor()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.MoveCursorRight(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = It.IsAny<int>(), Times.Never);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is in the last column, the
        /// MoveCursorRight method moves the cursor to column zero
        /// of the following line.
        /// </summary>
        [Fact]
        public void MoveCursorRight_AtLastColumn_MovesCursorToNextLine()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(3);
            this.mockConsole.Setup(m => m.CursorTop).Returns(0);
            this.mockConsole.Setup(m => m.WindowWidth).Returns(4);
            this.mockConsole.Setup(m => m.WindowHeight).Returns(2);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorToHome();

            // Act
            autoCompleter.MoveCursorRight(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = 0, Times.Once);
            this.mockConsole.VerifySet(m => m.CursorTop = 1, Times.Once);
        }

        /// <summary>
        /// Tests that the MoveCursorToHome method moves the
        /// cursor to the home position.
        /// </summary>
        [Fact]
        public void MoveCursorToHome_MovesCursorToHome()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(1);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.MoveCursorToHome();

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = 0, Times.Exactly(autoCompleter.UserInput.Length));
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is already at the home position, the
        /// MoveCursorToHome method does not move the cursor.
        /// </summary>
        [Fact]
        public void MoveCursorToHome_CursorAlreadyAtHome_DoesNotMoveCursor()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(0);
            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            this.SendKeysToConsole(userInput);
            autoCompleter.ReadLine(this.suggestions, string.Empty);
            autoCompleter.MoveCursorToHome();

            // Act
            autoCompleter.MoveCursorRight(1);

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = It.IsAny<int>(), Times.Never);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that the MoveCursorToEnd method moves the cursor to
        /// the end of the user input.
        /// </summary>
        [Fact]
        public void MoveCursorToEnd_MovesCursorToEnd()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(2);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.MoveCursorLeft();

            // Act
            autoCompleter.MoveCursorToEnd();

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = 3, Times.Once);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        /// <summary>
        /// Tests that when the cursor is already at the end of the
        /// user input, the MoveCursorToEnd method does not move the
        /// cursor.
        /// </summary>
        [Fact]
        public void MoveCursorToEnd_CursorAlreadyAtEnd_DoesNotMoveCursor()
        {
            // Arrange
            this.mockConsole.Setup(m => m.CursorLeft).Returns(2);
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.MoveCursorToEnd();

            // Assert
            this.mockConsole.VerifySet(m => m.CursorLeft = It.IsAny<int>(), Times.Never);
            this.mockConsole.VerifySet(m => m.CursorTop = It.IsAny<int>(), Times.Never);
        }

        #endregion

        #region tests for working with suggestions

        /// <summary>
        /// Tests that the SelectFirstMatchingSuggestion method calls the
        /// <see cref="IAutoCompleteMatcher"/> implementation.
        /// </summary>
        [Fact]
        public void SelectFirstMatchingSuggestion_CallsTheMatcher()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.SelectFirstMatchingSuggestion();

            // Assert
            this.mockMatcher.Verify(
                m => m.FindMatch(
                    "123",
                    this.suggestions,
                    StringComparison.InvariantCultureIgnoreCase),
                Times.Once);
        }

        /// <summary>
        /// Tests that when no suggestion is selected, the
        /// SelectFirstMatchingSuggestion method selects the first suggestion.
        /// </summary>
        [Fact]
        public void SelectNextSuggestion_NoSuggestionSelected_SelectsFirstSuggestion()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            autoCompleter.SelectNextSuggestion();

            // Assert
            autoCompleter.GetCurrentSuggestion().Should().Be(this.suggestions[0]);
        }

        /// <summary>
        /// Tests that when the first suggestion is already selected, the
        /// SelectNextSuggestion method selects the second suggestion.
        /// </summary>
        [Fact]
        public void SelectNextSuggestion_OnFirstSuggestion_SelectsSecondSuggestion()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.SelectFirstMatchingSuggestion();

            // Act
            autoCompleter.SelectNextSuggestion();

            // Assert
            autoCompleter.GetCurrentSuggestion().Should().Be(this.suggestions[1]);
        }

        /// <summary>
        /// Tests that when the final suggestion is already selected, the
        /// SelectNextSuggestion method selects the first suggestion.
        /// </summary>
        [Fact]
        public void SelectNextSuggestion_OnFinalSuggestion_SelectsFirstSuggestion()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            for (var i = 0; i < this.suggestions.Count; i++)
            {
                autoCompleter.SelectNextSuggestion();
            }

            // Act
            autoCompleter.SelectNextSuggestion();

            // Assert
            autoCompleter.GetCurrentSuggestion().Should().Be(this.suggestions[0]);
        }

        /// <summary>
        /// Tests that when the final suggestion is already selected, the
        /// SelectPreviousSuggestion method selects the penultimate suggestion.
        /// </summary>
        [Fact]
        public void SelectPreviousSuggestion_OnFinalSuggestion_SelectsPenultimateSuggestion()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            for (var i = 0; i < this.suggestions.Count; i++)
            {
                autoCompleter.SelectNextSuggestion();
            }

            // Act
            autoCompleter.SelectPreviousSuggestion();

            // Assert
            autoCompleter.GetCurrentSuggestion().Should().Be(this.suggestions[this.suggestions.Count - 2]);
        }

        /// <summary>
        /// Tests that when the first suggestion is already selected, the
        /// SelectPreviousSelection method selects the final suggestion.
        /// </summary>
        [Fact]
        public void SelectPreviousSuggestion_OnFirstSuggestion_SelectsFinalSuggestion()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.SelectNextSuggestion();

            // Act
            autoCompleter.SelectPreviousSuggestion();

            // Assert
            autoCompleter.GetCurrentSuggestion().Should().Be(this.suggestions[this.suggestions.Count - 1]);
        }

        /// <summary>
        /// Tests that the SelectSuggestion method selects the current suggestion.
        /// </summary>
        [Fact]
        public void GetCurrentSuggestion_ReturnsCurrentSuggestion()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            foreach (var expectedSuggestion in this.suggestions)
            {
                // Act
                autoCompleter.SelectNextSuggestion();
                var actualSuggestion = autoCompleter.GetCurrentSuggestion();

                // Assert
                actualSuggestion.Should().Be(expectedSuggestion);
            }
        }

        /// <summary>
        /// Tests that when no suggestion is selected, the GetCurrentSuggestion
        /// method returns null.
        /// </summary>
        [Fact]
        public void GetCurrentSuggestion_NoSuggestionSelected_ReturnsNull()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();

            // Act
            var actualSuggestion = autoCompleter.GetCurrentSuggestion();

            // Assert
            actualSuggestion.Should().BeNull();
        }

        /// <summary>
        /// Tests that when the suggestions list is empty, the GetCurrentSuggestion
        /// method returns null.
        /// </summary>
        [Fact]
        public void GetCurrentSuggestion_AutoCompleterHasNoSuggestions_ReturnsNull()
        {
            // Arrange
            var autoCompleter = this.CreateAutoCompleterWithUserInput();
            autoCompleter.Suggestions.Clear();

            // Act
            var actualSuggestion = autoCompleter.GetCurrentSuggestion();

            // Assert
            actualSuggestion.Should().BeNull();
        }

        #endregion

        #endregion

        private AutoCompleter CreateAutoCompleterWithUserInput()
        {
            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                this.mockMatcher.Object,
                this.mockConsole.Object);
            var userInput = new List<ConsoleKeyInfoWrapper>
            {
                new () { Character = '1', Key = ConsoleKey.NoName },
                new () { Character = '2', Key = ConsoleKey.NoName },
                new () { Character = '3', Key = ConsoleKey.NoName },
                new () { Character = ' ', Key = ConsoleKey.Enter },
            };
            this.SendKeysToConsole(userInput);
            autoCompleter.ReadLine(this.suggestions, string.Empty);
            return autoCompleter;
        }

        private void SendKeysToConsole(IEnumerable<ConsoleKeyInfoWrapper> keys)
        {
            var keyPresses = new List<ConsoleKeyInfo>();
            foreach (var key in keys)
            {
                keyPresses.Add(new ConsoleKeyInfo(key.Character, key.Key, key.Shift, key.Alt, key.Control));
            }

            // https://stackoverflow.com/a/20294711/16563198
            var sequence = this.mockConsole.SetupSequence(c => c.ReadKey(It.IsAny<bool>()));
            foreach (var keyPress in keyPresses)
            {
                sequence = sequence.Returns(keyPress);
            }
        }

        /// <summary>
        /// Some rather arbitrary keypress mappings purely for the purposes of these unit tests.
        /// </summary>
        private class DummyKeyPressMappings : IAutoCompleteKeyPressMappings
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DummyKeyPressMappings"/> class.
            /// </summary>
            public DummyKeyPressMappings()
            {
                this.MockDummyKeyPressHandler = new Mock<IAutoCompleteKeyPressHandler>();
                this.MockDefaultKeyPressHandler = new Mock<IAutoCompleteKeyPressHandler>();
            }

            /// <summary>
            /// Gets some rather arbitrary keypress mappings purely for the purposes
            /// of these unit tests.
            /// </summary>
            public IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> Mappings
                => new Dictionary<ConsoleKey, IAutoCompleteKeyPressHandler>
            {
                { ConsoleKey.Q, this.MockDummyKeyPressHandler.Object },
            };

            /// <inheritdoc/>
            public IAutoCompleteKeyPressHandler DefaultHandler => this.MockDefaultKeyPressHandler.Object;

            /// <summary>
            /// Gets a non-default keypress handler.
            /// </summary>
            public Mock<IAutoCompleteKeyPressHandler> MockDummyKeyPressHandler { get; }

            /// <summary>
            /// Gets the default keypress handler.
            /// </summary>
            public Mock<IAutoCompleteKeyPressHandler> MockDefaultKeyPressHandler { get; }
        }
    }
}
