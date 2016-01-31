namespace MetaMind.Engine.Core.Entity.Input
{
    using System;

    public class MMMouseState
    {
        public DateTime TimeStamp { get; set; }

        public MMMouseState()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}