namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    using Layers;
    using Settings;

    public class PointView2DLayout : ViewLayout, IPointView2DLayout
    {
        private PointView2DSettings viewSettings;

        public PointView2DLayout(IView view)
            : base(view)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.viewSettings = this.ViewGetLayer<PointView2DLayer>().ViewSettings;
        }

        public int ColumnNum
        {
            get
            {
                return this.RowNum > 1 ? this.viewSettings.ColumnNumMax : this.View.ItemsRead.Count;
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
            return id % this.viewSettings.ColumnNumMax;
        }

        public int IdFrom(int i, int j)
        {
            return i * this.viewSettings.ColumnNumMax + j;
        }

        public int RowOf(int id)
        {
            for (var row = 0; row < this.viewSettings.RowNumMax; row++)
            {
                if (id - row * this.viewSettings.ColumnNumMax >= 0)
                {
                    continue;
                }

                return row - 1;
            }

            return this.viewSettings.RowNumMax - 1;
        }

        public int RowIn(int id)
        {
            return 1;
        }
    }
}