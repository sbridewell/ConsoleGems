// <copyright file="TextBlockTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Text
{
    /// <summary>
    /// Unit tests for the <see cref="TextBlock"/> class.
    /// </summary>
    public class TextBlockTest
    {
        /// <summary>
        /// Tests that the constructor sets the <see cref="TextBlock.Width"/> property
        /// to the correct width.
        /// </summary>
        /// <param name="width">The width to pass to the constructor.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(42)]
        public void Constructor_SetsWidthProperty(int width)
        {
            // Arrange

            // Act
            var block = new TextBlock(width);

            // Assert
            block.Width.Should().Be(width);
        }

        /// <summary>
        /// Tests that when no text has been added, the ToString method returns an empty string.
        /// </summary>
        [Fact]
        public void ToString_NoTextAdded_ReturnsEmptyString()
        {
            // Arrange
            var block = new TextBlock(10);

            // Act
            string result = block.ToString();

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Tests that when no text has been added, the InsertText method wraps the supplied
        /// text to the width of the block and sets the Height property to the number of lines
        /// in the wrapped text.
        /// </summary>
        [Fact]
        public void InsertText_SuppliedTextWrapsToFitWidth()
        {
            // Arrange
            var block = new TextBlock(10);
            var textToInsert = "This is a long line of text that will wrap.";
            var expectedText
                = "This is a " + Environment.NewLine
                + "long line " + Environment.NewLine
                + "of text th" + Environment.NewLine
                + "at will wr" + Environment.NewLine
                + "ap.       ";
            var expectedHeight = expectedText.Split(Environment.NewLine).Length;

            // Act
            block.InsertText(textToInsert);

            // Assert
            block.ToString().Should().Be(expectedText);
            block.Height.Should().Be(expectedHeight);
        }

        /// <summary>
        /// Tests that when no text has been added, the InsertBlock method resizes the
        /// height of the <see cref="TextBlock"/> under text to accommodate the inserted
        /// <see cref="TextBlock"/>, and fills the space not occupied by the inserted
        /// <see cref="TextBlock"/> with spaces.
        /// </summary>
        [Fact]
        public void InsertBlock_NoPreviousText_InsertsBlockCorrectly()
        {
            // Arrange
            var blockUnderTest = new TextBlock(10);
            var blockToInsert = new TextBlock(2);
            blockToInsert.InsertText("1234");
            var expectedText
                = "          " + Environment.NewLine
                + "   12     " + Environment.NewLine
                + "   34     ";
            var expectedHeight = expectedText.Split(Environment.NewLine).Length;

            // Act
            blockUnderTest.InsertBlock(blockToInsert, new ConsolePoint(3, 1));

            // Assert
            blockUnderTest.ToString().Should().Be(expectedText);
            blockUnderTest.Height.Should().Be(expectedHeight);
        }

        /// <summary>
        /// Tests that when the InsertBlock method is used twice to insert overlapping
        /// blocks of text, they overlap correctly and any characters which shouldn't
        /// be overwritten are not overwritten.
        /// </summary>
        [Fact]
        public void InsertBlock_Insert2Blocks_PreviousTextIsPreserved()
        {
            // Arrange
            var blockUnderTest = new TextBlock(10);
            var block1ToInsert = new TextBlock(2);
            block1ToInsert.InsertText("1234");
            var block2ToInsert = new TextBlock(2);
            block2ToInsert.InsertText("abcd");
            var expectedText
                = "    ab    " + Environment.NewLine
                + "   1cd    " + Environment.NewLine
                + "   34     ";
            var expectedHeight = expectedText.Split(Environment.NewLine).Length;
            blockUnderTest.InsertBlock(block1ToInsert, new ConsolePoint(3, 1));

            // Act
            blockUnderTest.InsertBlock(block2ToInsert, new ConsolePoint(4, 0));

            // Assert
            blockUnderTest.ToString().Should().Be(expectedText);
            blockUnderTest.Height.Should().Be(expectedHeight);
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the block to insert is too wide
        /// or positioned too far to the right to fit into the target text block.
        /// </summary>
        [Fact]
        public void InsertBlock_InsertedBlockDoesNotFitInTargetBlock_Throws()
        {
            // Arrange
            var blockUnderTest = new TextBlock(10);
            var blockToInsert = new TextBlock(10);
            blockToInsert.InsertText("1234567890");

            // Act
            var action = () => blockUnderTest.InsertBlock(blockToInsert, new ConsolePoint(1, 0));

            // Assert
            var ex = action.Should().Throw<ArgumentException>().Which;
            ex.Message.Should().Contain("Block with width 10 is too wide to insert at horizontal position 1 of a block with width 10");
            ex.ParamName.Should().Be("block");
        }

        /// <summary>
        /// Tests that the correct exception is thrown when the co-ordinates to insert the
        /// block at are negative.
        /// </summary>
        /// <param name="left">The left co-ordinate to insert at.</param>
        /// <param name="top">The top co-ordinate to insert at.</param>
        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void InsertBlock_InsertAtHasNegativeCoordinates_Throws(int left, int top)
        {
            // Arrange
            var blockUnderTest = new TextBlock(10);
            var blockToInsert = new TextBlock(2);

            // Act
            var action = () => blockUnderTest.InsertBlock(blockToInsert, new ConsolePoint(left, top));

            // Assert
            var ex = action.Should().Throw<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Contain($"The co-ordinates must be non-negative. The co-ordinates you supplied are {left},{top}");
            ex.ParamName.Should().Be("insertAt");
        }
    }
}
