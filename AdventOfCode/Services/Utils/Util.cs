﻿namespace AdventOfCode.Services.Utils
{
    public class Util : IUtil
    {
        public int CharToInt(char c) =>
            int.Parse(c.ToString());

        public int StringToInt(string s) =>
            int.Parse(s.ToString());

        public bool IsNumber(string s) =>
            int.TryParse(s, out _);

    }
}
