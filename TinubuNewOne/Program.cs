using TinubuNewOne.Interface;
using TinubuNewOne.Models;
using TinubuNewOne.Services;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Define file path to the input file (mower.txt)
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File", "mower.txt");
            string[] inputLines = File.ReadAllLines(filePath); // Read all lines from the file

            // Validate that the input has an odd number of lines and is non-empty
            if (inputLines.Length < 2 || inputLines.Length % 2 == 0)
            {
                throw new InvalidOperationException("The input file format is invalid.");
            }

            // Parse lawn dimensions from the first line
            (int maxX, int maxY) = ParseLawnDimensions(inputLines[0]);

            // Initialize mower processing service
            var mowerProcessingService = CreateMowerProcessingService();

            // Process the input lines to get the list of mowers and their final positions
            List<Mower> mowers = ProcessMowers(inputLines, mowerProcessingService, maxX, maxY);

            // Display final positions of the mowers
            DisplayResults(mowers);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur and display an error message
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Wait for user input before closing the program
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    // Method to parse lawn dimensions from the first line of the input
    private static (int maxX, int maxY) ParseLawnDimensions(string lawnDimensionLine)
    {
        var dimensions = lawnDimensionLine.Split(' '); // Split the line by space
        // Validate that the input contains exactly two integers (X and Y dimensions)
        if (dimensions.Length != 2 || !int.TryParse(dimensions[0], out int maxX) || !int.TryParse(dimensions[1], out int maxY))
        {
            throw new InvalidOperationException("Invalid lawn dimensions.");
        }
        return (maxX, maxY); // Return the parsed X and Y dimensions
    }

    // Method to process mowers' positions and instructions
    private static List<Mower> ProcessMowers(string[] inputLines, IMowerProcessingService mowerProcessingService, int maxX, int maxY)
    {
        var mowers = new List<Mower>();

        // Loop through every two lines (starting from line 1), as each mower's input has two lines
        for (int i = 1; i < inputLines.Length; i += 2)
        {
            // Parse the position and instructions for each mower
            var mowerDto = ParseMowerDTO(inputLines[i], inputLines[i + 1]);

            // Process the mower using the service to determine its final position
            var mower = mowerProcessingService.ProcessMower(mowerDto, maxX, maxY);
            
            mowers.Add(mower); // Add the processed mower to the list
        }

        return mowers; // Return the list of mowers with their final positions
    }

    // Method to parse mower position and instructions from the input lines
    private static MowerDTO ParseMowerDTO(string positionLine, string instructionsLine)
    {
        var positionParts = positionLine.Split(' '); // Split the position line by space

        // Validate that the position input has exactly three parts: X, Y, and Orientation
        if (positionParts.Length != 3 || !int.TryParse(positionParts[0], out int x) || !int.TryParse(positionParts[1], out int y) || positionParts[2].Length != 1)
        {
            throw new InvalidOperationException("Invalid mower position format.");
        }

        // Return a DTO containing parsed position and instructions
        return new MowerDTO
        {
            X = x,
            Y = y,
            Orientation = positionParts[2][0], // Extract the first character of the orientation
            Instructions = instructionsLine // Instructions are the entire second line
        };
    }

    // Method to display the final results (mowers' final positions)
    private static void DisplayResults(List<Mower> mowers)
    {
        Console.WriteLine("The expected result (final position of the mowers) is:");
        // Loop through each mower and display its final X, Y, and orientation
        foreach (var mower in mowers)
        {
            Console.WriteLine($"{mower.X} {mower.Y} {mower.Orientation}");
        }
    }

    // Method to create and return a MowerProcessingService object
    private static IMowerProcessingService CreateMowerProcessingService()
    {
        // Initialize and return the MowerMovementService as a dependency
        var mowerMovement = new MowerMovementService();
        // Pass the movement service to the MowerProcessingService constructor
        return new MowerProcessingService(mowerMovement);
    }
}
