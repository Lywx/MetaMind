namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Format
    {
        public static string Compose(IEnumerable<string> heads, int headLength, string headStart, string headEnd, string infoStart, string infoEnd, string info)
        {
            var result = new StringBuilder();

            foreach (var head in heads)
            {
                result
                    .Append(headStart)
                    .Append(head.PadRight(headLength))
                    .Append(headEnd);
            }

            result.
                Append(infoStart).
                Append(info).
                Append(infoEnd);

            return result.ToString();
        }

        public static string Compose(string text, int headLength, string headStart, string headEnd, string infoStart, string infoEnd)
        {
            return Compose(GetHeads(text), headLength, headStart, headEnd, infoStart, infoEnd, GetInfo(text));
        }

        public static List<string> GetHeads(string text)
        {
            return text.Split(':').First().Split('.').ToList();
        }

        public static string GetInfo(string text)
        {
            return text.Split(':').Last().Trim();
        }
    }
}