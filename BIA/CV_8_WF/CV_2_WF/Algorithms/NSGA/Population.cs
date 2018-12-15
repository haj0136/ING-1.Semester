using CV_8_WF.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CV_8_WF.NSGA
{
    public class Population
    {
        public List<float> individuals;

        public List<float> costF1;

        public List<float> costF2;

        private F1 f1;
        private F2 f2;
        private readonly Random rnd;
        private readonly float MBP;

        public Population()
        {
            individuals = new List<float>();
            costF1 = new List<float>();
            costF2 = new List<float>();
            f1 = new F1();
            f2 = new F2();
            rnd = new Random();
            MBP = 0.1f;
        }

        public void GenerateFirstPop(int size)
        {

            for (int i = 0; i < size; i++)
            {
                float randomX = rnd.Next(-55, 54) + (float)rnd.NextDouble();
                individuals.Add(randomX);
                costF1.Add(f1.GetResult(randomX));
                costF2.Add(f2.GetResult(randomX));
            }
        }

        public void GenerateOffsprings(int populationSize)
        {
            for (int i = 0; i < populationSize; i++)
            {
                float x1Decimal;
                do
                {

                    int x1Index = i;
                    int x2Index;
                    do
                    {
                        x2Index = rnd.Next(populationSize - 1);
                    } while (x1Index == x2Index);

                    string x1Binary = ToBinary(individuals[x1Index]);
                    string x2Binary = ToBinary(individuals[x2Index]);

                    int cp;
                    do
                    {
                        cp = rnd.Next(x1Binary.Length - 1);

                    } while (cp == 0 || cp > 30);
                    var offspring = new StringBuilder();
                    offspring.Append(x1Binary.Substring(0, cp) + x2Binary.Substring(cp));

                    //mutation
                    if (rnd.NextDouble() < MBP)
                    {
                        int mutationIndex = rnd.Next(offspring.Length - 1);
                        if (offspring[mutationIndex] == 0)
                            offspring[mutationIndex] = '1';
                        else
                            offspring[mutationIndex] = '0';

                    }

                    x1Decimal = BinaryStringToSingle(offspring.ToString());

                    while (x1Decimal < -55 || x1Decimal > 55)
                    {
                        x1Decimal = rnd.Next(-55, 54) + (float)rnd.NextDouble();
                    }
                } while (f1.GetResult(x1Decimal) == 0 && x1Decimal != 0 || float.IsNaN(x1Decimal));

                individuals.Add(x1Decimal);
                costF1.Add(f1.GetResult(x1Decimal));
                costF2.Add(f2.GetResult(x1Decimal));
            }
        }

        private string ToBinary(float myValue)
        {
            byte[] b = BitConverter.GetBytes(myValue);
            int i = BitConverter.ToInt32(b, 0);
            return Convert.ToString(i, 2);
        }

        private float BinaryStringToSingle(string s)
        {
            int i = Convert.ToInt32(s, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);
        }

    }
}
