namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private List<string> SolveDay3(List<string> input)
        {
            return new List<string>
            {
                SumPriorityInRucksack(input).ToString(),
                //TotalScoreFromStrategyGuideWithRightInfo(input).ToString()
            };
        }

        /// <summary>
        /// Splits input strings into two equal strings(compartments)
        /// and sums int value of priority of char that are common between them
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int SumPriorityInRucksack(List<string> input)
        {
            List<(string FirstCompartment, string SecondCompartment)> compartments =
                input.Select(x => (x.Substring(0, x.Length / 2), x.Substring(x.Length / 2, x.Length / 2))).ToList();

            List<char> commonChars = compartments.Select(x => GetCommonCharacterInTwoStrings(x.FirstCompartment, x.SecondCompartment)).ToList();
            List<int> charToPriority = commonChars.Select(GetCharPriority).ToList();

            return charToPriority.Sum();
        }

        /// <summary>
        /// Takes in char and converts to int in the following manner
        /// a-z -> 1-26, A-Z -> 27-52
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Priority int</returns>
        private int GetCharPriority(char c) =>
            char.IsUpper(c) ? c - 38 : c - 96;

        /// <summary>
        /// Takes in two string and finds the first common char between them
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Common char</returns>
        private char GetCommonCharacterInTwoStrings(string first, string second)
        {
            List<char> firstCharArray = first.ToCharArray().ToList();
            List<char> secondCharArray = second.ToCharArray().ToList();
            char commonChar = firstCharArray.First(secondCharArray.Contains);
            return commonChar;
        }
    }
}
