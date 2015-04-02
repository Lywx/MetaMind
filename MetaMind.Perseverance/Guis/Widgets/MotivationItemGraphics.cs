// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using System.Globalization;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class MotivationItemGraphics : ViewItemBasicGraphics
    {
        private readonly MotivationItemSymbolGraphics symbol;

        private readonly MotivationItemTaskGraphics task;

        public MotivationItemGraphics(IViewItem item)
            : base(item)
        {
            this.symbol = new MotivationItemSymbolGraphics(item);
            this.task = new MotivationItemTaskGraphics(item);
        }

        protected override Vector2 IdCenter
        {
            get
            {
                return new Vector2(
                    ItemControl.RootFrame.Rectangle.Center.X,
                    ItemControl.RootFrame.Rectangle.Top - 15);
            }
        }

        private string NameCropped
        {
            get
            {
                return FontManager.CropText(
                    this.ItemSettings.NameFont,
                    this.ItemData.Name,
                    this.ItemSettings.NameSize,
                    this.ViewSettings.RootMargin.X * 6);
            }
        }

        private string HelpInformation
        {
            get { return "N:Name"; }
        }

        private Vector2 HelpLocation
        {
            get { return this.NameLocation; }
        }

        private Vector2 NameLocation
        {
            get
            {
                return PointExt.ToVector2(ItemControl.RootFrame.Rectangle.Center)
                       + PointExt.ToVector2(ItemSettings.NameMargin);
            }
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active)
            {
                return;
            }

            // main motivation item
            this.DrawSymbol(gameTime, alpha);
            this.DrawName(alpha);
            this.DrawId(alpha);
            
            // sub task view
            this.DrawTasks(gameTime, alpha);
        }

        public override void Update(GameTime gameTime)
        {
            this.symbol.Update(gameTime);
        }

        protected override void DrawId(byte alpha)
        {
            FontManager.DrawCenteredText(
                ItemSettings.IdFont, 
                ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenter,
                !Item.IsEnabled(ItemState.Item_Pending) ? ItemSettings.IdColor : ItemSettings.IdPendingColor, 
                ItemSettings.IdSize);
        }

        private void DrawName(byte alpha)
        {
            if (!Item.IsEnabled(ItemState.Item_Selected))
            {
                return;
            }

            if (this.Item.IsEnabled(ItemState.Item_Pending))
            {
                FontManager.DrawCenteredText(
                    this.ItemSettings.HelpFont,
                    this.HelpInformation,
                    this.HelpLocation,
                    ColorExt.MakeTransparent(this.ItemSettings.HelpColor, alpha),
                    this.ItemSettings.HelpSize);
            }
            else
            {
                FontManager.DrawCenteredText(
                    this.ItemSettings.NameFont,
                    this.NameCropped,
                    this.NameLocation,
                    ColorExt.MakeTransparent(this.ItemSettings.NameColor, alpha),
                    this.ItemSettings.NameSize);
            }
        }

        private void DrawSymbol(GameTime gameTime, byte alpha)
        {
            this.symbol.Draw(gameTime, alpha);
        }

        private void DrawTasks(GameTime gameTime, byte alpha)
        {
            this.task.Draw(gameTime, alpha);
        }
    }
}