namespace MetaMind.Engine.Guis.Elements.Inputs
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