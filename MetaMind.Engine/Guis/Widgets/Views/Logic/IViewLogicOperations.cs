﻿namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    public interface IViewLogicOperations
    {
        #region Binding

        void LoadBinding();

        void UnloadBinding();

        #endregion

        #region Control

        void AddItem();

        void AddItem(dynamic data);

        void ResetItems();

        #endregion
    }
}