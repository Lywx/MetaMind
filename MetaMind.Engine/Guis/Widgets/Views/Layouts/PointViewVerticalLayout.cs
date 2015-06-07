namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    public class PointViewVerticalLayout : ViewLayout, IPointViewVerticalLayout
    {
        public PointViewVerticalLayout(IView view)
            : base(view)
        {
        }

        public virtual int RowNum { get { return this.ItemsRead.Count; } }

        public virtual int RowOf(int id)
        {
            return id;
        }

        public virtual int RowIn(int id)
        {
            return 1;
        }
    }
}