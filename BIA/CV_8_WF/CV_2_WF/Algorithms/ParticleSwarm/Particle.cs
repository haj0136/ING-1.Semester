using CV_4_WF.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.ParticleSwarm
{
    public class Particle
    {
        private static Random rnd = new Random();
        public List<float> X { get; set; }
        private List<float> speed;
        public List<float> PBest { get; set; }

        public Particle()
        {
            X = new List<float>();
            speed = new List<float>();
        }

        public float[] ToFloatArray()
        {
            return X.ToArray();
        }

        public float[,] To2dArray()
        {
            var vectorList = new List<float[]>
            {
                this.ToFloatArray()
            };
            return HelpTools.CreateRectangularArray(vectorList);
        }

        public void CalculateSpeed(List<float> gBest, float c1, float c2, float vMax)
        {   
            //if list "speed" is empty, fill it with 0's
            if (!speed.Any())
            {
                for (int i = 0; i < X.Count - 1; i++)
                {
                    speed.Add(0);
                }
            }

            for (int i = 0; i < speed.Count; i++)
            {
                speed[i] = speed[i] + c1 * (float)rnd.NextDouble() * (PBest[i] - X[i]) + c2 * (float)rnd.NextDouble() * (gBest[i] - X[i]);
                if(speed[i] > vMax)
                {
                    speed[i] = (float)rnd.NextDouble() * vMax;
                }
            }
        }

        public void AdjustPosition(AbstractFunction testFunction)
        {
            List<float> tempList = new List<float>();
            for (int i = 0; i < X.Count - 1; i++)
            {
                tempList.Add(X[i] + speed[i]);
                if(tempList[i] < testFunction.MinX || tempList[i] > testFunction.MaxX)
                {
                    tempList[i] = (float)rnd.NextDouble() * (testFunction.MaxX - testFunction.MinX) + testFunction.MinX;
                }
            }
            X = new List<float>(tempList);
            X.Add((float)testFunction.getResult(Array.ConvertAll(X.ToArray(), x => (double)x)));

            if(X.Last() < PBest.Last())
            {
                PBest = new List<float>(X);
            }
        }
    }
}
