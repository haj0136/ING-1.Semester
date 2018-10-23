using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.ParticleSwarm
{
    public class Population
    {
        public List<Particle> Particles { get; set; }
        public List<float> GBest { get; set; };

        public Population()
        {
            Particles = new List<Particle>();
            GBest = new List<float>();
        }

        public float[,] To2dArray()
        {
            var vectorList = new List<float[]>();
            foreach (var particle in Particles)
            {
                vectorList.Add(particle.ToFloatArray());
            }
            return HelpTools.CreateRectangularArray(vectorList);
        }
    }
}
