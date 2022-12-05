using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public interface ISolver
    {
        string GetSolution(List<string> inputs, AdventDays day);
    }
}
