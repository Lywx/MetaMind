namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    using Layers;
    using Settings;

    public class MMPointView2DLayout : ViewLayout, IMMPointView2DLayout
    {
        private PointView2DSettings viewSettings;

        public MMPointView2DLayout(IMMView view)
            : base(view)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.viewSettings = this.GetViewLayer<MMPointView2DLayer>().ViewSettings;
        }

        public int ColumnNum
        {
            get
            {
                return this.RowNum > 1 ? this.viewSettings.ViewColumnMax : this.View.ItemsRead.Count;
            }
        }

        public int RowNum
        {
            get
            {
                var lastId = this.View.ItemsRead.Count - 1;
                return this.RowOf(lastId) + 1;
            }
        }

        public int ColumnOf(int id)
        {
            return id % this.viewSettings.ViewColumnMax;
        }

        public int IdFrom(int i, int j)
        {
            return i * this.viewSettings.ViewColumnMax + j;
        }

        public int RowOf(int id)
        {
            for (var row = 0; row < this.viewSettings.ViewRowMax; row++)
            {
                if (id - row * this.viewSettings.ViewColumnMax >= 0)
                {
                    continue;
                }

                return row - 1;
            }

            return this.viewSettings.ViewRowMax - 1;
        }

        public int RowIn(int id)
        {
            return 1;
        }
    }
}