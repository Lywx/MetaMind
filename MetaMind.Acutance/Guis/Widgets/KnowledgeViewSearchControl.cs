namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
    using System.IO;
    using System.Linq;

    using FileSearcher;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Settings;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class KnowledgeViewSearchControl : ViewComponent
    {
        private readonly int directoryNumMax = 10;
        private readonly int fileNumMax      = 20;
        private readonly int resultNumMax    = 30;

        public KnowledgeViewSearchControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Searcher = new Searcher();
            this.Searcher.FoundInfo += this.RefreshSearchResult;
        }

        public Searcher Searcher { get; set; }

        /// <summary>
        /// Load knowledge or schedule from file, occasionally from path.
        /// </summary>
        /// <param name="offset">offset of line number when loading file</param>
        public void LoadResult(string path, bool relative, int offset)
        {
            this.ViewLogic.FileItem.ItemControl.SetName(Path.GetFileName(path));

            if (relative)
            {
                path = FileManager.DataPath(path);
            }

            this.ViewLogic.ClearNonControlItems();
            this.ViewLogic.ClearResultItems();

            try
            {
                var isFile = (File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory;
                if (isFile)
                {
                    this.LoadResultFromFile(path, offset);

                    this.ViewLogic.Scroll.MoveUpToTop();
                }
                else
                {
                    this.LoadResultFromDirectory(path);

                    this.ViewLogic.Scroll.MoveUpToTop();
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
        }

        public void Search(string fileName)
        {
            if (this.ViewLogic.FileItem == null)
            {
                return;
            }

            this.ViewLogic.ClearNonControlItems();
            this.ViewLogic.ClearResultItems();

            var searchName     = SearchSettings.SearchName(fileName, true);
            var searcherParams = SearchSettings.SearchParams(searchName);
            Searcher.Start(searcherParams);
        }

        public void SearchStop()
        {
            this.ViewLogic.FileItem.ItemControl.EditCancel();

            Searcher.Stop();
        }

        private void LoadResultFromDirectory(string path)
        {
            foreach (var dir in Directory.GetDirectories(path).Take(this.directoryNumMax))
            {
                this.ViewLogic.AddResultItem(FileManager.DataRelativePath(dir));
            }

            foreach (var file in Directory.GetFiles(path).Take(this.fileNumMax))
            {
                this.ViewLogic.AddResultItem(FileManager.DataRelativePath(file));
            }
        }

        private void LoadResultFromFile(string path, int offset)
        {
            var query = KnowledgeLoader.LoadFile(path, offset);
            if (query == null)
            {
                return;
            }

            this.ViewLogic.FileBuffer = query.Buffer;

            foreach (var entry in query.Data)
            {
                this.ViewLogic.AddItem(entry);
            }
        }

        private void RefreshSearchResult(FoundInfoEventArgs e)
        {
            if (View.ViewItems.Count > this.resultNumMax)
            {
                return;
            }

            var relativePath = FileManager.DataRelativePath(e.Info.FullName);
            
            // won't load git repository
            if (relativePath.Contains(".git"))
            {
                return;
            }

            this.ViewLogic.RemoveBlankItem();
            this.ViewLogic.AddResultItem(relativePath);
            this.ViewLogic.AddBlankItem();
        }
    }
}