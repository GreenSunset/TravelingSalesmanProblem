using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Class to represent solutions to the TSP
    /// </summary>
    internal class TSPSolution
    {
        public List<string> sequence { get; }
        public int cost { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TSPSolution()
        {
            sequence = new List<string>();
            cost = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="solution">Route</param>
        /// <param name="solutionCost">Cost of the route</param>
        public TSPSolution(List<string> solution, int solutionCost)
        {
            sequence = solution;
            cost = solutionCost;
        }

        public string PrintSolution()
        {
            string result = "[";
            for (int i = 0; i < sequence.Count; i++)
            {
                result += sequence[i];
                if (i != sequence.Count - 1) result += ", ";
            }
            return result + "](" + cost + ")";
        }
    }
}
