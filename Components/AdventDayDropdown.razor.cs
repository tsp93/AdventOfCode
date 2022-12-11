using AdventOfCode.Enums;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Components
{
    public partial class AdventDayDropdown : ComponentBase
    {
        [Parameter]
        public AdventDays SelectedAdventDay { get; set; } = AdventDays.Day1;
        [Parameter]
        public EventCallback<AdventDays> SelectedAdventDayChanged { get; set; }
        private string SelectedAdventDayString
        {
            get => this.SelectedAdventDay.ToString();
            set
            {
                AdventDays test;
                Enum.TryParse(value, out test);
                this.SelectedAdventDay = test;
                SelectedAdventDayChanged.InvokeAsync(this.SelectedAdventDay);
            }
        }

        public List<string> AdventDaysList { get; set; }

        protected override void OnInitialized()
        {
            AdventDaysList = Enum.GetValues(typeof(AdventDays)).Cast<AdventDays>().Select(x => x.ToString()).ToList();
        }
    }
}
