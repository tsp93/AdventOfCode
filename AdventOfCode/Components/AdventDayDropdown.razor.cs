using AdventOfCode.Enums;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Components
{
    public partial class AdventDayDropdown : ComponentBase
    {

        public AdventDays SelectedAdventDay { get; set; } = AdventDays.Day1;
        private string SelectedAdventDayString
        {
            get => this.SelectedAdventDay.ToString();
            set
            {
                AdventDays test;
                Enum.TryParse(value, out test);
                this.SelectedAdventDay = test;
            }
        }

        public List<string> AdventDaysList { get; set; }

        protected override void OnInitialized()
        {
            AdventDaysList = Enum.GetValues(typeof(AdventDays)).Cast<AdventDays>().Select(x => x.ToString()).ToList();
        }
    }
}
