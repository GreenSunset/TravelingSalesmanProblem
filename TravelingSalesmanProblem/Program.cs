using System;
using System.Diagnostics;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Main Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Returns data as a string organized in a table
        /// </summary>
        /// <param name="data">Contents of the table</param>
        /// <param name="rowNames">First column content</param>
        /// <param name="columnNames">Top ROw content</param>
        /// <returns>Table</returns>
        static public string PrintTable(string[,] data, string[] rowNames, string[] columnNames)
        {
            int maxtablelength = 0;
            for (int i = 0; i < data.GetLength(0); ++i)
                for (int j = 0; j < data.GetLength(1); ++j)
                    if (data[i, j].Length > maxtablelength) maxtablelength = data[i, j].Length;
            for (int i = 0; i < rowNames.Length; ++i)
                if (rowNames[i].Length > maxtablelength) maxtablelength = rowNames[i].Length;
            for (int i = 0; i < columnNames.Length; ++i)
                if (columnNames[i].Length > maxtablelength) maxtablelength = columnNames[i].Length;
            ++maxtablelength;
            string table = new string(' ', maxtablelength);
            for (int i = 0; i < columnNames.Length; ++i)
                table += new string(' ', maxtablelength - columnNames[i].Length) + columnNames[i];
            for (int i = 0; i < data.GetLength(0); ++i)
            {
                table += '\n';
                if (i < rowNames.Length) table += rowNames[i] + new string(' ', maxtablelength - rowNames[i].Length);
                else table += new string(' ', maxtablelength);
                for (int j = 0; j < data.GetLength(1); ++j)
                    table += new string(' ', maxtablelength - data[i, j].Length) + data[i, j];
            }
            return table;

        }

        /// <summary>
        /// Main program
        /// </summary>
        static void Main()
        {
            Stopwatch timer = new Stopwatch();
            TSPGraph[] graphs = new TSPGraph[] { new TSPGraph("4_nodos.txt"), new TSPGraph("6_nodos.txt") };
            TimedAlgorithm[] algorithms = new TimedAlgorithm[] {
                new TimedAlgorithm(new GreedyTSP()),
                new TimedAlgorithm(new BruteForceTSP()),
                new TimedAlgorithm(new DynamicProgrammingTSP()) };
            TSPSolution[,] solutions = new TSPSolution[algorithms.Length,graphs.Length];
            string[,] contents = new string[graphs.Length, algorithms.Length * 2];
            for (int i = 0; i < algorithms.Length; ++i)
            {
                for (int j = 0; j < graphs.Length; ++j)
                {
                    solutions[i,j] = algorithms[i].Start(graphs[j]);
                    // Console.WriteLine("Solution: " + solutions[i,j].PrintSolution());
                    contents[j, 2 * i] = solutions[i, j].cost.ToString();
                    contents[j, 2 * i + 1] = algorithms[i].stopwatch.ElapsedMilliseconds.ToString();
                    timer.Reset();
                }
            }
            string[] columns = new string[] { "Valor Greedy", "Tiempo Greedy", "Valor FB", "Tiempo FB", "Valor DP", "Tiempo DP" };
            string[] rows = new string[] { "4 nodos", "6 nodos" };
            Console.WriteLine(PrintTable(contents, rows, columns));
        }
    }
}
