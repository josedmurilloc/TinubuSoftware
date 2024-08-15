using TinubuNewOne.Interface;
using TinubuNewOne.Models;

namespace TinubuNewOne.Services
{
    public class MowerProcessingService : IMowerProcessingService
    {
        private readonly IMowerMovement _mowerMovement;

        public MowerProcessingService(IMowerMovement mowerMovement)
        {
            _mowerMovement = mowerMovement;
        }

        public Mower ProcessMower(MowerDTO mowerDTO, int maxX, int maxY)
        {
            // Instantiate the Mower object using the values from MowerDTO
            var mower = new Mower
            {
                X = mowerDTO.X,
                Y = mowerDTO.Y,
                Orientation = mowerDTO.Orientation
            };

            // Process each instruction in the sequence
            foreach (char instruction in mowerDTO.Instructions)
            {
                switch (instruction)
                {
                    case 'L':
                        _mowerMovement.TurnLeft(mower);
                        break;
                    case 'R':
                        _mowerMovement.TurnRight(mower);
                        break;
                    case 'F':
                        _mowerMovement.MoveForward(mower, maxX, maxY);
                        break;
                    default:
                        throw new ArgumentException($"Invalid instruction: {instruction}");
                }
            }

            return mower;
        }
    }
}