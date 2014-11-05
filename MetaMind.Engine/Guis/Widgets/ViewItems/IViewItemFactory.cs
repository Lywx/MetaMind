﻿using MetaMind.Engine.Guis.Widgets.Items;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemFactory
    {
        dynamic CreateControl( IViewItem item );

        dynamic CreateData( IViewItem item );

        IItemGraphics CreateGraphics( IViewItem item );
    }
}