namespace MetaMind.Engine.Guis.Widgets.Views.Selections
{
    public class IndexedBlockViewVerticalSelectionController : BlockViewVerticalSelectionController
    {
        public IndexedBlockViewVerticalSelectionController(IView view) : base(view)
        {
        }

        public override void MoveUp()
        {
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

            if (!this.CurrentSelectedId.HasValue)
            {
                this.Reverse();

                return;
            }

            var id = this.CurrentSelectedId.Value;

            //if (!this.IsTopmost(id))
            //{
            //    this.Select(this.UpperId(id));
            //}
        }

        public override void MoveDown()
        {
            // Items is empty
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

            if (!this.CurrentSelectedId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id = this.CurrentSelectedId.Value;

            // Last item is deleted
            if (id >= this.View.ItemsRead.Count)
            {
                return;
            }

            //if (!this.IsBottommost(id))
            //{
            //    this.Select(this.LowerId(id));
            //}
        }
    }
}
