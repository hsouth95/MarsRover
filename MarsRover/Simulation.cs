using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Simulation
    {
        private IPlanet planet;
        private IRover[] rovers;

        public IPlanet Planet
        {
            get
            {
                return this.planet;
            }
            set
            {
                this.planet = value;
            }
        }

        public IRover[] Rovers
        {
            get
            {
                return this.rovers;
            }
            set
            {
                this.rovers = value;
            }
        }

        public Simulation(IPlanet planet, IRover[] rovers)
        {
            this.planet = planet;
            this.rovers = rovers;

            this.planet.BuildPlanet(this.rovers);
        }
    }
}
