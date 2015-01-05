namespace MetaMind.Acutance.Parsers.Grammers
{
    using MetaMind.Acutance.Parsers.Elements;

    using Sprache;

    public static class TimeTagGrammer
    {
        public static Parser<TimeTag> TimeTagFullParser = 
            from hours   in Parse.CharExcept(':').Many().Text()
            from lcolon  in Parse.Char(':')
            from minutes in Parse.CharExcept(':').Many().Text() 
            from rcolon  in Parse.Char(':') 
            from seconds in Parse.CharExcept(':').Many().Text()
            select new TimeTag(hours, minutes, seconds);

        public static Parser<TimeTag> TimeTagConciseParser = 
            from minutes in Parse.CharExcept(':').Many().Text() 
            from rcolon  in Parse.Char(':') 
            from seconds in Parse.CharExcept(':').Many().Text()
            select new TimeTag("00", minutes, seconds);

        public static Parser<Parser<TimeTag>> TimeTagStrategyParser =
            Parse.Regex(@"(\d+:\d+:\d+)", "Hours:Minutes:Seconds").Return(TimeTagFullParser)
                .Or(Parse.Regex(@"(\d+:\d+)", "Minutes:Seconds").Return(TimeTagConciseParser));
    }
}
