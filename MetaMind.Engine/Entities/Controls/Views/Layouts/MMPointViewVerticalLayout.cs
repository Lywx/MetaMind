namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    public class MMPointViewVerticalLayout : MMViewLayout, IMMPointViewVerticalLayout
    {
        public MMPointViewVerticalLayout(IMMView view)
            : base(view)
        {
        }

        public virtual int RowNum { get { return this.Items.Count; } }

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