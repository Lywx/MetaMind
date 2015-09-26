namespace MetaMind.Engine.Gui.Components
{
    public interface IRenderComponent : IRenderTarget, IComponent
    {
        #region 

        int Width { get; set; }

        int Height { get; set; }

        int X { get; set; }

        int Y { get; set; }

        #endregion
    }
}