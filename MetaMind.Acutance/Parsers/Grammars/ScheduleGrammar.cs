namespace MetaMind.Acutance.Parsers.Grammars
{
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine.Parsers.Grammars;

    using Sprache;

    public static class ScheduleGrammar
    {
        public static Parser<RepetitionTag> RepeativityParser =
            Parse.String("Everyday")  .End().Return(RepetitionTag.EveryDay)
        .Or(Parse.String("EveryWeek") .End().Return(RepetitionTag.EveryWeek))

        // TODO: Disabled for safety issue
        // .Or(Parse.String("EveryMonth").End().Return(RepetitionTag.EveryMonth))
        .Or(Parse.String("-")               .Return(RepetitionTag.Unspecified));

        public static Parser<DayTag> DayParser =
            Parse.String("Mon").      End().Return(DayTag.Monday)
        .Or(Parse.String("Monday").   End().Return(DayTag.Monday))
        .Or(Parse.String("Tue").      End().Return(DayTag.Tuesday))
        .Or(Parse.String("Tuesday").  End().Return(DayTag.Tuesday))
        .Or(Parse.String("Wed").      End().Return(DayTag.Wednesday))
        .Or(Parse.String("Wednesday").End().Return(DayTag.Wednesday))
        .Or(Parse.String("Thu").      End().Return(DayTag.Thursday))
        .Or(Parse.String("Thursday"). End().Return(DayTag.Thursday))
        .Or(Parse.String("Fri").      End().Return(DayTag.Friday))
        .Or(Parse.String("Friday").   End().Return(DayTag.Friday))
        .Or(Parse.String("Sat").      End().Return(DayTag.Saturday))
        .Or(Parse.String("Saturday"). End().Return(DayTag.Saturday))
        .Or(Parse.String("Sun").      End().Return(DayTag.Sunday))
        .Or(Parse.String("Sunday").   End().Return(DayTag.Sunday))
        .Or(Parse.String("-").              Return(DayTag.Unspecified));

        public static ScheduleTag DateTag(string input)
        {
            var elems = input.Split(' ');

            var repeativity = RepeativityParser                 .Parse(elems[0]);
            var day         = DayParser                         .Parse(elems[1]);
            var time        = KnowledgeGrammar.TimeTagFullParser.Parse(elems[2]);

            return new ScheduleTag(repeativity, day, time);
        }

        public static Parser<string> ScheduleUnitParser = 
            Parse.Regex(@"^(?![\[\n])(.)*((.*)?(\n)?(?!\[))*", "Between two [DateTag]");

        public static Parser<string> CommandUnitParser = 
            
            // this regex is very sensitive to Window \r\n newline expression
            from regex in Parse.Regex(@"Command:((.*)(\r)?(\n)*)!?([\r\n:])*", "Command: ... until an empty line")
            from whitespaces in Parse.WhiteSpace.Many().Optional()
            select regex.Trim();

        public static Parser<RawSchedule> ScheduleParser = from text   in LineGrammar.BracketedTextParser
                                                        from spaces in Parse.WhiteSpace.Many().Optional()
                                                        from unit   in ScheduleUnitParser
                                                        select new RawSchedule(DateTag(text), unit);

        public static Parser<RawScheduleFile> ScheduleFileParser = from schedules in ScheduleParser.AtLeastOnce()
                                                                select new RawScheduleFile(schedules.ToList());
    }
}