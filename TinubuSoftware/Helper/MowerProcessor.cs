using TinubuSoftware.Models;

namespace TinubuSoftware.Helper;

public static class MowerProcessor
{
    /// <summary>
    /// Processes the mowers based on the input lines and lawn dimensions.
    /// </summary>
    /// <param name="inputLines">The lines from the input file.</param>
    /// <param name="maxX">The maximum X coordinate of the lawn.</param>
    /// <param name="maxY">The maximum Y coordinate of the lawn.</param>
    /// <returns>A list of mowers with their final positions and orientations.</returns>
    public static List<Mower> ProcessMowers(string[] inputLines, int maxX, int maxY)
    {
        // Validate the input lines for proper format and structure
        ValidateAndParseInput(inputLines, out List<(int x, int y, char orientation, string instructions)> parsedMowers);

        // Initialize a list to hold the mowers
        List<Mower> mowers = [];

        // Process each parsed mower's initial position and instructions
        foreach (var (x, y, orientation, instructions) in parsedMowers)
        {
            // Create a new mower with the parsed position and orientation
            var mower = new Mower { X = x, Y = y, Orientation = orientation };

            // Process the instructions for the mower
            MowerHelper.ProcessInstructions(mower, instructions, maxX, maxY);

            // Add the mower to the list
            mowers.Add(mower);
        }

        // Return the list of processed mowers
        return mowers;
    }

    /// <summary>
    /// Validates the input array and parses the initial positions and orientations.
    /// </summary>
    /// <param name="inputLines">The lines from the input file.</param>
    /// <param name="parsedMowers">The parsed mowers list to populate.</param>
    internal static void ValidateAndParseInput(string[] inputLines, out List<(int x, int y, char orientation, string instructions)> parsedMowers)
    {
        // Ensure the input is not null, has at least one mower, and an odd number of lines (1 grid + pairs of position/instructions)
        if (inputLines == null || inputLines.Length < 2 || inputLines.Length % 2 != 1)
            throw new ArgumentException("Input lines are not in the expected format.");

        parsedMowers = [];

        for (int i = 1; i < inputLines.Length; i += 2)
        {
            var initialPosition = inputLines[i].Split(' ');

            // Ensure the initial position contains exactly 3 elements
            if (initialPosition.Length != 3)
                throw new ArgumentException($"Invalid initial position at line {i + 1}");

            // Validate and parse the x and y coordinates, and ensure the orientation is a single character
            if (!int.TryParse(initialPosition[0], out int x) || !int.TryParse(initialPosition[1], out int y) || initialPosition[2].Length != 1)
                throw new ArgumentException($"Invalid coordinates or orientation at line {i + 1}");

            char orientation = initialPosition[2][0];
            string instructions = inputLines[i + 1];

            parsedMowers.Add((x, y, orientation, instructions));
        }
    }
}

