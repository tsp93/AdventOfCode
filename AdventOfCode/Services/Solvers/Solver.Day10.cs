namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay10(List<string> input) =>
            Task.FromResult(new List<string>
            {
                FindSumOfSignalStrengths(input).ToString(),
                //FindHighestScenicScore(input).ToString(),
            });

        /// <summary>
        /// Finds sums of signal strengths
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int FindSumOfSignalStrengths(List<string> input)
        {
            Dictionary<int, int> registerPerCycle = GetRegisterPerCycle(input);
            List<int> cyclesToCheck = new List<int>
            {
                20, 60, 100, 140, 180, 220,
            };
            return SumRegistersAtCycles(cyclesToCheck, registerPerCycle);
        }

        /// <summary>
        /// Gets registers per cycle from input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Dictionary<int, int> GetRegisterPerCycle(List<string> input)
        {
            int cycleCounter = 0;
            int register = 1;

            Dictionary<int, int> registerPerCycle = new Dictionary<int, int>
            {
                { cycleCounter, register },
            };

            foreach (var inpy in input)
            {
                if (inpy.StartsWith("n"))
                {
                    cycleCounter++;
                }
                else
                {
                    register += Util.StringToInt(inpy.Split(" ")[1]);
                    cycleCounter += 2;
                }
                registerPerCycle.Add(cycleCounter, register);
            }
            return registerPerCycle;
        }

        /// <summary>
        /// Sums register of particular cycles
        /// </summary>
        /// <param name="cycles"></param>
        /// <param name="registerPerCycle"></param>
        /// <returns></returns>
        private int SumRegistersAtCycles(List<int> cycles, Dictionary<int, int> registerPerCycle)
        {
            int sum = 0;
            foreach (int cycle in cycles)
            {
                if (registerPerCycle.ContainsKey(cycle - 1))
                {
                    sum += registerPerCycle[cycle - 1] * cycle;
                }
                else
                {
                    sum += registerPerCycle[cycle - 2] * cycle;
                }
            }
            return sum;
        }
    }
}
