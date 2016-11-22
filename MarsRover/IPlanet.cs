using System.Collections.Generic;

namespace MarsRover
{
    abstract class IPlanet
    {
        private int height;
        private int width;
        
        public abstract Dictionary<Point, bool> Grid
        {
            get;
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
            }
        }

        public IPlanet(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public abstract void BuildPlanet(IRover[] rovers);
    }
}
