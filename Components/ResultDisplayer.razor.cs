using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Components
{
    public partial class ResultDisplayer
    {
        [Parameter]
        public List<string> Results { get; set; }

        protected override void OnInitialized()
        {
            Results = new List<string>();
        }
    }
}
