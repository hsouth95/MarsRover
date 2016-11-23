using System.Collections.Generic;
using MarsRover.Rovers;

namespace MarsRover.Planets
{
    /// <summary>
    /// Abstract class used to represent the common functionalities of a planet
    /// </summary>
    public abstract class IPlanet
    {
        /// <summary>
        /// The Planet's grid dictating where Rovers are
        /// </summary>
        public abstract IDictionary<Point, bool> Grid
        {
            get;
        }

        /// <summary>
        /// The height of the planet
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The width of the planet
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Initialises an instance of the <see cref="IPlanet" /> class. 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        protected IPlanet(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Builds the Planet's grid and the location of the Rovers
        /// </summary>
        /// <param name="rovers">The Rovers to add to the Planet's grid</param>
        public abstract void BuildPlanet(IEnumerable<IRover> rovers);

        /// <summary>
        /// Checks if a given area already contains a Rover
        /// </summary>
        /// <param name="point">The area to check</param>
        /// <returns>A <see cref="bool"/> to state if a area is already occupied</returns>
        public abstract bool IsAreaOccupied(Point point);

        /// <summary>
        /// Updates the Planet's grid 
        /// </summary>
        /// <param name="rover">The <see cref="IRover"/> with an updated position</param>
        /// <param name="previousCoordinates">The previous coordinates of the Rover</param>
        public abstract void UpdateGridPosition(IRover rover, Point previousCoordinates);
    }
}
