using System;
using System.Collections.Generic;
using System.IO;
using MarsRover.Enums;
using MarsRover.Planets;
using MarsRover.Rovers;
using Action = MarsRover.Enums.Action;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the file path of the text file that contains the simulation:");
            string filePath = Console.ReadLine();
            string[] lines = File.ReadAllLines(filePath);

            // Split the first line in the file to get the height and width of the grid
            string[] dimensions = lines[0].Split(' ');

            // Increment the value to build the dimensions of the grid
            int width = int.Parse(dimensions[0]) + 1;
            int height = int.Parse(dimensions[1]) + 1;

            var rovers = new List<IRover>();

            for (int i = 1; i < lines.Length; i += 2)
            {
                string[] startingCoordinates = lines[i].Split(' ');

                // Split out the starting coordinates of this rover
                int x = int.Parse(startingCoordinates[0]);
                int y = int.Parse(startingCoordinates[1]);

                Direction direction;

                switch (startingCoordinates[2])
                {
                    case "N":
                        direction = Direction.North;
                        break;
                    case "E":
                        direction = Direction.East;
                        break;
                    case "S":
                        direction = Direction.South;
                        break;
                    case "W":
                        direction = Direction.West;
                        break;
                    default:
                        throw new ArgumentException("Initial direction is incorrect");
                }

                Queue<Action> actions = GetActions(lines[i + 1]);

                rovers.Add(new Rover(new Point(x, y), direction, actions));
            }

            // Build the simulation
            var simulation = new Simulation(new Planet(height, width), rovers);

            IEnumerable<string> positions = null;
            try
            {
                positions = simulation.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error with the simulation: {e.Message}");
                Console.WriteLine("Please update the file and try again.");
            }

            // Output the results
            if (positions != null)
            {
                foreach (var position in positions)
                {
                    Console.WriteLine(position);
                }
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Builds a series of movements for a Rover
        /// </summary>
        /// <param name="actionLine">The string of input</param>
        /// <returns>A <see cref="Queue{Action}"/> of the Rover's actions</returns>
        public static Queue<Action> GetActions(string actionLine)
        {
            var roverActions = new Queue<Action>();

            if (!string.IsNullOrEmpty(actionLine))
            {
                char[] actions = actionLine.ToUpper().ToCharArray();

                foreach (var action in actions)
                {
                    switch (action)
                    {
                        case 'M':
                            roverActions.Enqueue(Action.MoveForward);
                            break;
                        case 'L':
                            roverActions.Enqueue(Action.RotateLeft);
                            break;
                        case 'R':
                            roverActions.Enqueue(Action.RotateRight);
                            break;
                        default:
                            throw new ArgumentException("Invalid actions entered");
                    }
                }
            }

            return roverActions;
        }
    }
}
