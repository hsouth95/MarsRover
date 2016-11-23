using MarsRover.Enums;

namespace MarsRover.Rovers
{
    /// <summary>
    /// Abstract class to represent the common functionalities of a Rover
    /// </summary>
    public abstract class IRover
    {
        /// <summary>
        /// The coordinates of the Rover
        /// </summary>
        public Point Coordinates { get; set; }

        /// <summary>
        /// The direction the Rover is facing
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// Initilises an instance of the <see cref="IRover"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates of the Rover</param>
        /// <param name="direction">The direction the Rover is facing</param>
        protected IRover(Point coordinates, Direction direction)
        {
            this.Coordinates = coordinates;
            this.Direction = direction;
        }

        /// <summary>
        /// Moves the Rover
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// Gets the Rover's next move
        /// </summary>
        /// <returns>The <see cref="Action"/> of the next move</returns>
        public abstract Action GetNextMove();
    }
}
