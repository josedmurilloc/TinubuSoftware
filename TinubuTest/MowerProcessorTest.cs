using TinubuSoftware.Helper;
using TinubuSoftware.Models;

namespace TinubuTest;

[TestClass]
public class MowerProcessorTests
{
    [TestMethod]
    public void ProcessMowers_Should_ReturnCorrectFinalPositions()
    {
        // Arrange
        string[] inputLines =
        [
                "5 5",
                "1 2 N",
                "LFLFLFLFF",
                "3 3 E",
                "FFRFFRFRRF"
        ];
        int maxX = 5;
        int maxY = 5;

        // Act
        List<Mower> result = MowerProcessor.ProcessMowers(inputLines, maxX, maxY);

        // Assert
        Assert.AreEqual(2, result.Count);

        Assert.AreEqual(1, result[0].X);
        Assert.AreEqual(3, result[0].Y);
        Assert.AreEqual('N', result[0].Orientation);

        Assert.AreEqual(5, result[1].X);
        Assert.AreEqual(1, result[1].Y);
        Assert.AreEqual('E', result[1].Orientation);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Input lines are not in the expected format.")]
    public void ValidateAndParseInput_Should_ThrowException_OnInvalidInputLength()
    {
        // Arrange
        string[] inputLines =
        [
                "5 5",
                "1 2 N"
            // Missing instruction line
        ];

        // Act
        MowerProcessor.ValidateAndParseInput(inputLines, out _);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Invalid initial position at line 2")]
    public void ValidateAndParseInput_Should_ThrowException_OnInvalidInitialPositionFormat()
    {
        // Arrange
        string[] inputLines =
        [
                "5 5",
                "1 2",
                "LFLFLFLFF"
        ];

        // Act
        MowerProcessor.ValidateAndParseInput(inputLines, out _);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Invalid coordinates or orientation at line 2")]
    public void ValidateAndParseInput_Should_ThrowException_OnInvalidCoordinatesOrOrientation()
    {
        // Arrange
        string[] inputLines =
        [
                "5 5",
                "1 N",
                "LFLFLFLFF"
        ];

        // Act
        MowerProcessor.ValidateAndParseInput(inputLines, out _);
    }

    [TestMethod]
    public void ValidateAndParseInput_Should_ParseInputCorrectly()
    {
        // Arrange
        string[] inputLines =
        [
                "5 5",
                "1 2 N",
                "LFLFLFLFF",
                "3 3 E",
                "FFRFFRFRRF"
        ];

        // Act
        MowerProcessor.ValidateAndParseInput(inputLines, out List<(int x, int y, char orientation, string instructions)> parsedMowers);

        // Assert
        Assert.AreEqual(2, parsedMowers.Count);
        Assert.AreEqual((1, 2, 'N', "LFLFLFLFF"), parsedMowers[0]);
        Assert.AreEqual((3, 3, 'E', "FFRFFRFRRF"), parsedMowers[1]);
    }
}
