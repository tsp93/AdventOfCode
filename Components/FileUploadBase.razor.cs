using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace AdventOfCode.Components
{
    public partial class FileUploadBase : ComponentBase
    {
        public byte[] FileBytes { get; set; } = default!;

        [Parameter]
        public EventCallback<byte[]> FileBytesChanged { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        private void OnChange(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                FileBytes = file.Stream.ToArray();
            }
            FileBytesChanged.InvokeAsync(FileBytes);
        }
    }
}
