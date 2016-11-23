using System;
using System.Collections.Generic;
using MarsRover.Rovers;

namespace MarsRover.Planets
{
    /// <summary>
    /// Implementation of the <see cref="IPlanet"/> class
    /// </summary>
    public class Planet : IPlanet
    {
        /// <summary>
        /// Cached value of grid
        /// </summary>
        private IDictionary<Point, bool> grid;

        /// <summary>
        /// Instanstiates an instance of the <see cref="Planet"/> class.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public Planet(int height, int width)
            : base(height, width)
        {
        }
        
        /// <inheritdoc />
        public override IDictionary<Point, bool> Grid
        {
            get
            {
                // Check if the grid has been accessed before
                if (this.grid != null)
                {
                    return this.grid;
                }

                var coords = new Dictionary<Point, bool>();

                if (this.Height > 0 && this.Width > 0)
                {
                    // Build a fresh grid and initialise the default values
                    var point = new Point();
                    for (int x = 0; x < this.Width; x++)
                    {
                        for (int y = 0; y < this.Height; y++)
                        {
                            // The Point acts as a key to allow for quick access
                            coords.Add(new Point(x, y), false);
                        }
                    }

                    this.grid = coords;
                }
                else
                {
                    return null;
                }

                return this.grid;
            }
        }

        /// <inheritdoc />
        public override bool IsAreaOccupied(Point point)
        {
            return this.Grid[point];
        }

        /// <inheritdoc />
        public override void BuildPlanet(IEnumerable<IRover> rovers)
        {
            if (rovers == null)
            {
                throw new ArgumentNullException(nameof(rovers));
            }

            foreach (var rover in rovers)
            {
                // Check for invalid boundaries on the rover
                if (rover.Coordinates.X < 0 ||
                    rover.Coordinates.X >= this.Width ||
                    rover.Coordinates.Y < 0 ||
                    rover.Coordinates.Y >= this.Height)
                {
                    throw new ArgumentOutOfRangeException($"Rover with coordinates: {rover.Coordinates.ToString()} was out of the boundary of this Planet.");
                }

                // Check if there is already a Rover at this location
                if (this.Grid[rover.Coordinates])
                {
                    throw new InvalidOperationException($"Rover already exists at coordinates: {rover.Coordinates.ToString()}");
                }

                this.Grid[rover.Coordinates] = true;
            }
        }

        /// <inheritdoc />
        public override void UpdateGridPosition(IRover rover, Point previousCoordinates)
        {
            if(rover == null)
            {
                throw new ArgumentNullException(nameof(rover));
            }

            if(previousCoordinates == null)
            {
                throw new ArgumentNullException(nameof(previousCoordinates));
            }

            // No changes need to occur if the new coordinates are the same as the previous ones
            if(rover.Coordinates.Equals(previousCoordinates))
            {
                return;
            }

            // Check if there is a conflict in positions
            if (this.Grid[rover.Coordinates])
            {
                throw new InvalidOperationException($"A rover already exists at coordinates: {rover.Coordinates.ToString()}");
            }

            this.Grid[previousCoordinates] = false;
            this.Grid[rover.Coordinates] = true;
        }
    }
}
