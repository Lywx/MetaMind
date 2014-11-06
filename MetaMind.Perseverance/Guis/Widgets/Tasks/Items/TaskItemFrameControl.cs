using System.Threading.Tasks;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemFrameControl : ViewItemFrameControl
    {
        public ItemRootFrame NameFrame { get; private set; }

        public ItemEntryFrame IdFrame { get; private set; }

        public ItemEntryFrame ExperienceFrame { get; set; }

        public TaskItemFrameControl( IViewItem item )
            : base( item )
        {
            NameFrame       = new ItemRootFrame( item );
            IdFrame         = new ItemEntryFrame( item );
            ExperienceFrame = new ItemEntryFrame( item );
        }

        protected override void UpdateFrames( GameTime gameTime )
        {
            base.UpdateFrames( gameTime );

            NameFrame      .Center   = NameFrameCenter;
            IdFrame        .Location = IdFrameLocation;
            ExperienceFrame.Location = ExperienceFrameLocation;

            ExperienceFrame.Size = ItemSettings.ExperienceFrameSize;
            NameFrame      .Size = ItemSettings.NameFrameSize;
            IdFrame        .Size = ItemSettings.IdFrameSize;

            NameFrame      .Update( gameTime );
            IdFrame        .Update( gameTime );
            ExperienceFrame.Update( gameTime );
        }

        private Point IdFrameLocation
        {
            get
            {
                return ( ( ( Point ) ItemControl.RootFrame.Center ).ToVector2() + new Vector2( -ItemSettings.NameFrameSize.X / 2, -ItemSettings.NameFrameSize.Y / 2 - ItemSettings.IdFrameSize.Y ) ).ToPoint();
            }
        }

        private Point NameFrameCenter
        {
            get { return ItemControl.RootFrame.Center; }
        }

        public Point ExperienceFrameLocation
        {
            get
            {
                return ( ( ( Point ) ItemControl.RootFrame.Center ).ToVector2() + new Vector2( 0, -ItemSettings.NameFrameSize.Y / 2 - ItemSettings.ExperienceFrameSize.Y ) ).ToPoint();
                
            }
        }
    }
}