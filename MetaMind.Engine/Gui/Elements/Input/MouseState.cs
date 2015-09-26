namespace MetaMind.Engine.Gui.Elements.Input
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