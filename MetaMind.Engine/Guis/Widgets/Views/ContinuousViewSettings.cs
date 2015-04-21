namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class ContinuousViewSettings : ViewSettings, IParameterLoader<GraphicsSettings>
    {
        public Point PointStart;

        public ContinuousViewSettings()
        {
            
        }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.PointStart = new Point(160, parameter.Height / 2);
        }
    }
}