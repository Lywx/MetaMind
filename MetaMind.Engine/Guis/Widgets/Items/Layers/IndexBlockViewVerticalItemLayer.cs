namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using Layouts;
    using Logic;

    public class IndexBlockViewVerticalItemLayer : BlockViewVerticalItemLayer
    {
        public IndexBlockViewVerticalItemLayer(IViewItem item) : base(item)
        {
        }

        public new IIndexBlockViewVerticalItemLogic ItemLogic
        {
            get { return (IIndexBlockViewVerticalItemLogic)base.ItemLogic; }
        }

        public new IIndexBlockViewVerticalItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }
    }
}