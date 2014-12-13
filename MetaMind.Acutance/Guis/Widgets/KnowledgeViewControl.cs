namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using CodeProject.FileSearcher;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.ViewItems;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class KnowledgeViewControl : ViewControl2D
    {
        private readonly int maxDirectoryNum = 10;

        private readonly int maxLineNum = 200;

        private readonly int maxResultNum = 30;

        #region Constructors

        public KnowledgeViewControl(IView view, KnowledgeViewSettings viewSettings, KnowledgeItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Region      = new ViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
            this.ScrollBar   = new ViewScrollBar(view, viewSettings, itemSettings, viewSettings.ScrollBarSettings);
            this.ItemFactory = new KnowledgeItemFactory();

            this.AddFileItem();
            this.AddBlankItem();

            Searcher.FoundInfo += this.RefreshSearchResult;
        }

        #endregion Constructors

        #region Public Properties

        public KnowledgeItemFactory ItemFactory { get; protected set; }

        public ViewRegion Region { get; protected set; }

        public ViewScrollBar ScrollBar { get; protected set; }

        #endregion Public Properties

        #region Private Properties

        private IViewItem BlankItem
        {
            get { return View.Items.Count > 0 ? View.Items.First(item => item.ItemData.IsBlank) : null; }
        }

        private IViewItem FileItem
        {
            get { return View.Items.Count > 0 ? View.Items.First(item => item.ItemData.IsFile) : null; }
        }

        #endregion Private Properties

        #region Operations

        public void LoadResult(string relativePath)
        {
            this.FileItem.ItemControl.SetName(Path.GetFileName(relativePath));

            this.ClearNonControlItems();
            this.ClearResultItems();

            try
            {
                var path      = Path.Combine(FolderManager.DataFolderPath, relativePath);
                var attribute = File.GetAttributes(path);

                var isFile = (attribute & FileAttributes.Directory) != FileAttributes.Directory;
                if (isFile)
                {
                    this.LoadResultFromFile(path);
                    this.Scroll.MoveUpToTop();
                }
                else
                {
                    this.LoadResultFromDirectory(path);
                    this.Scroll.MoveUpToTop();
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        public void SearchStop()
        {
            this.FileItem.ItemControl.EditCancel();

            Searcher.Stop();
        }

        public void Search(string fileName)
        {
            if (FileItem == null)
            {
                return;
            }

            this.ClearNonControlItems();
            this.ClearResultItems();

            var fileNames = new List<string>(1) { "*" + fileName + "*" };

            Searcher.Start(
                new SearcherParams(
                    FolderManager.DataFolderPath,
                    true,
                    fileNames,
                    false,
                    DateTime.MinValue,
                    false,
                    DateTime.MinValue,
                    false,
                    string.Empty,
                    Encoding.Unicode));
        }

        private void AddBlankItem()
        {
            var blankItem = new KnowledgeEntry { Name = string.Empty, IsControl = true, IsBlank = true };
            this.AddItem(blankItem);
        }

        private void AddFileItem()
        {
            var fileItem = new KnowledgeEntry { Name = string.Empty, IsControl = true, IsFile = true };
            this.InsertItem(0, fileItem);
        }

        private void AddItem(KnowledgeEntry entry)
        {
            var item = new ViewItemExchangeless(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            View.Items.Add(item);
        }

        private void ClearNonControlItems()
        {
            var nonControl = View.Items.FindAll(item => !item.ItemData.IsControl);
            nonControl.ForEach(item => View.Items.Remove(item));
        }

        private void ClearResultItems()
        {
            var result = View.Items.FindAll(item => item.ItemData.IsSearchResult);
            result.ForEach(item => View.Items.Remove(item));
        }

        private void InsertBlankItem()
        {
            var blankItem = new KnowledgeEntry { Name = string.Empty, IsControl = true, IsBlank = true };
            this.InsertItem(1, blankItem);
        }

        private void InsertItem(int index, KnowledgeEntry entry)
        {
            var item = new ViewItemExchangeless(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.Items.Insert(index, item);
        }

        private void LoadResultFromDirectory(string path)
        {
            foreach (var dir in Directory.GetDirectories(path).Take(this.maxDirectoryNum))
            {
                this.AddItem(
                    new KnowledgeEntry { Name = FolderManager.RelativePath(dir), IsControl = true, IsSearchResult = true });
            }

            foreach (var file in Directory.GetFiles(path).Take(this.maxResultNum))
            {
                this.AddItem(
                    new KnowledgeEntry { Name = FolderManager.RelativePath(file), IsControl = true, IsSearchResult = true });
            }
        }

        private void LoadResultFromFile(string path)
        {
            var lines = File.ReadLines(path);
            foreach (var line in lines.Where(line => !string.IsNullOrWhiteSpace(line)).Take(this.maxLineNum))
            {
                this.AddItem(new KnowledgeEntry { Name = line });
            }
        }

        private void RefreshSearchResult(FoundInfoEventArgs e)
        {
            if (View.Items.Count > this.maxResultNum)
            {
                return;
            }

            var relativePath = FolderManager.RelativePath(e.Info.FullName);

            this.RemoveBlankItem();
            this.AddItem(new KnowledgeEntry { Name = relativePath, IsControl = true, IsSearchResult = true });
            this.AddBlankItem();
        }

        private void RemoveBlankItem()
        {
            if (this.BlankItem != null)
            {
                View.Items.Remove(this.BlankItem);
            }
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get { return this.View.IsEnabled(ViewState.Item_Editting); }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse
            // -----------------------------------------------------------------
            // region
            if (this.Active)
            {
                this.Region.UpdateInput(gameTime);
            }

            if (this.AcceptInput)
            {
                // mouse
                // ---------------------------------------------------------------------
                if (this.ViewSettings.MouseEnabled)
                {
                    // scroll
                    if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                    {
                        this.ScrollBar.Trigger();
                        this.Scroll.MoveUp();
                    }

                    if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                    {
                        this.Scroll.MoveDown();
                        this.ScrollBar.Trigger();
                    }
                }
            }

            // item input
            // -----------------------------------------------------------------
            if (View.Items.Count > 0)
            {
                foreach (var item in View.Items.FindAll(item => item.ItemData.IsControl).ToArray())
                {
                    item.UpdateInput(gameTime);
                }
            }
        }

        /// <remarks>
        /// All state change should be inside this methods.
        /// </remarks>>
        public override void UpdateStructure(GameTime gameTime)
        {
            base          .UpdateStructure(gameTime);
            this.Region   .UpdateStructure(gameTime);
            this.ScrollBar.UpdateStructure(gameTime);
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

        private Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                viewSettings.RowNumDisplay * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}