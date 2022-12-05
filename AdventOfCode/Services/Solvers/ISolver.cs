using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public interface ISolver
    {
        List<string> GetSolution(List<string> input, AdventDays day);
    }
}
