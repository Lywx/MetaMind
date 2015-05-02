namespace MetaMind.EngineTest.Guis
{
    using System.Collections.Generic;

    using LightInject;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class Test_ViewFactory
    {
        private ServiceContainer container;

        [SetUp]
        public void Test_Setup()
        {
            this.container = new ServiceContainer();
            this.container.Register<ViewSettings>();
            this.container.Register<ItemSettings>();
        }

        [Test]
        public void Test_Contruction()
        {
            var mockViewVisual = new Mock<ViewVisual>();
            var mockViewLogic  = new Mock<PointView1DLogic>();

            var view = new View(
                new ViewSettings(),
                new ViewFactory((v, vs, ist) => mockViewLogic.Object, (v, vs, ist) => mockViewVisual.Object),
                new ItemSettings(),
                new List<IViewItem>());

            //var viewVisual = new Func<IView, object, object, ViewVisual>((view, viewSettings, ItemSettings) => new ViewVisual(view));

            //var viewLogic  = new Func<IView, ViewSettings, ItemSettings, PointView2DLogic>((view, viewSettings, itemSettings) =>
            //                                                                               new PointView2DLogic(
            //                                                                                   view,
            //                                                                                   viewSettings,
            //                                                                                   itemSettings,
            //                                                                                   new ViewItemFactory(
            //                                                                                   item =>
            //                                                                                   new PointView2DItemLogic(
            //                                                                                       )));

            //var pointView1DFactory =
            //    new ViewFactory(
            //        ,
            //        viewVisual);
        }
    }
}