namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Linq;

    using FileSearcher;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class KnowledgeViewLogicControl : PointGridLogicControl
    {
        #region Constructors

        public KnowledgeViewLogicControl(IView view, KnowledgeViewSettings viewSettings, KnowledgeItemSettings itemSettings, KnowledgeItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.AddFileItem();
            this.AddBlankItem();

            this.SearchControl = new KnowledgeViewSearchControl(view,viewSettings, itemSettings);

            this.View[ViewState.View_Has_Focus] = this.Region[RegionState.Region_Has_Focus];
        }


        #endregion Constructors

        #region Private Properties

        private KnowledgeViewSearchControl SearchControl { get; set; }

        #endregion

        #region Public Properties

        public IViewItem BlankItem
        {
            get { return View.Items.Count > 0 ? View.Items.Where(item => item.ItemData.IsBlank).First() : null; }
        }

        public RawKnowledgeFileBuffer FileBuffer { get; set; }
        
        public IViewItem FileItem
        {
            get { return View.Items.Count > 0 ? View.Items.Where(item => item.ItemData.IsFile).First() : null; }
        }

        public Searcher Searcher
        {
            get { return this.SearchControl.Searcher; }
        }

        #endregion

        #region Item Management

        public void AddBlankItem()
        {
            var blankEntry = new Knowledge(string.Empty, true) { IsBlank = true };
            this.AddItem(blankEntry);
        }

        public void AddFileItem()
        {
            var fileEntry = new Knowledge(string.Empty, true) { IsFile = true };
            this.InsertItem(0, fileEntry);
        }

        public void AddItem(Knowledge entry)
        {
            var item = new ViewItemExchangeless(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            View.Items.Add(item);
        }

        public void AddResultItem(string relativePath)
        {
            var resultEntry = new Knowledge(relativePath, true) { IsResult = true };
            this.AddItem(resultEntry);
        }

        public void ClearNonControlItems()
        {
            foreach (var item in this.View.Items.FindAll(item => !item.ItemData.IsControl))
            {
                View.Items.Remove(item);
                item.Dispose();
            }
        }

        public void ClearResultItems()
        {
            foreach (var item in this.View.Items.FindAll(item => item.ItemData.IsResult))
            {
                View.Items.Remove(item);
                item.Dispose();
            }
        }

        public void InsertBlankItem()
        {
            var blankEntry = new Knowledge(string.Empty, true) { IsBlank = true };
            this.InsertItem(1, blankEntry);
        }

        public void InsertItem(int index, Knowledge entry)
        {
            var item = new ViewItemExchangeless(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.Items.Insert(index, item);
        }

        public void RemoveBlankItem()
        {
            if (this.BlankItem != null)
            {
                View.Items.Remove(this.BlankItem);
            }
        }

        #endregion

        #region Operations

        public void LoadBuffer()
        {
            if (this.FileBuffer != null)
            {
                var moduleCreatedEvent = new Event(
                    (int)SessionEventType.ModuleCreated,
                    new ModuleCreatedEventArgs(FileBuffer));

                EventManager.QueueEvent(moduleCreatedEvent);
            }
        }

        public void LoadResult(string path, bool relative, int offset)
        {
            this.SearchControl.LoadResult(path, relative, offset);
        }

        public void Search(string fileName)
        {
            this.SearchControl.Search(fileName);
        }

        public void SearchStop()
        {
            this.SearchControl.SearchStop();
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);

            if (this.AcceptInput)
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.KnowledgeLoadBuffer))
                    {
                        this.LoadBuffer();
                    }
                }
            }

            if (View.Items.Count > 0)
            {
                foreach (var item in View.Items.FindAll(item => item.ItemData.IsControl && !item.ItemData.IsBlank).ToArray())
                {
                    item.UpdateInput(time);
                }
            }
        }

        #endregion Update

        #region Configurations

        protected override Rectangle RegionBounds()
        {
            return new Rectangle(
                viewSettings.PointStart.X,
                viewSettings.PointStart.Y,
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X),
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}