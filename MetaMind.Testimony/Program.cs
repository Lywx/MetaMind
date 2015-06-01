// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony
{
    using System;

    using LightInject;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

#if WINDOWS || LINUX

    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            var container = new ServiceContainer();

            container.Register<IView, View>();

            container.Register<IViewLogic, PointView2DLogic<object>>();
            container.Register<IViewVisual, ViewVisual>();
            container.Register<IViewLayer, PointView2DLayer>();

            container.Register<IViewScrollController, PointView2DScrollController>();
            container.Register<IViewSelectionController, PointView2DSelectionController>();
            container.Register<IViewSwapController, ViewSwapController<object>>();
            container.Register<IViewLayout, PointView2DLayout>();

            container.Register<IViewItemLayout, ViewItemLayout>();

            var s = container.GetInstance<IView>();

            using (var engine = new GameEngine())
            {
                var game = new Testimony(engine);
                game.Run();
            }
        }
    }
#endif
}