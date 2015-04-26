﻿namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewScrollControlVertical : IPointViewScrollControl
    {
        int YOffset { get; }

        bool IsDownToDisplay(int row);

        bool IsUpToDisplay(int row);

        void MoveDown();

        void MoveUp();

        void MoveUpToTop();
    }
}