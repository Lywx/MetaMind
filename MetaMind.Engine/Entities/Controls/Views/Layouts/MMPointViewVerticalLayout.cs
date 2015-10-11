namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    public class MMPointViewVerticalLayout : ViewLayout, IMMPointViewVerticalLayout
    {
        public MMPointViewVerticalLayout(IMMView view)
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