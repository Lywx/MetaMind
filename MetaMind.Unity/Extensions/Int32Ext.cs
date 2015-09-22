namespace MetaMind.Unity.Extensions
{
    internal static class Int32Ext
    {
        public static string ToSummary(this int hour)
        {
            return hour.ToString("+#;-#;+0");
        }
    }
}