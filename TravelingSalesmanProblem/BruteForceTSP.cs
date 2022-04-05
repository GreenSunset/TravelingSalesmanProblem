using System.Collections.Generic;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Class to solve the TSP by Brute Force
    /// </summary>
    internal class BruteForceTSP : ITSPAlgorithm
    {
        /// <summary>
        /// Solving method
        /// </summary>
        /// <param name="problem">Problem</param>
        /// <returns>Solution</returns>
        public override TSPSolution Solve(TSPGraph problem)
        {
            List<string> nodes = new List<string>(problem.nodes);
            string start = nodes[0];
            nodes.RemoveAt(0);
            List<List<string>> routes = Utils.getAllPermutations<string>(nodes);
            foreach (List<string> route in routes)
            {
                route.Insert(0, start);
                route.Add(start);
            }
            int minCost = problem.getRouteCost(routes[0]);
            int bestRoute = 0;
            for (int i = 1; i < routes.Count; i++)
            {
                if (abort) break;
                int cost = problem.getRouteCost(routes[i]);
                if (cost < minCost)
                {
                    minCost = cost;
                    bestRoute = i;
                }
            }
            return new TSPSolution(routes[bestRoute], minCost);
        }

    }
}
