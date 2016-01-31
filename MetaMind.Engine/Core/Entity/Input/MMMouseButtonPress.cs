namespace MetaMind.Engine.Core.Entity.Input
{
    public class MMMouseButtonPress : MMMouseButtonState
    {
        public MMMouseButtonPress(bool isMouseOver)
        {
            this.IsMouseOver = isMouseOver;
        }

        public bool IsMouseOver { get; private set; }
    }
}