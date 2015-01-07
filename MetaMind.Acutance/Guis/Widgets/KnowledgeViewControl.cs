namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Linq;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class KnowledgeViewControl : GridControl
    {
        #region Constructors

        public KnowledgeViewControl(IView view, KnowledgeViewSettings viewSettings, KnowledgeItemSettings itemSettings, KnowledgeItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.AddFileItem();
            this.AddBlankItem();

            this.SearchControl = new KnowledgeViewSearchControl(view,viewSettings, itemSettings);
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

        public KnowledgeFileQuery FileBuffer { get; set; }
        
        public IViewItem FileItem
        {
            get { return View.Items.Count > 0 ? View.Items.Where(item => item.ItemData.IsFile).First() : null; }
        }

        #endregion

        #region Item Management

        public void AddBlankItem()
        {
            var blankEntry = new KnowledgeEntry(string.Empty, true) { IsBlank = true };
            this.AddItem(blankEntry);
        }

        public void AddFileItem()
        {
            var fileEntry = new KnowledgeEntry(string.Empty, true) { IsFile = true };
            this.InsertItem(0, fileEntry);
        }

        public void AddItem(KnowledgeEntry entry)
        {
            var item = new ViewItemExchangeless(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            View.Items.Add(item);
        }

        public void AddResultItem(string relativePath)
        {
            var resultEntry = new KnowledgeEntry(relativePath, true) { IsResult = true };
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
            var blankEntry = new KnowledgeEntry(string.Empty, true) { IsBlank = true };
            this.InsertItem(1, blankEntry);
        }

        public void InsertItem(int index, KnowledgeEntry entry)
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
                var moduleCreatedEvent = new EventBase(
                    (int)SessionEventType.ModuleCreated,
                    new ModuleCreatedEventArgs(FileBuffer.File));

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

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();

            if (this.AcceptInput)
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.KnowledgeLoadBuffer))
                    {
                        this.LoadBuffer();
                    }
                }
            }

            if (View.Items.Count > 0)
            {
                foreach (var item in View.Items.FindAll(item => item.ItemData.IsControl).ToArray())
                {
                    item.UpdateInput(gameTime);
                }
            }
        }

        protected override void UpdateViewFocus()
        {
            if (this.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                this.View.Disable(ViewState.View_Has_Focus);
            }
        }

        #endregion Update

        #region Configurations

        protected override Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X),
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}