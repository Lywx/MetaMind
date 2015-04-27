namespace MetaMind.Engine.Guis.Elements.Inputs
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