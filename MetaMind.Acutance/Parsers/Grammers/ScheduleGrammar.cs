namespace MetaMind.Acutance.Parsers.Grammers
{
    using MetaMind.Acutance.Parsers.Elements;

    using Sprache;

    public static class ScheduleGrammar
    {
        public static Parser<RepeativityTag> RepeativityParser =
            Parse.String("Everyday")  .End().Return(RepeativityTag.EveryDay)
        .Or(Parse.String("EveryWeek") .End().Return(RepeativityTag.EveryWeek))
        .Or(Parse.String("EveryMonth").End().Return(RepeativityTag.EveryMonth))
        .Or(Parse.String("-")               .Return(RepeativityTag.Unspecified));

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

        public static Parser<string> ScheduleUnitParser = 
            Parse.Regex(@"^(?![\[\n])(.)*((.*)?(\n)?(?!\[))*", "Between two [DateTag]");

        public static Parser<string> CommandUnitParser = 
            Parse.Regex(@"Command:((.*)\n?[^\n])*", "Command: ... until an empty line");
    }
}
