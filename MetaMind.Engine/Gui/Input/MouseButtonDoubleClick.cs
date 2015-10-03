namespace MetaMind.Engine.Gui.Input
{
    public class MouseButtonDoubleClick : MouseButtonState
    {
        public MouseButtonDoubleClick(bool isMouseOver)
        {
            this.IsMouseOver = isMouseOver;
        }

        public bool IsMouseOver { get; private set; }
    }
}