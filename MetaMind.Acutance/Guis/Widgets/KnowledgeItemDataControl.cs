// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemDataControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using CodeProject.FileSearcher;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemDataControl : ViewItemDataControl
    {
        private readonly string filePrompt = "File > ";

        private bool searchEnded = true;

        public KnowledgeItemDataControl(IViewItem item)
            : base(item)
        {
            Searcher.ThreadEnded += this.SearchEnded;
        }

        public void SetName(string fileName)
        {
            ItemData.Name = string.Format(filePrompt + "{0}", fileName);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            if (Item.IsEnabled(ItemState.Item_Selected) &&
                ItemData.IsSearchResult)
            {
                ItemControl.MouseUnselectIt();

                View.Control.LoadResult(ItemData.Name);
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            if (Item.IsEnabled(ItemState.Item_Editing))
            {
                CharModifier.ValueModified     += this.StartSearch;
                CharModifier.ModificationEnded += this.StartSearch;
            }
        }

        private void SearchEnded(ThreadEndedEventArgs e)
        {
            this.searchEnded = true;
        }

        private void StartSearch(object sender, ViewItemDataEventArgs e)
        {
            if (ItemData.IsFile)
            {
                if (!ItemData.Name.StartsWith(this.filePrompt))
                {
                    ItemData.Name = ItemData.Name.Insert(0, this.filePrompt);
                }

                if (this.searchEnded)
                {
                    this.searchEnded = false;

                    var raw = ((IViewItemCharPostProcessor)CharModifier).RemoveCursor(e.NewValue);
                    var fileName = raw.Replace(this.filePrompt, string.Empty).Replace(this.filePrompt.TrimEnd(' '), string.Empty);

                    View.Control.Search(fileName);
                }
            }
        }
    }
}