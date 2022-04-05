using System;
using System.Collections.Generic;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Static class to store some useful methods
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Function that returns all the permutations of a list
        /// </summary>
        /// <typeparam name="T">Type of the list</typeparam>
        /// <param name="list">Base List</param>
        /// <returns>List of permutations</returns>
        static public List<List<T>> getAllPermutations<T>(List<T> list)
        {
            List<List<T>> result = new List<List<T>>();
            int index = 0;
            recursivePermute<T>(list, ref result, ref index);
            return result;
        }

        /// <summary>
        /// Function that returns bitwise combinations of 0s and 1s
        /// </summary>
        /// <param name="ones">Number of ones to use</param>
        /// <param name="size">Number of bits to use</param>
        /// <returns>List of combinations</returns>
        /// <exception cref="ArgumentException"></exception>
        static public List<int> bitCombinations(int ones, int size)
        {
            if (ones > size) throw new ArgumentException();
            List<int> result = new List<int>();
            bitCombine(0, 0, ones, size, result);
            return result;
        }

        /// <summary>
        /// Private auxilary method for getAllPermutations
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">List to permute</param>
        /// <param name="result">Result list</param>
        /// <param name="index">Current number of permutations</param>
        /// <param name="previous">Values to add to a permutation</param>
        static private void recursivePermute<T>(List<T> list, ref List<List<T>> result, ref int index, List<T> previous = null)
        {
            if (list.Count == 0) return;
            foreach (T item in list)
            {
                List<T> sublist = new List<T>(list);
                sublist.Remove(item);
                if (previous == null) result.Add(new List<T>());
                else if (index >= result.Count) result.Add(new List<T>(previous));
                result[index].Add(item);
                recursivePermute<T>(sublist, ref result, ref index, new List<T>(result[index]));
            }
            if (list.Count == 1) index++;
        }

        /// <summary>
        /// Auxilary method for bitCombinations
        /// </summary>
        /// <param name="set">Accumulator</param>
        /// <param name="at"></param>
        /// <param name="ones">Number of remaining ones</param>
        /// <param name="size">Size of the number in bits</param>
        /// <param name="result">Result</param>
        static private void bitCombine(int set, int at, int ones, int size, List<int> result)
        {
            if (ones == 0) result.Add(set);
            else
                for (int i = at; i < size; i++)
                {
                    set |= (1 << i);
                    bitCombine(set, i + 1, ones - 1, size, result);
                    set &= ~(1 << i);
                }

        }
    }
}
