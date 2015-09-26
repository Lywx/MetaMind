namespace MetaMind.Engine.Gui.Elements.Input
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