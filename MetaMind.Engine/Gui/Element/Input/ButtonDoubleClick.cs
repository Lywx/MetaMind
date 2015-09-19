namespace MetaMind.Engine.Gui.Element.Input
{
    public class ButtonDoubleClick : ButtonState
    {
        public ButtonDoubleClick(bool isMouseOver)
        {
            this.IsMouseOver = isMouseOver;
        }

        public bool IsMouseOver { get; private set; }
    }
}