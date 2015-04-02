namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MetaMind.Engine.Settings.Loaders;

    public class Format : IConfigurationLoader
    {
        public static int DefaultHeadLength;

        public Format()
        {
        }

        ~Format()
        {
        }

        public string ConfigurationFile
        {
            get
            {
                return "Format.txt";
            }
        }

        public static string Compose(IEnumerable<string> heads, int headLength, string headStart, string headEnd, string info, string infoStart, string infoEnd)
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

        /// <summary>
        /// Default format composing.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Compose(string text)
        {
            return Compose(text, string.Empty, "> ", string.Empty, string.Empty);
        }

        public static string Compose(string text, string headStart, string headEnd, string infoStart, string infoEnd)
        {
            return Compose(text, DefaultHeadLength, headStart, headEnd, infoStart, infoEnd);
        }

        public static string Compose(string text, int headLength, string headStart, string headEnd, string infoStart, string infoEnd)
        {
            return Compose(GetHeads(text), headLength, headStart, headEnd, GetInfo(text), infoStart, infoEnd);
        }

        public static List<string> Disintegrate(string composed, int headLength, string headStart, string headEnd, string infoStart, string infoEnd)
        {
            // head part of composed string 
            // like "headStart head1 headEnd headStart head2 headEnd ... headStart headN headEnd"
            var headAllLength = composed.LastIndexOf(headEnd, StringComparison.Ordinal) + headEnd.Length;
            var headAllString = composed.Substring(0, headAllLength);

            // info part of composed string 
            // like "infoStart info infoEnd"
            var infoAllString = composed.Replace(headAllString, string.Empty);

            var elems = new List<string>();

            // head element 
            // like "headStart + head + headEnd"
            var headElemLength = headLength + headStart.Length + headEnd.Length;
            var headElemCount = headAllString.Length / headElemLength;

            // add head elems
            for (var i = 0; i < headElemCount; i++)
            {
                var head = headAllString.Substring(i * headElemLength, headElemLength)
                    .TrimStart(headStart.ToCharArray())
                    .TrimEnd(headEnd.ToCharArray())
                    .Trim();

                elems.Add(head);
            }

            // add info elem
            var info = infoAllString
                .TrimStart(infoStart.ToCharArray())
                .TrimEnd(infoEnd.ToCharArray())
                .Trim();

            elems.Add(info);

            return elems;
        }

        public static List<string> GetHeads(string text)
        {
            return text.Split(':').First().Split('.').ToList();
        }

        public static string GetInfo(string text)
        {
            return text.Split(':').Last().Trim();
        }

        public static void Load(IConfigurationLoader loader)
        {
            LoadFormatLength(loader);
        }

        public static string Paddle(string target, string reference)
        {
            return Paddle(target, reference, DefaultHeadLength, string.Empty, "> ", string.Empty, string.Empty);
        }

        public static string Paddle(string target, string reference, int headLength, string headStart, string headEnd, string infoStart, string infoEnd)
        {
            var targetElems    = Disintegrate(target   , headLength, headStart, headEnd, infoStart, infoEnd);
            var referenceElems = Disintegrate(reference, headLength, headStart, headEnd, infoStart, infoEnd);

            return Paddle(targetElems, referenceElems, headLength, headStart, headEnd, infoStart, infoEnd);
        }

        public static string Paddle(List<string> target, List<string> reference, int headLength, string headStart, string headEnd, string infoStart, string infoEnd)
        {
            var targetLonger = target.Count > reference.Count;

            var longer  = targetLonger ? target : reference;
            var shorter = targetLonger ? reference : target;

            var commonHeads   = new List<string>();
            var uncommonHeads = new List<string>();

            for (var i = 0; i < shorter.Count - 1; i++)
            {
                if (shorter[i] == longer[i])
                {
                    commonHeads.Add(Text.WhiteSpace(headLength));
                }
                else
                {
                    var alreadyAdded = commonHeads.Count;
                    uncommonHeads.AddRange(shorter.GetRange(i, shorter.Count - 1 - alreadyAdded));

                    break;
                }
            }

            var paddledHeads = commonHeads.GetRange(0, commonHeads.Count - 1);

            // unpaddled heads are those add non-whitespace headEnd after heads
            var unpaddledHeads = new List<string>(uncommonHeads);
            unpaddledHeads.Insert(0, commonHeads.Last());

            var paddled   = Compose(paddledHeads, headLength, headStart, "  ", string.Empty, infoStart, infoEnd);
            var unpaddled = Compose(unpaddledHeads, headLength, headStart, headEnd, string.Empty, infoStart, infoEnd);
            
            var extraHeads = new List<string>();

            if (targetLonger)
            {
                // add heads in exceeding positions
                for (var i = shorter.Count - 1; i < longer.Count - 1; i++)
                {
                    extraHeads.Add(longer[i]);
                }
            }

            return paddled + unpaddled + Compose(extraHeads, headLength, headStart, headEnd, target.Last(), infoStart, infoEnd);
        }

        private static void LoadFormatLength(IConfigurationLoader loader)
        {
            var dict = ConfigurationLoader.LoadUniquePairs(loader);

            DefaultHeadLength = ConfigurationLoader.MultipleIntValue(dict, "HeadLength", 0, 10);
        }
    }
}