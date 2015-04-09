using System;

namespace MetaMind.Acutance.Concepts
{
    /// <remarks>
    /// Tag of schedule should not be in side 13:55, 28:55, 43:55, 58:55 
    /// which will collide with save manager save listener in MultiplexerGroup.
    /// </remarks>
    public class Schedule : Command
    {
        public Schedule(string name, string path, int offset, DateTime date, CommandRepetion repetion)
            : base(name, path, offset, date, repetion)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }
    }
}
