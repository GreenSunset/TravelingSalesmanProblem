using System.Diagnostics;
using System.Threading;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Class to measure and limit time for an algorithm
    /// </summary>
    internal class TimedAlgorithm
    {
        public Stopwatch stopwatch { get; } = new Stopwatch();
        private bool outOfTime = false;
        private ITSPAlgorithm algorithm;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="algorithm">Self explainatory</param>
        public TimedAlgorithm(ITSPAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        /// <summary>
        /// Starts the timer on the execution of the algorithm for a problem
        /// </summary>
        /// <param name="problem">Graph</param>
        /// <returns>Solution to the problem</returns>
        public TSPSolution Start(TSPGraph problem)
        {
            TSPSolution solution = new TSPSolution();
            Thread stoppingThread = new Thread(new ThreadStart(() => WaitAndAbort(ref algorithm.abort)));
            //Thread solvingThread = new Thread(new ThreadStart(() => Solve(ref solution, problem)));
            stopwatch.Restart();
            stoppingThread.Start();
            //solvingThread.Start();
            solution = algorithm.Solve(problem);
            stopwatch.Stop();
            algorithm.abort = true;
            stoppingThread.Join();
            return solution;
        }

        /// <summary>
        /// (Supposetly) limits the time the algorithms is able to execute for
        /// </summary>
        /// <param name="abortButton">Signal to abort the execution</param>
        /// <param name="time">time limit</param>
        private void WaitAndAbort(ref bool abortButton, long time = 300000)
        {
            for (long timeSlept = 0; timeSlept < time;timeSlept += 100)
            {
                Thread.Sleep(100);
                if (abortButton) return;
            }
            outOfTime = true;
            abortButton = true;
        }

        /// <summary>
        /// (Unused) Starts the solving process on a different thread
        /// </summary>
        /// <param name="solution">Variable to store the solution</param>
        /// <param name="problem">Problem</param>
        private void Solve(ref TSPSolution solution, TSPGraph problem)
        {
            solution = algorithm.Solve(problem);
        }

        /// <summary>
        /// Returns the time taken by the algorithm
        /// </summary>
        /// <returns>Time</returns>
        public string GetTime()
        {
            if (outOfTime) return "EXCESIVO";
            return stopwatch.ElapsedMilliseconds.ToString();
        }
    }
}
