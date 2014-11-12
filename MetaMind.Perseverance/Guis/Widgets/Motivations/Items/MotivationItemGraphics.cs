// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using System.Collections.Generic;
    using System.Globalization;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    using Microsoft.Xna.Framework;

    public class MotivationItemGraphics : ViewItemBasicGraphics
    {
        private readonly MotivationItemSymbolGraphics symbol;

        private readonly MotivationItemTaskGraphics task;

        public MotivationItemGraphics(IViewItem item)
            : base(item)
        {
            this.symbol = new MotivationItemSymbolGraphics(item);
            this.task   = new MotivationItemTaskGraphics(item);
        }

        private Vector2 NamePosition
        {
            get { return PointExt.ToVector2(ItemControl.RootFrame.Rectangle.Center) + new Vector2(0, 50); }
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
                this.ItemSettings.IdFont, 
                this.ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenter,
                this.Item.IsEnabled(ItemState.Item_Pending)
                    ? this.ItemSettings.IdPendingColor
                    : this.ItemSettings.IdColor, 
                this.ItemSettings.IdSize);
        }

        private void DrawName(byte alpha)
        {
            if (!this.Item.IsEnabled(ItemState.Item_Selected))
            {
                return;
            }

            List<string> text = Text.BreakTextIntoList(
                this.ItemSettings.NameFont, 
                this.ItemSettings.NameSize, 
                this.ItemData.Name, 
                (float)this.ViewSettings.RootMargin.X * 4);
            for (var i = 0; i < text.Count; i++)
            {
                FontManager.DrawCenteredText(
                    this.ItemSettings.NameFont, 
                    text[i], 
                    this.NamePosition + new Vector2(0, this.ItemSettings.NameLineMargin) * i, 
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