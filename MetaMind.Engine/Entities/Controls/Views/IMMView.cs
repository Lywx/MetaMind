namespace MetaMind.Engine.Entities.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Item;
    using Item.Settings;
    using Layers;
    using Logic;
    using Settings;
    using Visuals;

    public interface IMMViewOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IMMViewLayer;

        #endregion
    }

    /// <summary>
    /// IView define the basic framework for a View object. It allows extending 
    /// component contracts by casting.
    /// </summary>
    public interface IMMView : IMMViewOperations, IMMViewComponentOperations, IMMBufferable
    {
        #region States

        Func<bool> this[MMViewState state] { get; set; }

        #endregion

        #region View Data

        IMMViewController ViewController { get; set; }

        IMMViewRenderer Renderer { get; set; }

        IMMViewLayer ViewLayer { get; set; }

        Dictionary<string, object> ViewComponents { get; }

        ViewSettings ViewSettings { get; set; }

        #endregion

        List<IMMViewItem> Items { get; }

        ItemSettings ItemSettings { get; set; }
    }

    public interface IMMViewInternal
    {
        #region Item Data

        List<IMMViewItem> ItemsRead { get; }

        /// <summary>
        /// Items collection that is used to write to next frame.
        /// </summary>
        /// <remarks>
        /// This collection should avoid being written twice in one frame, 
        /// because of the possible operation collision using the ItemsRead data.
        /// </remarks>
        List<IMMViewItem> ItemsWrite { get; set; }

        #endregion
    }
}