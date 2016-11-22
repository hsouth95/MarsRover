namespace MarsRover
{
    public abstract class IRover
    {
        private Point coordinates;

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

        public abstract void Move();
    }
}
