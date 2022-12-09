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

        public List<string> GetSolution(List<string> input, AdventDays day) => day switch
        {
            AdventDays.Day1 => SolveDay1(input),
            AdventDays.Day2 => SolveDay2(input),
            AdventDays.Day3 => SolveDay3(input),
            AdventDays.Day4 => SolveDay4(input),
            AdventDays.Day5 => SolveDay5(input),
            AdventDays.Day6 => SolveDay6(input),
            AdventDays.Day7 => SolveDay7(input),
            AdventDays.Day8 => throw new NotImplementedException(),
            AdventDays.Day9 => throw new NotImplementedException(),
            AdventDays.Day10 => throw new NotImplementedException(),
            AdventDays.Day11 => throw new NotImplementedException(),
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
