using TinubuNewOne.Interface;
using TinubuNewOne.Models;

namespace TinubuNewOne.Services
{
    public class MowerMovementService : IMowerMovement
    {
        public void TurnLeft(Mower mower)
        {
            mower.Orientation = mower.Orientation switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => mower.Orientation
            };
        }

        public void TurnRight(Mower mower)
        {
            mower.Orientation = mower.Orientation switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => mower.Orientation
            };
        }

        public void MoveForward(Mower mower, int maxX, int maxY)
        {
            switch (mower.Orientation)
            {
                case 'N':
                    if (mower.Y < maxY) mower.Y++;
                    break;
                case 'E':
                    if (mower.X < maxX) mower.X++;
                    break;
                case 'S':
                    if (mower.Y > 0) mower.Y--;
                    break;
                case 'W':
                    if (mower.X > 0) mower.X--;
                    break;
                default:
                    break;
            }
        }
    }
}