namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay4(List<string> input) =>
            Task.FromResult(new List<string>
            {
                AssignmentPairsFullyContainsOther(input).ToString(),
                AssignmentPairsOverlapOther(input).ToString(),
            });

        /// <summary>
        /// Takes in input list of pairs of ranges and sums the count of ones fully containing the other
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Number of containing pairs</returns>
        private int AssignmentPairsFullyContainsOther(List<string> input)
        {
            List<bool> assignmentPairs = input.Select(x => x.Split(',')).Select(x => RangesContainEithorOfOther(x[0], x[1])).ToList();

            return assignmentPairs.Select(x => x ? 1 : 0).Sum();
        }

        /// <summary>
        /// Takes in input list of pairs of ranges and sums the count of ones fully containing the other
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Number of containing pairs</returns>
        private int AssignmentPairsOverlapOther(List<string> input)
        {
            List<bool> assignmentPairs = input.Select(x => x.Split(',')).Select(x => RangesOverlap(x[0], x[1])).ToList();

            return assignmentPairs.Select(x => x ? 1 : 0).Sum();
        }

        /// <summary>
        /// Converts a range string of the format x-y to a string of x...y
        /// </summary>
        /// <param name="range"></param>
        /// <returns>Range string</returns>
        private bool RangesContainEithorOfOther(string firstRange, string secondRange)
        {
            string[] firstRangeNumbers = firstRange.Split('-');
            (int firstBeginRange, int firstEndRange) = (Util.StringToInt(firstRangeNumbers[0]), Util.StringToInt(firstRangeNumbers[1]));
            string[] secondRangeNumbers = secondRange.Split('-');
            (int secondBeginRange, int secondEndRange) = (Util.StringToInt(secondRangeNumbers[0]), Util.StringToInt(secondRangeNumbers[1]));

            return (firstBeginRange <= secondBeginRange && firstEndRange >= secondEndRange)
                || (secondBeginRange <= firstBeginRange && secondEndRange >= firstEndRange);
        }

        /// <summary>
        /// Converts a range string of the format x-y to a string of x...y
        /// </summary>
        /// <param name="range"></param>
        /// <returns>Range string</returns>
        private bool RangesOverlap(string firstRange, string secondRange)
        {
            string[] firstRangeNumbers = firstRange.Split('-');
            (int firstBeginRange, int firstEndRange) = (Util.StringToInt(firstRangeNumbers[0]), Util.StringToInt(firstRangeNumbers[1]));
            string[] secondRangeNumbers = secondRange.Split('-');
            (int secondBeginRange, int secondEndRange) = (Util.StringToInt(secondRangeNumbers[0]), Util.StringToInt(secondRangeNumbers[1]));

            return (firstBeginRange >= secondBeginRange && firstBeginRange <= secondEndRange)
                || (secondBeginRange >= firstBeginRange && secondBeginRange <= firstEndRange)
                || (firstEndRange <= secondEndRange && firstEndRange >= secondBeginRange)
                || (secondEndRange <= firstEndRange && secondEndRange >= firstBeginRange);
        }
    }
}
