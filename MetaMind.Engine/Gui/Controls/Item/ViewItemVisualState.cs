// --------------------------------------------------------------------------------------------------------------------
// <copyright file="" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Controls.Item
{
    public struct ViewItemVisualState<T>
    {
        public T MouseOut;

        public T MouseOver;

        public T Selected;

        public T Pending;

        public T Editing;
    }
}