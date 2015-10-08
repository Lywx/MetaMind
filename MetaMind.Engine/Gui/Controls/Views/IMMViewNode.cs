namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Item;
    using Item.Settings;
    using Layers;
    using Logic;
    using Settings;
    using Visuals;

    public interface IMMViewNodeOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IViewLayer;

        #endregion
    }

    /// <summary>
    /// IView define the basic framework for a View object. It allows extending 
    /// component contracts by casting.
    /// </summary>
    public interface IMMViewNode : IMMViewNodeOperations, IMMViewComponentOperations, IMMBufferable
    {
        #region States

        Func<bool> this[MMViewState state] { get; set; }

        #endregion
        #region View Data

        IMMViewController ViewController { get; set; }

        IMMViewNodeVisual ViewVisual { get; set; }

        IViewLayer ViewLayer { get; set; }

        Dictionary<string, object> ViewComponents { get; }

        ViewSettings ViewSettings { get; set; }

        #endregion

        List<IViewItem> Items { get; }

        ItemSettings ItemSettings { get; set; }
    }

    public interface IMMViewNodeInternal
    {
        #region Item Data

        List<IViewItem> ItemsRead { get; }

        /// <summary>
        /// Items collection that is used to write to next frame.
        /// </summary>
        /// <remarks>
        /// This collection should avoid being written twice in one frame, 
        /// because of the possible operation collision using the ItemsRead data.
        /// </remarks>
        List<IViewItem> ItemsWrite { get; set; }

        #endregion
    }
}