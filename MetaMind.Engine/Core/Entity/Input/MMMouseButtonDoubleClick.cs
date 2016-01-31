namespace MetaMind.Engine.Core.Entity.Input
{
    public class MMMouseButtonDoubleClick : MMMouseButtonState
    {
        public MMMouseButtonDoubleClick(bool isMouseOver)
        {
            this.IsMouseOver = isMouseOver;
        }

        public bool IsMouseOver { get; private set; }
    }
}