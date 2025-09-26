using System;

namespace AkelTestingTool.Utils
{
    public static class DemoExtensions
    {
        public static string ToShortDateString(this DateTime input)
        {
            return input.ToString("d");
        }
    }
}