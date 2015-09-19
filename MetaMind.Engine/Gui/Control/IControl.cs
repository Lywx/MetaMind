namespace MetaMind.Engine.Gui.Control
{
    public interface IControl : IRenderTarget, IComponent
    {
        #region 

        int Width { get; set; }

        int Height { get; set; }

        int X { get; set; }

        int Y { get; set; }

        #endregion
    }
}