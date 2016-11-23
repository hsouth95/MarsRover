using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Planets;
using MarsRover.Rovers;
using Action = MarsRover.Enums.Action;

namespace MarsRover
{
    public class Simulation
    {
        public IPlanet Planet { get; set; }

        public IEnumerable<IRover> Rovers { get; set; }

        public Simulation(IPlanet planet, IEnumerable<IRover> rovers)
        {
            this.Planet = planet;
            this.Rovers = rovers;

            this.Planet.BuildPlanet(this.Rovers);
        }

        public IEnumerable<string> Run()
        {
            foreach (var rover in this.Rovers)
            {
                this.RunRover(rover);
            }

            return this.GetFinishingPositions();
        }

        public void RunRover(IRover rover)
        {
            while (rover.GetNextMove() != Action.Nothing)
            {
                Point currentCoordinates = rover.Coordinates;

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
        
        public IEnumerable<string> GetFinishingPositions()
        {
            return this.Rovers.Select(r => {
                // Create friendly output for the finishing position of a Rover
                return $"{r.Coordinates.X} {r.Coordinates.Y} {r.Direction.ToString().First()}";
            });
        }

    }
}
