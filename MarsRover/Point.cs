namespace MarsRover
{
    public class Point
    {
        private int x;
        private int y;

        public int X { get { return this.x; } set { x = value; } }
        
        public int Y { get { return this.y; } set { y = value; } }

        public Point()
        {
            this.x = 0;
            this.y = 0;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Point p = (Point)obj;

            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return this.X ^ this.Y;
        }
    }
}
