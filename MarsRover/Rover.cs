namespace MarsRover
{
    class Rover : IRover
    {
        Direction direction;

        Direction Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        public Rover(Point coordinates, Direction direction) 
            : base(coordinates)
        {
            this.direction = direction;
        }
    }
}
