using System;
using System.Collections.Generic;
namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Class to solve the TSP using Dynamic Programming
    /// </summary>
    internal class DynamicProgrammingTSP : ITSPAlgorithm
    {
        /// <summary>
        /// Solving Method
        /// </summary>
        /// <param name="problem">Problem</param>
        /// <returns>Solution</returns>
        public override TSPSolution Solve(TSPGraph problem)
        {
            int[,] costs = new int[problem.nodes.Count, (int)Math.Pow(2, problem.nodes.Count)];
            for (int i = 1; i < problem.nodes.Count; i++)
                costs[i, 1 | 1 << i] = problem.getCost(0, i);
            fillTable(ref costs, problem);
            List<string> solutionRoute = optimalRoute(ref costs, problem);
            int solutionCost = problem.getRouteCost(solutionRoute);
            return new TSPSolution(solutionRoute, solutionCost);
        }

        /// <summary>
        /// Fills the table of costs for different sets of nodes
        /// </summary>
        /// <param name="costs">Table of costs</param>
        /// <param name="problem">Problem</param>
        private void fillTable(ref int[,] costs, TSPGraph problem)
        {
            for (int explored = 3; explored <= problem.nodes.Count; explored++)
            {
                if (abort) break;
                List<int> combinations = Utils.bitCombinations(explored, problem.nodes.Count);
                foreach (int combination in combinations)
                {
                    if (abort) break;
                    if (combination % 2 == 0) continue;
                    for (int next = 1; next < problem.nodes.Count; next++)
                    {
                        if (abort) break;
                        if (((1 << next) & combination) == 0) continue;
                        int lastState = combination ^ (1 << next);
                        int min = int.MaxValue;
                        for (int lastExplored = 1; lastExplored < problem.nodes.Count; lastExplored++)
                        {
                            if (abort) break;
                            if (lastExplored == next || ((1 << lastExplored) & combination) == 0) continue;
                            int distance = costs[lastExplored, lastState] + problem.getCost(lastExplored, next);
                            if (min > distance) min = distance;
                        }
                        costs[next, combination] = min;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the optimal route from the table of costs
        /// </summary>
        /// <param name="costs">Table of costs</param>
        /// <param name="problem">Problem</param>
        /// <returns>Route</returns>
        private List<string> optimalRoute(ref int[,] costs, TSPGraph problem)
        {
            int last = 0;
            int lastState = (1 << problem.nodes.Count) - 1;
            List<string> route = new List<string>(problem.nodes.Count + 1);
            for (int explored = problem.nodes.Count - 1; explored >= 1; explored--)
            {
                if (abort) break;
                int index = -1;
                for (int node = 1; node < problem.nodes.Count; node++)
                {
                    if (abort) break;
                    if (((1 << node) & lastState) != 0 &&
                        (index == -1 ||
                        costs[index,lastState] + problem.getCost(index,last) > costs[node, lastState] + problem.getCost(node, last)))
                        index = node;
                }
                route.Insert(0,problem.nodes[index]);
                lastState ^= (1 << index);
                last = index;
            }
            route.Insert(0, problem.nodes[0]);
            route.Add(problem.nodes[0]);
            return route;
        }

        /// <summary>
        /// (Unused) Returns the route cost from the Table of costs.
        /// </summary>
        /// <param name="costs">Table of costs</param>
        /// <param name="problem">Problem</param>
        /// <returns>Final Cost</returns>
        private int minimalCost(ref int[,] costs, TSPGraph problem)
        {
            int END = (1 << problem.nodes.Count) - 1;
            int min = int.MaxValue;
            for (int endpoint = 1; endpoint < problem.nodes.Count; endpoint++)
            {
                if (abort) break;
                int distance = costs[endpoint, END] + problem.getCost(endpoint, 0);
                if (min > distance) min = distance;
            }
            return min;
        }
    }
}
