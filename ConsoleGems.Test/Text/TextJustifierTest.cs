// <copyright file="TextJustifierTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Text
{
    /// <summary>
    /// Unit tests for the <see cref="TextJustifier"/> class.
    /// </summary>
    public class TextJustifierTest
    {
        /// <summary>
        /// Tests that the correct exception is thrown when the Justify
        /// method is passed null text.
        /// </summary>
        [Fact]
        public void Justify_NullText_Throws()
        {
            // Arrange
            var justifier = new TextJustifier();

            // Act
            var action = () => justifier.Justify(null!, TextJustification.None, 100);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentNullException>().Which;
            ex.ParamName.Should().Be("text");
        }

        /// <summary>
        /// Tests that when the supplied justification is none, leading and
        /// trailing whitespace is trimmed and the resulting text is left
        /// justified with no trailing whitespace.
        /// </summary>
        [Fact]
        public void Justify_NoJustification_TrimsAndDoesNotJustify()
        {
            // Arrange
            var justifier = new TextJustifier();
            const string text = "  foo ";
            const int availableWidth = 20;
            var expectedText = "foo";
            var expectedBlock = new TextBlock(20);
            expectedBlock.InsertText(expectedText);

            // Act
            justifier.Justify(text, TextJustification.None, availableWidth);

            // Assert
            justifier.JustifiedText.Should().Be(expectedText);
            justifier.JustifiedLines.Should().ContainSingle().Which.ToString().Should().Be(expectedText);
            justifier.JustifiedTextBlock.Should().BeEquivalentTo(expectedBlock);
        }

        /// <summary>
        /// Tests that when the supplied justification is left, leading and
        /// trailing whitespace is trimmed and the resulting text is left
        /// justified and right padded with spaces to the required width.
        /// </summary>
        [Fact]
        public void Justify_LeftJustification_TrimsAndJustifiesLeft()
        {
            // Arrange
            var justifier = new TextJustifier();
            const string text = "  foo ";
            const int availableWidth = 20;
            var expectedText = "foo                 ";
            var expectedBlock = new TextBlock(20);
            expectedBlock.InsertText(expectedText);

            // Act
            justifier.Justify(text, TextJustification.Left, availableWidth);

            // Assert
            justifier.JustifiedText.Should().Be(expectedText);
            justifier.JustifiedLines.Should().ContainSingle().Which.ToString().Should().Be(expectedText);
            justifier.JustifiedTextBlock.Should().BeEquivalentTo(expectedBlock);
        }

        /// <summary>
        /// Tests that when the supplied justification is centre, leading
        /// and trailing whitespace is trimmed and the resulting text is
        /// centre justified and right padded with spaces to the required
        /// width.
        /// </summary>
        [Fact]
        public void Justify_CentreJustification_TrimsAndJustifiesCentre()
        {
            // Arrange
            var justifier = new TextJustifier();
            const string text = "  foo ";
            const int availableWidth = 20;
            var expectedText = "        foo         ";
            var expectedBlock = new TextBlock(20);
            expectedBlock.InsertText(expectedText);

            // Act
            justifier.Justify(text, TextJustification.Centre, availableWidth);

            // Assert
            justifier.JustifiedText.Should().Be(expectedText);
            justifier.JustifiedLines.Should().ContainSingle().Which.ToString().Should().Be(expectedText);
            justifier.JustifiedTextBlock.Should().BeEquivalentTo(expectedBlock);
        }

        /// <summary>
        /// Tests that when the supplied justification is right, leading and
        /// trailing whitespace is trimmed and the resulting text is right
        /// justified.
        /// </summary>
        [Fact]
        public void Justify_RightJustification_TrimsAndJustifiesRight()
        {
            // Arrange
            var justifier = new TextJustifier();
            const string text = "  foo ";
            const int availableWidth = 20;
            var expectedText = "                 foo";
            var expectedBlock = new TextBlock(20);
            expectedBlock.InsertText(expectedText);

            // Act
            justifier.Justify(text, TextJustification.Right, availableWidth);

            // Assert
            justifier.JustifiedText.Should().Be(expectedText);
            justifier.JustifiedLines.Should().ContainSingle().Which.ToString().Should().Be(expectedText);
            justifier.JustifiedTextBlock.Should().BeEquivalentTo(expectedBlock);
        }

        /// <summary>
        /// Tests that text which is wider than the available width
        /// is wrapped and justified correctly.
        /// </summary>
        [Fact]
        public void Justify_TextWiderThanAvailableWidth_Wraps()
        {
            // Arrange
            var justifier = new TextJustifier();
            const string text = "   The quick brown fox jumps over the lazy dog.   ";
            const int availableWidth = 21;
            var expectedText
                = " The quick brown fox "
                + Environment.NewLine
                + " jumps over the lazy "
                + Environment.NewLine
                + "        dog.         ";
            var expectedLines = new List<string>
            {
                " The quick brown fox ",
                " jumps over the lazy ",
                "        dog.         ",
            };
            var expectedBlock = new TextBlock(21);
            var i = 0;
            foreach (var line in expectedLines)
            {
                var blockToInsert = new TextBlock(21);
                blockToInsert.InsertText(line);
                expectedBlock.InsertBlock(blockToInsert, new ConsolePoint(0, i++));
            }

            // Act
            justifier.Justify(text, TextJustification.Centre, availableWidth);

            // Assert
            justifier.JustifiedText.Should().Be(expectedText);
            justifier.JustifiedLines.Should().BeEquivalentTo(expectedLines);
            justifier.JustifiedTextBlock.Should().BeEquivalentTo(expectedBlock);
        }

        /// <summary>
        /// Tests that when the supplied justification is not a member of the
        /// <see cref="TextJustification"/> enum, the correct
        /// exception is thrown.
        /// </summary>
        [Fact]
        public void Justify_InvalidJustification_Throws()
        {
            // Arrange
            var justifier = new TextJustifier();
            const string text = "  foo ";
            const int availableWidth = 20;

            // Act
            var action = () => justifier.Justify(text, (TextJustification)(-1), availableWidth);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Contain($"The supplied value is not a member of the {nameof(TextJustification)} enum.");
            ex.ParamName.Should().Be("justification");
        }

        /// <summary>
        /// Tests that if the Justify method is called more than once on the same
        /// instance of <see cref="TextJustifier"/> then any fields which were set
        /// during the first call are re-initialised during the second call.
        /// </summary>
        [Fact]
        public void Justify_InitialisesBetweenCalls()
        {
            // Arrange
            var justifier = new TextJustifier();
            justifier.Justify("some words", TextJustification.Centre, 6);
            var text = "I like armadillos, smooth on the inside, crunchy on the outside, armadillos!";
            var expectedText
                = "  I like armadillos," + Environment.NewLine
                + "       smooth on the" + Environment.NewLine
                + "  inside, crunchy on" + Environment.NewLine
                + "        the outside," + Environment.NewLine
                + "         armadillos!";

            // Act
            justifier.Justify(text, TextJustification.Right, 20);

            // Assert
            justifier.JustifiedText.Should().Be(expectedText);
        }

        /// <summary>
        /// Tests that lazy initialisation of the JustifiedLines property
        /// is not dependent on any of the other properties having been
        /// accessed.
        /// </summary>
        [Fact]
        public void JustifiedLines_InitialisedIndependentlyOfOtherProperties()
        {
            // Arrange
            var justifier = new TextJustifier();
            justifier.Justify("some words", TextJustification.Centre, 6);

            // Act
            var lines = justifier.JustifiedLines;

            // Assert
            lines.Should().HaveCount(2);
            lines[0].Should().Be(" some ");
            lines[1].Should().Be("words ");
        }

        /// <summary>
        /// Tests that lazy initialisation of the JustifiedText property
        /// is not dependent on any of the other properties having been
        /// accessed.
        /// </summary>
        [Fact]
        public void JustifiedText_InitialisedIndependentlyOfOtherProperties()
        {
            // Arrange
            var justifier = new TextJustifier();
            justifier.Justify("some words", TextJustification.Centre, 6);

            // Act
            var text = justifier.JustifiedText;

            // Assert
            text.Should().Be(" some " + Environment.NewLine + "words ");
        }

        /// <summary>
        /// Tests that lazy initialisation of the JustifiedTextBlock property
        /// is not dependent on any of the other properties having been accessed.
        /// </summary>
        [Fact]
        public void JustifiedTextBlock_InitialisesIndependentlyOfOtherProperties()
        {
            // Arrange
            var justifier = new TextJustifier();
            justifier.Justify("some words", TextJustification.Centre, 6);
            var expectedBlock = new TextBlock(6);
            var block1ToInsert = new TextBlock(6);
            block1ToInsert.InsertText(" some ");
            var block2ToInsert = new TextBlock(6);
            block2ToInsert.InsertText("words ");
            expectedBlock.InsertBlock(block1ToInsert, new ConsolePoint(0, 0));
            expectedBlock.InsertBlock(block2ToInsert, new ConsolePoint(0, 1));

            // Act
            var actualBlock = justifier.JustifiedTextBlock;

            // Assert
            actualBlock.Should().BeEquivalentTo(expectedBlock);
        }
    }
}
