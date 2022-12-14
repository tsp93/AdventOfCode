namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay1(List<string> input) =>
            Task.FromResult(new List<string>
            {
                FindElfWithMostCalories(input).ToString(),
                FindTopThreeElvesWithMostCalories(input).ToString(),
            });

        /// <summary>
        /// Finds the elf with the most calories
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Calories</returns>
        private int FindElfWithMostCalories(List<string> input)
        {
            Dictionary<int, int> totalCalPerElf = GetTotalCaloriesPerElf(input);
            int elfWithDaMost = totalCalPerElf.Values.Max();

            return elfWithDaMost;
        }

        /// <summary>
        /// Finds the top three elves with the most calories
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Sum of calories</returns>
        private int FindTopThreeElvesWithMostCalories(List<string> input)
        {
            Dictionary<int, int> totalCalPerElf = GetTotalCaloriesPerElf(input);
            int topThreeElvesWithDaMost = totalCalPerElf.Values.OrderByDescending(x => x).Take(3).Sum();

            return topThreeElvesWithDaMost;
        }

        /// <summary>
        /// Converts input into a dictionary with total calories per elf
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Dictionary of elf per total calories</returns>
        private Dictionary<int, int> GetTotalCaloriesPerElf(List<string> input)
        {
            Dictionary<int, List<int>> calPerElf = new Dictionary<int, List<int>>();
            List<int> calList = new List<int>();
            int elfNum = 0;

            foreach (string inpy in input)
            {
                if (Util.IsEmptyString(inpy))
                {
                    calPerElf.Add(elfNum, calList);
                    calList = new List<int>();
                    elfNum++;
                }
                else
                {
                    calList.Add(Util.StringToInt(inpy));
                }
            }

            Dictionary<int, int> totalCalPerElf = calPerElf.ToDictionary(x => x.Key, x => x.Value.Sum());
            return totalCalPerElf;
        }
    }
}
