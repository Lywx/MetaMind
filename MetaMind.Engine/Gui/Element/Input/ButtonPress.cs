namespace MetaMind.Engine.Gui.Element.Input
{
    public class ButtonPress : ButtonState
    {
        public ButtonPress(bool isMouseOver)
        {
            this.IsMouseOver = isMouseOver;
        }

        public bool IsMouseOver { get; private set; }
    }
}