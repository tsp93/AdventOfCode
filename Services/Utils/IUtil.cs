namespace AdventOfCode.Services.Utils
{
    public interface IUtil
    {
        int CharToInt(char c);
        int StringToInt(string s);
        decimal StringToDecimal(string s);
        bool IsNumber(string s);
        bool IsEmptyString(string s);
        int Abs(int x);
        decimal Floor(decimal d);
    }
}
