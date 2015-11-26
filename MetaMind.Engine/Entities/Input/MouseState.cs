namespace MetaMind.Engine.Entities.Input
{
    using System;

    public class MouseState
    {
        public DateTime TimeStamp { get; set; }

        public MouseState()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}