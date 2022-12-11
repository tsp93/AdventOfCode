using AdventOfCode.Enums;
using AdventOfCode.Services.Utils;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver : ISolver
    {
        private readonly IUtil Util;

        public Solver(IUtil Util)
        {
            this.Util = Util;
        }

        public async Task<List<string>> GetSolution(List<string> input, AdventDays day) => day switch
        {
            AdventDays.Day1 => await SolveDay1(input),
            AdventDays.Day2 => await SolveDay2(input),
            AdventDays.Day3 => await SolveDay3(input),
            AdventDays.Day4 => await SolveDay4(input),
            AdventDays.Day5 => await SolveDay5(input),
            AdventDays.Day6 => await SolveDay6(input),
            AdventDays.Day7 => await SolveDay7(input),
            AdventDays.Day8 => await SolveDay8(input),
            AdventDays.Day9 => await SolveDay9(input),
            AdventDays.Day10 => await SolveDay10(input),
            AdventDays.Day11 => await SolveDay11(input),
            AdventDays.Day12 => throw new NotImplementedException(),
            AdventDays.Day13 => throw new NotImplementedException(),
            AdventDays.Day14 => throw new NotImplementedException(),
            AdventDays.Day15 => throw new NotImplementedException(),
            AdventDays.Day16 => throw new NotImplementedException(),
            AdventDays.Day17 => throw new NotImplementedException(),
            AdventDays.Day18 => throw new NotImplementedException(),
            AdventDays.Day19 => throw new NotImplementedException(),
            AdventDays.Day20 => throw new NotImplementedException(),
            AdventDays.Day21 => throw new NotImplementedException(),
            AdventDays.Day22 => throw new NotImplementedException(),
            AdventDays.Day23 => throw new NotImplementedException(),
            AdventDays.Day24 => throw new NotImplementedException(),
            AdventDays.Day25 => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
