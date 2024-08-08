using TinubuSoftware.Helper;
using TinubuSoftware.Models;

namespace TinubuTest;
[TestClass]
public class MowerHelperTests
{
    [TestMethod]
    public void ProcessInstructions_Should_MoveMower_Correctly()
    {
        // Arrange
        var mower1 = new Mower { X = 1, Y = 2, Orientation = 'N' };
        var mower2 = new Mower { X = 3, Y = 3, Orientation = 'E' };
        string instructions1 = "LFLFLFLFF";
        string instructions2 = "FFRFFRFRRF";
        int maxX = 5;
        int maxY = 5;

        // Act
        MowerHelper.ProcessInstructions(mower1, instructions1, maxX, maxY);
        MowerHelper.ProcessInstructions(mower2, instructions2, maxX, maxY);

        // Assert
        Assert.AreEqual(1, mower1.X);
        Assert.AreEqual(3, mower1.Y);
        Assert.AreEqual('N', mower1.Orientation);

        Assert.AreEqual(5, mower2.X);
        Assert.AreEqual(1, mower2.Y);
        Assert.AreEqual('E', mower2.Orientation);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Invalid instruction")]
    public void ProcessInstructions_Should_ThrowArgumentException_OnInvalidInstruction()
    {
        // Arrange
        var mower = new Mower { X = 0, Y = 0, Orientation = 'N' };
        string invalidInstructions = "XYZ"; // Contains invalid instructions
        int maxX = 5;
        int maxY = 5;

        // Act
        MowerHelper.ProcessInstructions(mower, invalidInstructions, maxX, maxY);

        // Assert is handled by the ExpectedException attribute
    }

    [TestMethod]
    public void TurnLeft_Should_UpdateOrientation_Correctly()
    {
        // Arrange
        var mower = new Mower { X = 0, Y = 0, Orientation = 'N' };

        // Act & Assert
        MowerHelper.TurnLeft(mower);
        Assert.AreEqual('W', mower.Orientation);

        MowerHelper.TurnLeft(mower);
        Assert.AreEqual('S', mower.Orientation);

        MowerHelper.TurnLeft(mower);
        Assert.AreEqual('E', mower.Orientation);

        MowerHelper.TurnLeft(mower);
        Assert.AreEqual('N', mower.Orientation);
    }

    [TestMethod]
    public void TurnRight_Should_UpdateOrientation_Correctly()
    {
        // Arrange
        var mower = new Mower { X = 0, Y = 0, Orientation = 'N' };

        // Act & Assert
        MowerHelper.TurnRight(mower);
        Assert.AreEqual('E', mower.Orientation);

        MowerHelper.TurnRight(mower);
        Assert.AreEqual('S', mower.Orientation);

        MowerHelper.TurnRight(mower);
        Assert.AreEqual('W', mower.Orientation);

        MowerHelper.TurnRight(mower);
        Assert.AreEqual('N', mower.Orientation);
    }

    [TestMethod]
    public void MoveForward_Should_UpdatePosition_Correctly()
    {
        // Arrange
        var mower = new Mower { X = 1, Y = 1, Orientation = 'N' };
        int maxX = 5;
        int maxY = 5;

        // Act & Assert
        MowerHelper.MoveForward(mower, maxX, maxY);
        Assert.AreEqual(1, mower.X);
        Assert.AreEqual(2, mower.Y);

        mower.Orientation = 'E';
        MowerHelper.MoveForward(mower, maxX, maxY);
        Assert.AreEqual(2, mower.X);
        Assert.AreEqual(2, mower.Y);

        mower.Orientation = 'S';
        MowerHelper.MoveForward(mower, maxX, maxY);
        Assert.AreEqual(2, mower.X);
        Assert.AreEqual(1, mower.Y);

        mower.Orientation = 'W';
        MowerHelper.MoveForward(mower, maxX, maxY);
        Assert.AreEqual(1, mower.X);
        Assert.AreEqual(1, mower.Y);
    }
}