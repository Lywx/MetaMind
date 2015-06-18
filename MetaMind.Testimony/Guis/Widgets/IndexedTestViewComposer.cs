namespace MetaMind.Testimony.Guis.Widgets
{
    using System;
    using Concepts.Tests;
    using Engine.Guis.Widgets.Regions;
    using Engine.Guis.Widgets.Views;
    using Engine.Guis.Widgets.Views.Logic;
    using Engine.Guis.Widgets.Views.Selections;

    public class IndexedTestViewComposer : TestViewComposer
    {
        public IndexedTestViewComposer(TestSession testSeesion)
            : base(testSeesion)
        {
        }

        public override void Compose(IView view, dynamic viewData)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            if (viewData == null)
            {
                throw new ArgumentNullException("viewData");
            }

            this.View = view;
            this.ViewData = viewData;

            this.AddView();
            this.AddViewRegion();
            this.SetupLogic();
        }

        protected override BlockViewVerticalSelectionController AddViewSelection()
        {
            return new IndexedBlockViewVerticalSelectionController(this.View);
        }

        protected override IViewLogic AddViewLogic()
        {
            return new IndexedTestViewLogic(
                this.View,
                this.ViewScroll,
                this.ViewSelection,
                this.ViewSwap,
                this.ViewLayout,
                this.ItemFactory);
        }

        protected override void SetupLogic()
        {
            this.View[ViewState.View_Has_Focus] =
                () => this.View[ViewState.View_Has_Selection]() ||
                      this.ViewRegion[RegionState.Region_Has_Focus]();
        }
    }
}