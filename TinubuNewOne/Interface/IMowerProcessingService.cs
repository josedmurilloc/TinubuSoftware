using TinubuNewOne.Models;

namespace TinubuNewOne.Interface;

public interface IMowerProcessingService
{
    Mower ProcessMower(MowerDTO mowerDTO, int maxX, int maxY);
}
