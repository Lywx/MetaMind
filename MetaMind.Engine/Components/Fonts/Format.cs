namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MetaMind.Engine.Settings.Loaders;

    public class Format : IConfigurationLoader
    {
        #region Format Parameters

        public static int DefaultHeadLength = 12;


        #endregion

        #region Constructors and Destructors

        public Format()
        {
        }

        ~Format()
        {
        }

        #endregion

        #region Formatting

        private enum LengthComparison
        {
            Longer,
            Equal,
            Shorter,
        }

        public static string Compose(IEnumerable<string> heads, int headLength, string headStart, string headEnd, string info, string infoStart, string infoEnd)
        {
            var result = new StringBuilder();

            foreach (var head in heads)
            {
                // TODO: CJK characters may suffer double ... when paddling
                var croppedHead = GameEngine.FontManager.CropMonoSpacedStringAsciiCount(head, headLength);
                var cjkCount    = GameEngine.FontManager.GetExclusiveCJKCharacterCount(croppedHead);

                result.
                    Append(headStart).

                    // cut out extra position taken out by double sized CJK characters
                    Append(croppedHead.PadRight(headLength - cjkCount)).
                    Append(headEnd);
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
        /// <param target="text"></param>
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
                var head =
                    headAllString.Substring(i * headElemLength, headElemLength)
                        .TrimStart(headStart.ToCharArray())
                        .TrimEnd(headEnd.ToCharArray())
                        .Trim();

                elems.Add(head);
            }

            // add info elem
            var info = infoAllString.TrimStart(infoStart.ToCharArray()).TrimEnd(infoEnd.ToCharArray()).Trim();

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
            var delta = target.Count - reference.Count;
            var comparison = delta > 0 ?        LengthComparison.Longer
                                 : delta == 0 ? LengthComparison.Equal 
                                       :        LengthComparison.Shorter;

            var max = comparison == LengthComparison.Longer ? target : reference;
            var min = comparison == LengthComparison.Longer ? reference : target;

            var commonHeads   = new List<string>();
            var uncommonHeads = new List<string>();

            for (var i = 0; i < min.Count; i++)
            {
                if (min[i] == max[i])
                {
                    commonHeads.Add(Text.WhiteSpace(headLength));
                }
                else
                {
                    uncommonHeads.AddRange(target.GetRange(i, min.Count - commonHeads.Count));
                    break;
                }
            }

            var paddledHeads = commonHeads.GetRange(0, commonHeads.Count > 0 ? commonHeads.Count - 1 : 0);

            // unpaddled heads are those add non-whitespace headEnd after heads
            var unpaddledHeads = new List<string>(uncommonHeads);

            if (commonHeads.Count > 0)
            {
                unpaddledHeads.Insert(0, commonHeads.Last());
            }

            var paddled   = Compose(paddledHeads  , headLength, headStart, "  "   , string.Empty, infoStart, infoEnd);
            var unpaddled = Compose(unpaddledHeads, headLength, headStart, headEnd, string.Empty, infoStart, infoEnd);
            
            // extra heads are always uncommon
            var extraHeads = new List<string>();

            if (comparison == LengthComparison.Longer)
            {
                // add heads in exceeding positions
                for (var i = min.Count; i < max.Count - 1; i++)
                {
                    extraHeads.Add(target[i]);
                }
                
                return paddled + unpaddled + Compose(extraHeads, headLength, headStart, headEnd, target.Last(), infoStart, infoEnd);
            }
            else
            {
                return paddled + unpaddled.TrimEnd(" >".ToCharArray());
            }
        }

        #endregion

        #region Format Loading

        public string ConfigurationFile
        {
            get
            {
                return "Format.txt";
            }
        }

        public static void Load(IConfigurationLoader loader)
        {
            LoadFormatLength(loader);
        }

        private static void LoadFormatLength(IConfigurationLoader loader)
        {
            var dict = ConfigurationLoader.LoadUniquePairs(loader);

            DefaultHeadLength = ConfigurationLoader.MultipleIntValue(dict, "HeadLength", 0, 10);
        }

        #endregion
    }
}