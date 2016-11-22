using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class Planet : IPlanet
    {
        private IDictionary<Point, bool> grid;

        public Planet(int height, int width)
            : base(height, width)
        {
        }

        public override IDictionary<Point, bool> Grid
        {
            get
            {
                // Has the grid been accessed before?
                if (this.grid == null)
                {
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
                }

                return this.grid;
            }
        }

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
                    throw new ArgumentOutOfRangeException($"Rover with coordinates: X({rover.Coordinates.X}) Y({rover.Coordinates.Y}) was out of the boundary of this Planet.");
                }

                // Check if there is already a Rover at this location
                if (this.Grid[rover.Coordinates])
                {
                    throw new InvalidOperationException($"Rover already exists at coordinates: X({rover.Coordinates.X}) Y({rover.Coordinates.Y})");
                }

                this.Grid[rover.Coordinates] = true;
            }
        }
    }
}
