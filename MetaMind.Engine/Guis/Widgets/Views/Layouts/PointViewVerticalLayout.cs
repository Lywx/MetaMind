namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    public class PointViewVerticalLayout : ViewLayout, IPointViewVerticalLayout
    {
        public PointViewVerticalLayout(IView view)
            : base(view)
        {
        }

        public int RowNum { get { return this.ItemsRead.Count; } }

        public int RowOf(int id)
        {
            return id;
        }

        public int RowIn(int id)
        {
            return 1;
        }
    }
}