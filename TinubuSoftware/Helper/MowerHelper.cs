using TinubuSoftware.Models;

namespace TinubuSoftware.Helper;

public static class MowerHelper
{
    public static void ProcessInstructions(Mower mower, string instructions, int maxX, int maxY)
    {
        foreach (char instruction in instructions)
        {
            switch (instruction)
            {
                case 'L':
                    TurnLeft(mower);
                    break;
                case 'R':
                    TurnRight(mower);
                    break;
                case 'F':
                    MoveForward(mower, maxX, maxY);
                    break;
                default:
                    throw new ArgumentException("Invalid instruction");
            }
        }
    }

    internal static void TurnLeft(Mower mower)
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

    internal static void TurnRight(Mower mower)
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

    internal static void MoveForward(Mower mower, int maxX, int maxY)
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
