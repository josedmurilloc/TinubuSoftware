using TinubuNewOne.Models;

namespace TinubuNewOne.Interface;

public interface IMowerMovement
{
    void TurnLeft(Mower mower);
    void TurnRight(Mower mower);
    void MoveForward(Mower mower, int maxX, int maxY);
}
