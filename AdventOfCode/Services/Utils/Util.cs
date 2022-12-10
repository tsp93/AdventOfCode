namespace AdventOfCode.Services.Utils
{
    public class Util : IUtil
    {
        public int CharToInt(char c) =>
            int.Parse(c.ToString());

        public int StringToInt(string s) =>
            int.Parse(s.ToString());

        public bool IsNumber(string s) =>
            int.TryParse(s, out _);

        public bool IsEmptyString(string s) =>
            string.IsNullOrEmpty(s.Trim());

        public int Abs(int x) =>
            Math.Abs(x);
    }
}
