namespace MetaMind.Acutance.Parsers.Elements
{
    public class Schedule
    {
        public readonly DateTag Date;

        public readonly string Content;

        public Schedule(DateTag date, string content)
        {
            this.Date = date;
            this.Content = content;
        }
    }
}