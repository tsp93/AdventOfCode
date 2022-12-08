namespace AdventOfCode.Services.Utils
{
    public interface IUtil
    {
        int CharToInt(char c);
        int StringToInt(string s);
        bool IsNumber(string s);
        bool IsEmptyString(string s);
    }
}
