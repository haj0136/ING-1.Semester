using CV_4;
using System;
using System.Linq;

namespace CV_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataLoader = new DataLoader();
            var graph = dataLoader.LoadData();

            var movies = new Graph();
            var actors = new Graph();
            // ACTORS
            foreach (var actor in graph.NodeList.FindAll(node => node.Type == GraphNode.NodeType.Actor).ToList())
            {
                var newNode = new GraphNode(actor, false);
                foreach (var neighbour in actor.Neighbours)
                {
                    foreach (var neighbour2 in neighbour.Neighbours)
                    {
                        if (!newNode.Neighbours.Contains(neighbour2) && !newNode.Equals(neighbour2))
                        {
                            newNode.Neighbours.Add(new GraphNode(neighbour2, false));
                        }

                    }
                }
                actors.NodeList.Add(newNode);
            }

            foreach (var node in actors.NodeList)
            {
                node.Degree = node.Neighbours.Count;
            }


            foreach (var node in actors.NodeList)
            {
                Console.WriteLine($"Actor {node.Id} degree: {node.Degree}");
            }

            // MOVIES
            
            foreach (var movie in graph.NodeList.FindAll(node => node.Type == GraphNode.NodeType.Movie).ToList())
            {
                var newNode = new GraphNode(movie, false);
                foreach (var neighbour in movie.Neighbours)
                {
                    foreach (var neighbour2 in neighbour.Neighbours)
                    {
                        if (!newNode.Neighbours.Contains(neighbour2) && !newNode.Equals(neighbour2))
                        {
                            newNode.Neighbours.Add(new GraphNode(neighbour2, false));
                        }

                    }
                }
                movies.NodeList.Add(newNode);
            }

            foreach (var node in movies.NodeList)
            {
                node.Degree = node.Neighbours.Count;
            }

            foreach (var node in movies.NodeList)
            {
                Console.WriteLine($"Movie {node.Id} degree: {node.Degree}");
            }

            Console.WriteLine();
            PrintMatrix(actors.ToMatrix());
            PrintMatrix(movies.ToMatrix());
            PrintMatrix(FloydWarshallAlgorithm.GetResult(actors.ToMatrix()));
            PrintMatrix(FloydWarshallAlgorithm.GetResult(movies.ToMatrix()));


            CalculateAverageDegree(actors);
            CalculateAverageDegree(movies);

            Console.WriteLine("Average degree: Actors = " + actors.AverageDegree);
            Console.WriteLine("Average degree: Movies = " + movies.AverageDegree);
            int[] diameterInfo = GetDiameter(FloydWarshallAlgorithm.GetResult(actors.ToMatrix()));
            Console.WriteLine($"Actors graph diameter = {diameterInfo[2]} | from {actors.NodeList[diameterInfo[0]].Name} to {actors.NodeList[diameterInfo[1]].Name}");
            diameterInfo = GetDiameter(FloydWarshallAlgorithm.GetResult(movies.ToMatrix()));
            Console.WriteLine($"Movies graph diameter = {diameterInfo[2]} | from {movies.NodeList[diameterInfo[0]].Name} to {movies.NodeList[diameterInfo[1]].Name}");
            Console.WriteLine();

            actors.CalculateAverageDistance(FloydWarshallAlgorithm.GetResult(actors.ToMatrix()));

            foreach (var actor in actors.NodeList)
            {
                Console.WriteLine($"Average distance of Node:{actor.Name} = {actor.AverageDistance}");
            }

            Console.WriteLine();

        }

        static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static void CalculateAverageDegree(Graph g)
        {
            foreach (var node in g.NodeList)
            {
                g.AverageDegree += node.Degree;
            }
            g.AverageDegree /= g.NodeList.Count;
        }

        static int[] GetDiameter(double[,] matrix)
        {
            // result[0] = i, result[1] = j, result[2] = diameter
            int[] result = new int[3];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if(matrix[i,j] > result[2])
                    {
                        result[0] = i;
                        result[1] = j;
                        result[2] = (int)matrix[i, j];
                    }
                }
            }

            return result;
        }
    }
}
