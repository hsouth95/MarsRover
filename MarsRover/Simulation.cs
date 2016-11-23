using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Planets;
using MarsRover.Rovers;
using Action = MarsRover.Enums.Action;

namespace MarsRover
{
    public class Simulation
    {
        /// <summary>
        /// The Planet this simulation is running
        /// </summary>
        public IPlanet Planet { get; set; }

        /// <summary>
        /// The Rovers this simulation is running
        /// </summary>
        public IEnumerable<IRover> Rovers { get; set; }

        /// <summary>
        /// Initilises an instance of the <see cref="Simulation"/> class.
        /// </summary>
        /// <param name="planet">The planet in this simulation</param>
        /// <param name="rovers">The Rovers in this simulation</param>
        public Simulation(IPlanet planet, IEnumerable<IRover> rovers)
        {
            this.Planet = planet;
            this.Rovers = rovers;

            this.Planet.BuildPlanet(this.Rovers);
        }

        /// <summary>
        /// Runs the simulation
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of the returned coordinates</returns>
        public IEnumerable<string> Run()
        {
            foreach (var rover in this.Rovers)
            {
                this.RunRover(rover);
            }

            return this.GetFinishingPositions();
        }

        /// <summary>
        /// Runs the simulation for a given Rover
        /// </summary>
        /// <param name="rover">The Rover to run</param>
        public void RunRover(IRover rover)
        {
            while (rover.GetNextMove() != Action.Nothing)
            {
                // Copy the value of the current coordinates
                var currentCoordinates = new Point(rover.Coordinates.X, rover.Coordinates.Y);

                rover.Move();

                if (!rover.Coordinates.Equals(currentCoordinates))
                {
                    if (this.Planet.IsAreaOccupied(rover.Coordinates))
                    {
                        throw new InvalidOperationException("Rover position conflicts with another");
                    }

                    this.Planet.UpdateGridPosition(rover, currentCoordinates);
                }
            }
        }

        /// <summary>
        /// Retrieves the final positions of the Rover's
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of the final positions</returns>
        public IEnumerable<String> GetFinishingPositions()
        {
            return this.Rovers.Select(r =>
            {
                // Create friendly output for the finishing position of a Rover
                return $"{r.Coordinates.X} {r.Coordinates.Y} {r.Direction.ToString().First()}";
            });
        }

    }
}
