namespace MarsRover
{
    abstract class IRover
    {
        Point coordinates;

        public Point Coordinates
        {
            get
            {
                return this.coordinates;
            }
            set
            {
                this.coordinates = value;
            }
        }

        public IRover(Point coordinates)
        {
            this.coordinates = coordinates;
        }
    }
}
