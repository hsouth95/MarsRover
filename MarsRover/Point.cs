namespace MarsRover
{
    public class Point
    {
        /// <summary>
        /// The X coordinates of this Point
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinates of this Point
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Initilises an instance of the <see cref="Point"/> class.
        /// </summary>
        public Point()
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Initilises an instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinates of the Point</param>
        /// <param name="y">The y coordinates of the point</param>
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        /// <inheritdocs />
        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var p = (Point)obj;

            return (X == p.X) && (Y == p.Y);
        }

        /// <inheritdocs />
        public override int GetHashCode()
        {
            return this.X ^ this.Y;
        }

        /// <inheritdocs />
        public override string ToString()
        {
            return $"Points: X({this.X}) Y({this.Y})";
        }
    }
}
