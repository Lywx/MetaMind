namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    public sealed class ViewItemFactory : IViewItemFactory
    {
        public ViewItemFactory(Func<IViewItem, dynamic> logic, Func<IViewItem, dynamic> visual, Func<IViewItem, dynamic> data)
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

        public Func<IViewItem, dynamic> Logic { get; set; }

        public Func<IViewItem, dynamic> Visual { get; set; }

        public dynamic CreateData(IViewItem item)
        {
            return this.Data(item);
        }

        public dynamic CreateLogicControl(IViewItem item)
        {
            return this.Logic(item);
        }

        public IItemVisualControl CreateVisualControl(IViewItem item)
        {
            return this.Visual(item);
        }
    }
}