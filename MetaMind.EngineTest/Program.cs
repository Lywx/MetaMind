namespace MetaMind.EngineTest
{
    using System;

    using LightInject;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.ItemFrames;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.ContinousView;
    using MetaMind.Runtime.Guis.Widgets;

#if WINDOWS || LINUX
    public static class Program
    {
        internal static ServiceContainer Container = new ServiceContainer();

        [STAThread]
        static void Main()
        {
            Container.Register<ContinuousView1D>();
            Container.Register<ContinuousView1DSettings>();

            Container.Register<ViewFactory>();
            Container.Register<ViewItemDataModifier>();
            Container.Register<ViewItemFrameControl>();
            Container.Register<ViewItemVisual>();
            Container.Register<ViewItemExchangable>();
            Container.Register<ExperienceItemLogic>();

            using (var engine = new GameEngine())
            {
                var test = new Test(engine);
                test.Run();
            }
        }
    }
#endif
}
