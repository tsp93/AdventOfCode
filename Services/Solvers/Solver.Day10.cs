namespace AdventOfCode.Services.Solvers
{
    public partial class Solver
    {
        private Task<List<string>> SolveDay10(List<string> input)
        {
            List<string> solutions = new List<string>
            {
                FindSumOfSignalStrengths(input).ToString()
            };
            solutions.AddRange(DrawCRT(input));
            return Task.FromResult(solutions);
        }

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
        /// Returns a list of strings containing drawn crt
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<string> DrawCRT(List<string> input)
        {
            Dictionary<int, int> registerPerCycle = GetRegisterPerCycle(input);
            List<List<string>> crt = CalculateCrt(registerPerCycle);
            List<string> crtStrings = crt.Select(x => string.Join("", x)).ToList();
            return crtStrings;
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

            Dictionary<int, int> registerPerCycle = new Dictionary<int, int>();

            foreach (var inpy in input)
            {
                if (inpy.StartsWith("n"))
                {
                    cycleCounter++;
                    registerPerCycle.Add(cycleCounter, register);
                }
                else
                {
                    cycleCounter += 2;
                    registerPerCycle.Add(cycleCounter - 1, register);
                    registerPerCycle.Add(cycleCounter, register);
                    register += Util.StringToInt(inpy.Split(" ")[1]);
                }
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
                sum += registerPerCycle[cycle] * cycle;
            }
            return sum;
        }

        /// <summary>
        /// Calculates crt from given input
        /// </summary>
        /// <param name="registerPerCycle"></param>
        /// <returns></returns>
        private List<List<string>> CalculateCrt(Dictionary<int, int> registerPerCycle)
        {
            int crtWidth = 40;
            int crtHeight = 6;
            List<List<string>> crt = new List<List<string>>();

            string litPixel = "#";
            string darkPixel = ".";

            for (int i = 0; i < crtHeight; i++)
            {
                List<string> crtLine = new List<string>();
                for (int j = 0; j < crtWidth; j++)
                {
                    int cycle = j + 1;
                    int dictLocation = cycle + crtWidth * i;
                    int registerAtcycle = registerPerCycle[dictLocation];
                    List<int> locationsToCheck = new List<int>
                    {
                        registerAtcycle - 1,
                        registerAtcycle,
                        registerAtcycle + 1,
                    };

                    if (locationsToCheck.Contains(j))
                    {
                        crtLine.Add(litPixel);
                    }
                    else
                    {
                        crtLine.Add(darkPixel);
                    }
                }
                crt.Add(crtLine);
            }
            return crt;
        }
    }
}
