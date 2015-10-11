namespace MetaMind.Engine.Entities.Input
{
    public class MouseButtonPress : MouseButtonState
    {
        public MouseButtonPress(bool isMouseOver)
        {
            this.IsMouseOver = isMouseOver;
        }

        public bool IsMouseOver { get; private set; }
    }
}