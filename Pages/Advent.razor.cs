using AdventOfCode.Components;
using AdventOfCode.Enums;
using AdventOfCode.Services.Solvers;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Pages
{
    public partial class Advent
    {
        [Inject]
        private ISolver Solver { get; set; }

        public List<string> Inputs { get; set; }
        public AdventDays AdventDay { get; set; }
        public List<string> Solutions { get; set; }
        private Spinner Spinner { get; set; }

        private async void SolveForInput()
        {
            this.Solutions = new List<string>();
            this.Spinner.Show();
            try
            {
                this.Solutions = await this.Solver.GetSolution(this.Inputs, this.AdventDay);
            }
            catch(Exception ex)
            {
                this.Solutions.Add("Input for the day in question is probably wrong, actual error is:");
                this.Solutions.Add(ex.Message);
            }
            this.Spinner.Hide();
        }
    }
}
