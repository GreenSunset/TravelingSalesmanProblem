using System.Collections.Generic;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Class to solve the TSP using a Greedy algorithm
    /// </summary>
    internal class GreedyTSP : ITSPAlgorithm
    {
        /// <summary>
        /// Solving method
        /// </summary>
        /// <param name="problem">Problem</param>
        /// <returns>Solution</returns>
        public override TSPSolution Solve(TSPGraph problem)
        {
            List<string> nodes = new List<string>(problem.nodes);
            List<string> route = new List<string>();
            route.Add(nodes[0]);
            nodes.Remove(nodes[0]);
            int totalCost = 0;
            while (nodes.Count > 0)
            {
                if (abort) break;
                int min = problem.getCost(route[route.Count - 1], nodes[0]);
                string next = nodes[0];
                for (int i = 1; i < nodes.Count; i++)
                {
                    if (abort) break;
                    int cost = problem.getCost(route[route.Count - 1], nodes[i]);
                    if (cost < min)
                    {
                        min = cost;
                        next = nodes[i];
                    }
                }
                totalCost += min;
                route.Add(next);
                nodes.Remove(next);
            }
            totalCost += problem.getCost(route[route.Count - 1], route[0]);
            route.Add(route[0]);
            return new TSPSolution(route, totalCost);
        }
    }
}
