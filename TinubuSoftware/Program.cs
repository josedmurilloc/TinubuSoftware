using TinubuSoftware.Helper;
using TinubuSoftware.Models;

class Program
{
    static void Main(string[] args)
    {
        // Define the path to the input file
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File", "mower.txt");

        // Read all lines from the input file
        string[] inputLines = File.ReadAllLines(filePath);

        // Parse lawn dimensions from the first line of the input file
        string[] lawnDimensions = inputLines[0].Split(' ');
        int maxX = int.Parse(lawnDimensions[0]);
        int maxY = int.Parse(lawnDimensions[1]);

        // Process the mowers and get their final states
        List<Mower> mowers = MowerProcessor.ProcessMowers(inputLines, maxX, maxY);

        // Output the final position and orientation of each mower
        foreach (Mower mower in mowers)
        {
            Console.WriteLine("The expected result (final position of the mowers) is:");
            Console.WriteLine($"{mower.X} {mower.Y} {mower.Orientation}");
        }

        // Prevent the console from closing immediately
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }

}
