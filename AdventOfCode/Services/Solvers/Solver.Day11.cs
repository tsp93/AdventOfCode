using System.Runtime.CompilerServices;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay11(List<string> input) =>
            Task.FromResult(new List<string>
            {
                TopTwoMonkeyBusiness(input).ToString(),
                //LongerRopeTailVisited(input).ToString(),
            });

        /// <summary>
        /// Finds the top two inspector monkeys
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int TopTwoMonkeyBusiness(List<string> input)
        {
            Dictionary<int, Monkey> monkeys = NormaliseInputToMonkey(input);
            Dictionary<int, Monkey> afterMonkeyInspections = MonkeyInspections(monkeys, 20);
            return afterMonkeyInspections.Values.OrderByDescending(x => x.AmountOfInspections)
                                                .Take(2)
                                                .Select(x => x.AmountOfInspections)
                                                .Aggregate((a, b) => a * b);
        }

        /// <summary>
        /// Returns list of monkeys having gone through a number of rounds inspecting items
        /// </summary>
        /// <param name="monkeys"></param>
        /// <param name="rounds"></param>
        /// <returns></returns>
        private Dictionary<int, Monkey> MonkeyInspections(Dictionary<int, Monkey> monkeys, int rounds)
        {
            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (int item in monkey.Value.StartingItems)
                    {
                        monkey.Value.AmountOfInspections++;
                        int val = monkey.Value.Operation.Value == "old" ? item : Util.StringToInt(monkey.Value.Operation.Value);
                        switch (monkey.Value.Operation.Operator)
                        {
                            case "+":
                                val += item;
                                break;
                            case "*":
                                val *= item;
                                break;
                        }
                        val = val / 3;
                        if (val % monkey.Value.TestDivisibleBy == 0)
                        {
                            monkeys[monkey.Value.ThrowToTrue].StartingItems.Add(val);
                        }
                        else
                        {
                            monkeys[monkey.Value.ThrowToFalse].StartingItems.Add(val);
                        }
                    }
                    monkey.Value.StartingItems.Clear();
                }
            }
            return monkeys;
        }

        /// <summary>
        /// Normalises input to something tangible
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Dictionary<int, Monkey> NormaliseInputToMonkey(List<string> input)
        {
            input = input.Select(x => x.Trim())
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(x => x.Replace(":", ""))
                        .Select(x => x.Replace(",", ""))
                        .ToList();
            int amountOfMonkeys = input.Where(x => x.StartsWith("Mon")).Count();
            int monkeyLines = 6;
            Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();
            for (int i = 0; i < amountOfMonkeys; i++)
            {
                List<string> monkeyStrings = input.Skip(i * monkeyLines).Take(monkeyLines).ToList();
                monkeys[i] = ConvertToMonkey(monkeyStrings);
            }
            return monkeys;
        }

        /// <summary>
        /// Converts list of monkey strings to monkey class
        /// </summary>
        /// <param name="monkeyStrings"></param>
        /// <returns></returns>
        private Monkey ConvertToMonkey(List<string> monkeyStrings)
        {
            Monkey monkey = new Monkey();
            foreach (string monkeyString in monkeyStrings)
            {
                List<string> splits = monkeyString.Split(" ").ToList();
                switch (splits[0])
                {
                    case "Monkey":
                        break;
                    case "Starting":
                        monkey.StartingItems = splits.Skip(2).Take(splits.Count() - 2).Select(Util.StringToInt).ToList();
                        break;
                    case "Operation":
                        monkey.Operation = (splits[4], splits[5]);
                        break;
                    case "Test":
                        monkey.TestDivisibleBy = Util.StringToInt(splits[3]);
                        break;
                    case "If":
                        switch (splits[1])
                        {
                            case "true":
                                monkey.ThrowToTrue = Util.StringToInt(splits[5]);
                                break;
                            case "false":
                                monkey.ThrowToFalse = Util.StringToInt(splits[5]);
                                break;
                        }
                        break;
                }
            }
            return monkey;
        }

        public class Monkey
        {
            public List<int> StartingItems { get; set; }
            public (string Operator, string Value) Operation { get; set; }
            public int TestDivisibleBy { get; set; }
            public int ThrowToTrue { get; set; }
            public int ThrowToFalse { get; set; }
            public int AmountOfInspections { get; set; } = 0;
        }
    }
}
