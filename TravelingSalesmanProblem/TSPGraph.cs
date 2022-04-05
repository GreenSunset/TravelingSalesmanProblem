using System;
using System.Collections.Generic;
using System.IO;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Class to represent a TSP Graph
    /// </summary>
    internal class TSPGraph
    {
        public List<string> nodes { get; }
        private int[,] costs;
        private Dictionary<string, int> indexes;

        /// <summary>
        /// Constructor from file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <exception cref="Exception">Readfile and format exceptions</exception>
        public TSPGraph(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            int count = int.Parse(lines[0]);
            int current = 0;

            costs = new int[count, count];
            nodes = new List<string>();
            indexes = new Dictionary<string, int>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(' ');
                if (words.Length != 3) throw new Exception("Wrong number of words");
                if (!nodes.Contains(words[0]))
                {
                    nodes.Add(words[0]);
                    indexes.Add(words[0], current);
                    current++;
                }
                if (!nodes.Contains(words[1]))
                {
                    nodes.Add(words[1]);
                    indexes.Add(words[1], current);
                    current++;
                }
                if (current > count) throw new Exception("Found more node names than nodes");
                costs[indexes[words[0]], indexes[words[1]]] = int.Parse(words[2]);
                costs[indexes[words[1]], indexes[words[0]]] = int.Parse(words[2]);
            }
            if (current < count) throw new Exception("Found less node names than nodes");
        }

        /// <summary>
        /// Returns the cost of going from a node to another using their names
        /// </summary>
        /// <param name="start">Start</param>
        /// <param name="end">End</param>
        /// <returns>Cost</returns>
        /// <exception cref="ArgumentException">Invalid node</exception>
        public int getCost(string start, string end)
        {
            if (!indexes.ContainsKey(start) || !indexes.ContainsKey(end))
                throw new ArgumentException();
            return costs[indexes[start], indexes[end]];
        }

        /// <summary>
        /// Returns the cost of going from a node to another using their indexes
        /// </summary>
        /// <param name="start">Start</param>
        /// <param name="end">End</param>
        /// <returns>Cost</returns>
        /// <exception cref="ArgumentException">Invalid node</exception>
        public int getCost(int start, int end)
        {
            if (start < 0 || end >= costs.Length)
                throw new ArgumentException();
            return costs[start, end];
        }

        /// <summary>
        /// Returns the cost of a route
        /// </summary>
        /// <param name="route">Route</param>
        /// <returns>Cost</returns>
        public int getRouteCost(List<string> route)
        {
            int cost = 0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                cost += getCost(route[i], route[i + 1]);
            }
            return cost;
        }
    }
}
