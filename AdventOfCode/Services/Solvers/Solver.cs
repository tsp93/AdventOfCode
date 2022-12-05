using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public partial class Solver : ISolver
    {
        public List<string> GetSolution(List<string> input, AdventDays day) => day switch
        {
            AdventDays.Day1 => SolveDay1(input),
            AdventDays.Day2 => throw new NotImplementedException(),
            AdventDays.Day3 => throw new NotImplementedException(),
            AdventDays.Day4 => throw new NotImplementedException(),
            AdventDays.Day5 => throw new NotImplementedException(),
            AdventDays.Day6 => throw new NotImplementedException(),
            AdventDays.Day7 => throw new NotImplementedException(),
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
