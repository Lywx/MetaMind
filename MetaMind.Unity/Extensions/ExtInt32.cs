namespace MetaMind.Unity.Extensions
{
    internal static class ExtInt32
    {
        public static string ToSummary(this int hour)
        {
            return hour.ToString("+#;-#;+0");
        }
    }
}