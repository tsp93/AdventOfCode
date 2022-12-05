using AdventOfCode.Components;
using AdventOfCode.Enums;
using AdventOfCode.Services.Solvers;
using Microsoft.AspNetCore.Components;
using System.Collections;

namespace AdventOfCode.Pages
{
    public partial class Advent
    {
        [Inject]
        private ISolver Solver { get; set; }

        private InputFileAdvent InputFile { get; set; }
        private string Solution { get; set; }
        private AdventDays AdventDay { get; set; }

        private void SolveForInput()
        {

        }
    }
}
