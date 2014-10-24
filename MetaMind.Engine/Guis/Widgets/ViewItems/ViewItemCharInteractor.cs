using MetaMind.Engine.Components;
using MetaMind.Engine.Components.Inputs;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public abstract class ViewItemCharInteractor : ViewItemComponent
    {
        protected ViewItemCharInteractor( IViewItem item )
            : base( item )
        {
        }

        #region Events

        protected abstract void DetectEnterKeyDown( object sender, KeyEventArgs e );

        protected abstract void DetectEscapeKeyDown( object sender, KeyEventArgs e );

        #endregion Events

        #region Operations

        public abstract void Cancel();

        public abstract void Release();

        #endregion Operations

        #region Update and Draw

        public abstract void Draw( GameTime gameTime );

        public abstract void Update( GameTime gameTime );

        #endregion Update and Draw
    }
}