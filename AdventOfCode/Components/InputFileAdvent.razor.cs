using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Components
{
    public partial class InputFileAdvent : ComponentBase
    {
        [Parameter]
        public List<string> InputStrings { get; set; }
        [Parameter]
        public EventCallback<List<string>> InputStringsChanged { get; set; }

        public async void FileUploaded(byte[] fileBytes)
        {
            string result = System.Text.Encoding.UTF8.GetString(fileBytes);
            this.InputStrings = result.Split("\r\n").ToList();
            await InputStringsChanged.InvokeAsync(this.InputStrings);
        }
    }
}
