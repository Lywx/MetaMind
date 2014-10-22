using System.Collections.Generic;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewSwapControl
    {
        IEnumerable<IView> Observors { get; }

        void AddObserver( IView view );

        float Progress { get; set; }

        void Initialize( Point origin, Point target );

        void ObserveSwapFrom( IViewItem draggingItem, IView view );

        Point RootCenterPoint();
    }
}