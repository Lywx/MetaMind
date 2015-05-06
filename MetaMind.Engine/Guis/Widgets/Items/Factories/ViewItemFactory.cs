namespace MetaMind.Engine.Guis.Widgets.Items.Factories
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Visuals;

    public sealed class ViewItemFactory : IViewItemFactory
    {
        public ViewItemFactory(
            Func<IViewItem, IViewItemLogic> logic,
            Func<IViewItem, IViewItemVisual> visual,
            Func<IViewItem, dynamic> data)
        {
            if (visual == null)
            {
                throw new ArgumentNullException("visual");
            }

            if (logic == null)
            {
                throw new ArgumentNullException("logic");
            }

            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.Visual = visual;
            this.Logic  = logic;
            this.Data   = data;
        }

        public Func<IViewItem, dynamic> Data { get; set; }

        public Func<IViewItem, IViewItemLogic> Logic { get; set; }

        public Func<IViewItem, IViewItemVisual> Visual { get; set; }

        public dynamic CreateData(IViewItem item)
        {
            return this.Data(item);
        }

        public IViewItemLogic CreateLogic(IViewItem item)
        {
            return this.Logic(item);
        }

        public IViewItemVisual CreateVisual(IViewItem item)
        {
            return this.Visual(item);
        }
    }
}