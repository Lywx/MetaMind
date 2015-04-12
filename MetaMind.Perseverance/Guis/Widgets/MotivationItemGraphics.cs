// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using System.Globalization;

    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class MotivationItemGraphics : ViewItemGraphics
    {
        private readonly MotivationItemSymbolGraphics symbol;

        private readonly MotivationItemTaskGraphics task;

        public MotivationItemGraphics(IViewItem item)
            : base(item)
        {
            this.symbol = new MotivationItemSymbolGraphics(item);
            this.task = new MotivationItemTaskGraphics(item);
        }

        #region Graphics Data

        protected override Vector2 IdCenter
        {
            get
            {
                return new Vector2(ItemControl.RootFrame.Rectangle.Center.X, ItemControl.RootFrame.Rectangle.Top - 15);
            }
        }

        private string NameCropped
        {
            get
            {
                return FontManager.CropMonospacedString(
                    ItemData.Name,
                    ItemSettings.NameSize,
                    ViewSettings.PointMargin.X * 6);
            }
        }

        private string HelpInformation
        {
            get
            {
                return "N:Name";
            }
        }

        private Vector2 HelpLocation
        {
            get
            {
                return this.NameLocation;
            }
        }

        private Vector2 NameLocation
        {
            get
            {
                return ExtPoint.ToVector2(ItemControl.RootFrame.Rectangle.Center)
                       + ExtPoint.ToVector2(ItemSettings.NameMargin);
            }
        }

        #endregion

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active)
            {
                return;
            }

            // main motivation item
            this.DrawSymbol(gameTime, alpha);
            this.DrawName(alpha);
            this.DrawId(TODO, alpha);

            // sub task view
            this.DrawTasks(gameTime, alpha);
        }

        public override void Update(GameTime gameTime)
        {
            this.symbol.Update(gameTime);
        }

        protected override void DrawId(IGameGraphics gameGraphics, byte alpha)
        {
            FontManager.DrawStringCenteredHV(
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

            if (Item.IsEnabled(ItemState.Item_Pending))
            {
                FontManager.DrawStringCenteredHV(
                    ItemSettings.HelpFont,
                    this.HelpInformation,
                    this.HelpLocation,
                    ExtColor.MakeTransparent(ItemSettings.HelpColor, alpha),
                    ItemSettings.HelpSize);
            }
            else
            {
                FontManager.DrawStringCenteredHV(
                    ItemSettings.NameFont,
                    this.NameCropped,
                    this.NameLocation,
                    ExtColor.MakeTransparent(ItemSettings.NameColor, alpha),
                    ItemSettings.NameSize);
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

        #endregion
    }
}