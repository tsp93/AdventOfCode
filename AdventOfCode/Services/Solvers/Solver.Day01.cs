namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private List<string> SolveDay1(List<string> input)
        {
            return new List<string>
            {
                FindElfWithMostCalories(input).ToString(),
                FindTopThreeElvesWithMostCalories(input).ToString()
            };
        }

        private int FindElfWithMostCalories(List<string> input)
        {
            Dictionary<int, int> totalCalPerElf = GetTotalCaloriesPerElf(input);
            int elfWithDaMost = totalCalPerElf.Values.Max();

            return elfWithDaMost;
        }

        private int FindTopThreeElvesWithMostCalories(List<string> input)
        {
            Dictionary<int, int> totalCalPerElf = GetTotalCaloriesPerElf(input);
            int topThreeElvesWithDaMost = totalCalPerElf.Values.OrderByDescending(x => x).Take(3).Sum();

            return topThreeElvesWithDaMost;
        }

        private Dictionary<int, int> GetTotalCaloriesPerElf(List<string> input)
        {
            Dictionary<int, List<int>> calPerElf = new Dictionary<int, List<int>>();
            List<int> calList = new List<int>();
            int elfNum = 0;

            foreach (string inpy in input)
            {
                if (string.IsNullOrEmpty(inpy))
                {
                    calPerElf.Add(elfNum, calList);
                    calList = new List<int>();
                    elfNum++;
                }
                else
                {
                    calList.Add(int.Parse(inpy));
                }
            }

            Dictionary<int, int> totalCalPerElf = calPerElf.ToDictionary(x => x.Key, x => x.Value.Sum());
            return totalCalPerElf;
        }
    }
}
