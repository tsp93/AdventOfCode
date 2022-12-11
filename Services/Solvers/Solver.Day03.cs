namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay3(List<string> input) =>
            Task.FromResult(new List<string>
            { 
                SumPriorityInRucksack(input).ToString(),
                SumBadgePriorityInRucksack(input).ToString(),
            });

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
        /// Splits list of input strings into groups of three, finds common char between them
        /// and sums the char priority of all found
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int SumBadgePriorityInRucksack(List<string> input)
        {
            List<List<string>> groupRucksacks = new List<List<string>>();
            List<string> groupRucksack = new List<string>();
            int beginCounter = 0;
            int groupSize = 3;
            while (true)
            {
                groupRucksack = input.Skip(beginCounter).Take(groupSize).ToList();
                if (!groupRucksack.Any())
                    break;
                groupRucksacks.Add(groupRucksack);
                beginCounter += groupSize;
            }

            List<char> commonChars = groupRucksacks.Select(x => GetCommonCharacterInThreeString(x[0], x[1], x[2])).ToList();
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
        /// Takes in two strings and finds the first common char between them
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

        /// <summary>
        /// Takes in three strings and finds the first common char between them
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Common char</returns>
        private char GetCommonCharacterInThreeString(string first, string second, string third)
        {
            List<char> firstCharArray = first.ToCharArray().ToList();
            List<char> secondCharArray = second.ToCharArray().ToList();
            List<char> thirdCharArray = third.ToCharArray().ToList();
            List<char> commonChars = firstCharArray.Where(secondCharArray.Contains).ToList();
            char commonChar = commonChars.First(thirdCharArray.Contains);
            return commonChar;
        }
    }
}
