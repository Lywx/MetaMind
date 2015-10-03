namespace MetaMind.Engine.Gui.Controls.Views.Layouts
{
    public class PointViewVerticalLayout : ViewLayout, IPointViewVerticalLayout
    {
        public PointViewVerticalLayout(IMMViewNode view)
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