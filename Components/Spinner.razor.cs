using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Components
{
    public partial class Spinner
    {
        [Parameter]
        public string Label { get; set; }

        private bool IsVisible { get; set; }

        public void Show()
        {
            this.IsVisible = true;
            StateHasChanged();
        }

        public void Hide()
        {
            this.IsVisible = false;
            StateHasChanged();
        }
    }
}
