using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.EngineTest.Guis
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    using Microsoft.Xna.Framework;

    using NUnit.Framework;

    [TestFixture]
    public class Test_View
    {
        [Test]
        public void Test_PointViewHorizontalCreation()
        {
            var view = new View(
                new PointViewHorizontalSettings(new Point(50, 50)),
                new PointViewHorizontalFactory(),
                new ItemSettings(),
                new List<IViewItem>());
        }

        [Test]
        public void Test_PointView2DCreation()
        {
            var view = new View(
                new PointView2DSettings(new Point(50, 50)), 
                new PointView2DFactory(), 
                new ItemSettings(),
                new List<IViewItem>());
        }
    }
}
