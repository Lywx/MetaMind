// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemFileDataControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using FileSearcher;

    using MetaMind.Engine.Events;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemFileDataControl : ViewItemDataControl
    {
        private readonly string filePrompt = "File > ";

        private bool searchEnded = true;

        public KnowledgeItemFileDataControl(IViewItem item)
            : base(item)
        {
            Searcher.ThreadEnded += this.SearchEnded;
        }

        public void TrimPrompt()
        {
            ItemData.Name = ItemData.Name.Replace(this.filePrompt, string.Empty);
        }

        public void SetName(string fileName)
        {
            ItemData.Name = string.Format(this.filePrompt + "{0}", fileName);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            if (View.IsEnabled(ViewState.View_Has_Focus))
            {
                Item.Enable(ItemState.Item_Selected);
            }
            else
            {
                Item.Disable(ItemState.Item_Selected);
            }

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
            if (!ItemData.Name.StartsWith(this.filePrompt))
            {
                ItemData.Name = ItemData.Name.Insert(0, this.filePrompt);
            }

            if (this.searchEnded)
            {
                this.searchEnded = false;

                var processor = this.CharModifier as IViewItemCharPostProcessor;
                if (processor != null)
                {
                    var raw      = processor.RemoveCursor(e.NewValue);
                    var fileName = raw.Replace(this.filePrompt, string.Empty);

                    View.Control.Search(fileName);
                }
            }
        }
    }
}