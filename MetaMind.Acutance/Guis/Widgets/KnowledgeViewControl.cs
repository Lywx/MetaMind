namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using CodeProject.FileSearcher;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class KnowledgeViewControl : GridControl
    {
        private readonly int maxDirectoryNum = 10;

        private readonly int maxLineNum = 200;

        private readonly int maxResultNum = 30;

        #region Constructors

        public KnowledgeViewControl(IView view, KnowledgeViewSettings viewSettings, KnowledgeItemSettings itemSettings, KnowledgeItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.AddFileItem();
            this.AddBlankItem();

            Searcher.FoundInfo += this.RefreshSearchResult;
        }

        #endregion Constructors

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

        public void LoadCall(string name, string path, int minutes)
        {
            var callCreatedEvent = new EventBase(
                (int)AdventureEventType.CallCreated,
                new CallCreatedEventArgs(name, path, minutes));

            GameEngine.EventManager.QueueEvent(callCreatedEvent);
        }

        public void LoadResult(string path, bool relative)
        {
            this.FileItem.ItemControl.SetName(Path.GetFileName(path));

            if (relative)
            {
                path = Path.Combine(FolderManager.DataFolderPath, path);
            }

            this.ClearNonControlItems();
            this.ClearResultItems();

            try
            {
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
            catch (DirectoryNotFoundException)
            {
            }
        }

        private void LoadResultFromFile(string path)
        {
            var lines = File.ReadLines(path);
            foreach (var line in lines.Where(line => !string.IsNullOrWhiteSpace(line)).Take(this.maxLineNum))
            {
                var matchEvent = Regex.Match(line, @"^\[(\d)+\]");
                if (matchEvent.Success)
                {
                    int timeout;
                    int.TryParse(matchEvent.Value.Trim('[', ']'), out timeout);
                    this.AddItem(new KnowledgeEntry(line, line.Replace(matchEvent.Value, string.Empty).Trim(' '), path, timeout));
                }
                else
                {
                    var matchIdea = Regex.Match(line, @"^\|");
                    if (matchIdea.Success)
                    {
                        this.AddItem(new KnowledgeEntry(line, line, path, 0));
                    }
                }
            }
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

            var pars = SearcherParams(fileNames);

            Searcher.Start(pars);
        }

        public void SearchStop()
        {
            this.FileItem.ItemControl.EditCancel();

            Searcher.Stop();
        }

        private static SearcherParams SearcherParams(List<string> fileNames)
        {
            return new SearcherParams(
                searchDir:             FolderManager.DataFolderPath, 
                includeSubDirsChecked: true, 
                fileNames:             fileNames, 
                newerThanChecked:      false, 
                newerThanDateTime:     DateTime.MinValue, 
                olderThanChecked:      false, 
                olderThanDateTime:     DateTime.MinValue, 
                containingChecked:     false, 
                containingText:        string.Empty, 
                encoding:              Encoding.Unicode);
        }

        private void AddBlankItem()
        {
            var blankItem = new KnowledgeEntry(string.Empty) { IsBlank = true };
            this.AddItem(blankItem);
        }

        private void AddFileItem()
        {
            var fileItem = new KnowledgeEntry(string.Empty) { IsFile = true };
            this.InsertItem(0, fileItem);
        }

        private void AddItem(KnowledgeEntry entry)
        {
            var item = new ViewItemExchangeless(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry);
            View.Items.Add(item);
        }

        private void ClearNonControlItems()
        {
            foreach (var item in this.View.Items.FindAll(item => !item.ItemData.IsControl))
            {
                View.Items.Remove(item);
                item.Dispose();
            }
        }

        private void ClearResultItems()
        {
            foreach (var item in this.View.Items.FindAll(item => item.ItemData.IsSearchResult))
            {
                View.Items.Remove(item);
                item.Dispose();
            }
        }

        private void InsertBlankItem()
        {
            var blankItem = new KnowledgeEntry(string.Empty) { IsBlank = true };
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
                this.AddItem(new KnowledgeEntry(FolderManager.RelativePath(dir)) { IsSearchResult = true });
            }

            foreach (var file in Directory.GetFiles(path).Take(this.maxResultNum))
            {
                this.AddItem(new KnowledgeEntry(FolderManager.RelativePath(file)) { IsSearchResult = true });
            }
        }

        private void RefreshSearchResult(FoundInfoEventArgs e)
        {
            if (View.Items.Count > this.maxResultNum)
            {
                return;
            }

            var relativePath = FolderManager.RelativePath(e.Info.FullName);
            
            // won't load git repository
            if (relativePath.Contains(".git"))
            {
                return;
            }

            this.RemoveBlankItem();
            this.AddItem(new KnowledgeEntry(relativePath) { IsSearchResult = true });
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

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();

            if (View.Items.Count > 0)
            {
                foreach (var item in View.Items.FindAll(item => item.ItemData.IsControl || item.ItemData.IsCall).ToArray())
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
                viewSettings.ColumnNumDisplay * (itemSettings.NameFrameSize.X + itemSettings.IdFrameSize.X + itemSettings.ExperienceFrameSize.X),
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}