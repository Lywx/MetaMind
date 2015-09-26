namespace MetaMind.Unity.Extensions
{
    internal static class Int32Extension
    {
        public static string ToSummary(this int hour)
        {
            return hour.ToString("+#;-#;+0");
        }
    }
}