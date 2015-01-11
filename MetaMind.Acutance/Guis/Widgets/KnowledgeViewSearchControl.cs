namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using FileSearcher;

    using MetaMind.Acutance.Concepts;
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
            Searcher.FoundInfo += this.RefreshSearchResult;
        }

        /// <summary>
        /// Load knowledge or schedule from file, occasionally from path.
        /// </summary>
        /// <param name="offset">offset of line number when loading file</param>
        public void LoadResult(string path, bool relative, int offset)
        {
            ViewControl.FileItem.ItemControl.SetName(Path.GetFileName(path));

            if (relative)
            {
                path = FolderManager.DataPath(path);
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

            Searcher.Start(SearchParams(SearchName(fileName)));
        }

        public void SearchStop()
        {
            ViewControl.FileItem.ItemControl.EditCancel();

            Searcher.Stop();
        }

        private static SearcherParams SearchParams(List<string> fileNames)
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

        private static List<string> SearchName(string fileName)
        {
            // won't be able to search directories now with .md subfix
            return new List<string>(1) { "*" + fileName + "*" + ".md" };
        }

        private void LoadResultFromDirectory(string path)
        {
            foreach (var dir in Directory.GetDirectories(path).Take(this.directoryNumMax))
            {
                ViewControl.AddResultItem(FolderManager.DataRelativePath(dir));
            }

            foreach (var file in Directory.GetFiles(path).Take(this.fileNumMax))
            {
                ViewControl.AddResultItem(FolderManager.DataRelativePath(file));
            }
        }

        private void LoadResultFromFile(string path, int offset)
        {
            var query = KnowledgeLoader.LoadFile(path, offset);
            if (query == null)
            {
                return;
            }

            ViewControl.FileBuffer = query;

            foreach (var entry in query.Entries)
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

            var relativePath = FolderManager.DataRelativePath(e.Info.FullName);
            
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