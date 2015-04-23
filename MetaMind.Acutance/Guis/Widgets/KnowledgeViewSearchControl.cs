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
            ViewControl.FileItem.ItemControl.SetName(Path.GetFileName(path));

            if (relative)
            {
                path = FileManager.DataPath(path);
            }

            ViewControl.ClearNonControlItems();
            ViewControl.ClearResultItems();

            try
            {
                var isFile = (File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory;
                if (isFile)
                {
                    this.LoadResultFromFile(path, offset);

                    ViewControl.Scroll.MoveUpToTop();
                }
                else
                {
                    this.LoadResultFromDirectory(path);

                    ViewControl.Scroll.MoveUpToTop();
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
            if (ViewControl.FileItem == null)
            {
                return;
            }

            ViewControl.ClearNonControlItems();
            ViewControl.ClearResultItems();

            var searchName     = SearchSettings.SearchName(fileName, true);
            var searcherParams = SearchSettings.SearchParams(searchName);
            Searcher.Start(searcherParams);
        }

        public void SearchStop()
        {
            ViewControl.FileItem.ItemControl.EditCancel();

            Searcher.Stop();
        }

        private void LoadResultFromDirectory(string path)
        {
            foreach (var dir in Directory.GetDirectories(path).Take(this.directoryNumMax))
            {
                ViewControl.AddResultItem(FileManager.DataRelativePath(dir));
            }

            foreach (var file in Directory.GetFiles(path).Take(this.fileNumMax))
            {
                ViewControl.AddResultItem(FileManager.DataRelativePath(file));
            }
        }

        private void LoadResultFromFile(string path, int offset)
        {
            var query = KnowledgeLoader.LoadFile(path, offset);
            if (query == null)
            {
                return;
            }

            ViewControl.FileBuffer = query.Buffer;

            foreach (var entry in query.Data)
            {
                ViewControl.AddItem(entry);
            }
        }

        private void RefreshSearchResult(FoundInfoEventArgs e)
        {
            if (View.Items.Count > this.resultNumMax)
            {
                return;
            }

            var relativePath = FileManager.DataRelativePath(e.Info.FullName);
            
            // won't load git repository
            if (relativePath.Contains(".git"))
            {
                return;
            }

            ViewControl.RemoveBlankItem();
            ViewControl.AddResultItem(relativePath);
            ViewControl.AddBlankItem();
        }
    }
}