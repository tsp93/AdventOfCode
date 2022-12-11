using AdventOfCode.Enums;

namespace AdventOfCode.Services.Solvers
{
    public interface ISolver
    {
        Task<List<string>> GetSolution(List<string> input, AdventDays day);
    }
}
