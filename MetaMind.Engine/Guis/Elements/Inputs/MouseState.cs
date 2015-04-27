namespace MetaMind.Engine.Guis.Elements.Inputs
{
    using System;

    public class MouseState
    {
        public DateTime When { get; set; }

        public MouseState()
        {
            this.When = DateTime.Now;
        }
    }
}