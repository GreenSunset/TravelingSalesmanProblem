
namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Parent class for TSP Solving Algorithms
    /// </summary>
    internal abstract class ITSPAlgorithm
    {
        /// <summary>
        /// Abort button
        /// </summary>
        public bool abort = false;

        /// <summary>
        /// Solving function
        /// </summary>
        /// <param name="problem">Problem</param>
        /// <returns>Solution</returns>
        public abstract TSPSolution Solve(TSPGraph problem);
    }
}
