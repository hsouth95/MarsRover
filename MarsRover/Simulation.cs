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
        private IEnumerable<IRover> rovers;

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

        public IEnumerable<IRover> Rovers
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

        public Simulation(IPlanet planet, IEnumerable<IRover> rovers)
        {
            this.planet = planet;
            this.rovers = rovers;

            this.planet.BuildPlanet(this.rovers);
        }
    }
}
