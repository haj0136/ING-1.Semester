using CV_4_WF.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_4_WF.SOMA
{
    public class Node
    {
        private static Random rnd = new Random();
        // dimension
        public List<float> X { get; set; }

        private List<float> nextPosition;

        private int[] PRTVector;

        private List<float> bestPosition;
        

        public Node()
        {
            X = new List<float>();
            nextPosition = new List<float>();
        }
        public Node(params float[] x) : this()
        {
            foreach (var value in x)
            {
                X.Add(value);
            }
        }
        public Node(int NOdimensions) : this()
        {
            for (int i = 0; i < NOdimensions; i++)
            {
                X.Add(float.MaxValue);
            }
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

        public void CalculatePRTVector(float PRT, float negativePRT)
        {
            bestPosition = new List<float>(X);
            PRTVector = new int[X.Count];
            for (int i = 0; i < X.Count; i++)
            {
                double randomDouble = rnd.NextDouble(); 
                if(randomDouble < PRT)
                {
                    PRTVector[i] = 1;
                    if(randomDouble < negativePRT)
                    {
                        PRTVector[i] *= -1;
                    }
                }
            }
        }

        public void CalculateNewPosition(float[] bestNode, float stepSize, AbstractFunction testFunction)
        {
            float[] previousPosition = new float[X.Count];
            nextPosition.CopyTo(previousPosition);
            nextPosition.Clear();

            for (int i = 0; i < X.Count - 1; i++)
            {
                nextPosition.Add(X[i] + (bestNode[i] - X[i]) * stepSize * PRTVector[i]);
                if(nextPosition[i] < testFunction.MinX || nextPosition[i] > testFunction.MaxX)
                {
                    nextPosition[i] = previousPosition[i];
                }
            }
            nextPosition.Add((float)testFunction.getResult(Array.ConvertAll(nextPosition.ToArray(), x => (double)x)));
            if(nextPosition[nextPosition.Count - 1] < bestPosition[nextPosition.Count - 1])
            {
                bestPosition = new List<float>(nextPosition);
            }
        }
        public float[] GetNextPosition()
        {
            return nextPosition.ToArray();
        }

        public void SetBestPosition()
        {
            X = new List<float>(bestPosition);
        }
    }
}
