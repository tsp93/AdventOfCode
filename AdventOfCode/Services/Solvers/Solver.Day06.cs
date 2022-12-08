﻿namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private List<string> SolveDay6(List<string> input)
        {
            return new List<string>
            {
                GetStartMarker(input, 4).ToString(),
                GetStartMarker(input, 14).ToString(),
            };
        }

        /// <summary>
        /// Takes in an input string and finds the first 4 row of chars, each of which are distinct
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int GetStartMarker(List<string> input, int amountOfCharsNeeded)
        {
            char[] dataStreamBuffer = input.First().ToCharArray();
            for (int i = 0; i < dataStreamBuffer.Count() - amountOfCharsNeeded; i++)
            {
                List<char> chars = dataStreamBuffer.Skip(i).Take(amountOfCharsNeeded).ToList();
                if (chars.Distinct().Count() == amountOfCharsNeeded)
                {
                    return i + amountOfCharsNeeded;
                }
            }
            return 0;
        }
    }
}
