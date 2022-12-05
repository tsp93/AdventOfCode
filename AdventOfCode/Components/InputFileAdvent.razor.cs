using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Components
{
    public partial class InputFileAdvent : ComponentBase
    {
        public List<string> InputStrings { get; set; }

        public void FileUploaded(byte[] fileBytes)
        {
            string result = System.Text.Encoding.UTF8.GetString(fileBytes);
            InputStrings = result.Split("\r\n").ToList();
        }
    }
}
