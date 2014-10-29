using System;
using C3.Primtive2DXna;
using MetaMind.Engine.Guis.Elements.Regions;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewRegion : Region, IViewComponent
    {
        public TaskViewRegion( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( StartRectangle( viewSettings, itemSettings ) )
        {
            View = view;
            ViewSettings = viewSettings;
            ItemSettings = itemSettings;
        }

        public dynamic ItemSettings { get; private set; }

        public IView View { get; private set; }

        public dynamic ViewControl { get { return View.Control; } }

        public dynamic ViewSettings { get; private set; }

        public void Draw( GameTime gameTime )
        {
            Primitives2D.DrawRectangle( ScreenManager.SpriteBatch, Frame.Rectangle, Color.White );
        }

        private static Rectangle StartRectangle( dynamic viewSettings, dynamic itemSettings )
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * itemSettings.NameFrameSize.X + viewSettings.ScrollBarSettings.Width,
                viewSettings.RowNumDisplay * itemSettings.NameFrameSize.Y
                );
        }
    }
}