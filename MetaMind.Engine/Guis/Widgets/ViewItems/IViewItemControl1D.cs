using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemControl1D
    {
        #region Components

        IItemRootFrame RootFrame { get; }

        #endregion Components

        #region Data

        int Id { get; set; }

        #endregion Data

        #region Update

        void Update( GameTime gameTime );

        #endregion Update

        #region Selection

        void SelectIt();

        void UnSelectIt();

        #endregion Selection

        #region Swapping

        void SwapIt( IViewItem item );

        #endregion Swap
    }
}