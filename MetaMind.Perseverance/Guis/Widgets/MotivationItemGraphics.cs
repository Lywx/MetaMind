﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using System.Globalization;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class MotivationItemGraphics : ViewItemGraphics
    {
        private readonly MotivationItemSymbolGraphics symbol;

        private readonly MotivationItemTaskGraphics task;

        public MotivationItemGraphics(IViewItem item)
            : base(item)
        {
            this.symbol = new MotivationItemSymbolGraphics(item);
            this.task   = new MotivationItemTaskGraphics(item);
        }

        #region Graphics Data

        //protected override Vector2 IdCenterPosition()
        //{
        //    get
        //    {
        //        return new Vector2(ItemControl.RootFrame.Rectangle.Center.X, ItemControl.RootFrame.Rectangle.Top - 15);
        //    }
        //}

        //private string NameCropped
        //{
        //    get
        //    {
        //        return FontManager.CropMonospacedString(
        //            ItemData.Name,
        //            ItemSettings.NameSize,
        //            ViewSettings.PointMargin.X * 6);
        //    }
        //}

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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!ItemControl.Active)
            {
                return;
            }

            // main motivation item
            this.symbol.Draw(graphics, time, alpha);

            this.DrawName(alpha);
            this.DrawId(graphics, alpha);

            // sub task view
            this.task.Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            this.symbol.Update(time);
        }

        protected override void DrawId(IGameGraphicsService graphics, byte alpha)
        {
            //FontManager.DrawStringCenteredHV(
            //    ItemSettings.IdFont,
            //    ItemControl.Id.ToString(new CultureInfo("en-US")),
            //    this.IdCenterPosition(),
            //    !Item.IsEnabled(ItemState.Item_Pending) ? ItemSettings.IdColor : ItemSettings.IdPendingColor,
            //    ItemSettings.IdSize);
        }

        private void DrawName(byte alpha)
        {
            if (!Item.IsEnabled(ItemState.Item_Selected))
            {
                return;
            }

            //if (Item.IsEnabled(ItemState.Item_Pending))
            //{
            //    FontManager.DrawStringCenteredHV(
            //        ItemSettings.HelpFont,
            //        this.HelpInformation,
            //        this.HelpLocation,
            //        ExtColor.MakeTransparent(ItemSettings.HelpColor, alpha),
            //        ItemSettings.HelpSize);
            //}
            //else
            //{
            //    FontManager.DrawStringCenteredHV(
            //        ItemSettings.NameFont,
            //        this.NameCropped,
            //        this.NameLocation,
            //        ExtColor.MakeTransparent(ItemSettings.NameColor, alpha),
            //        ItemSettings.NameSize);
            //}
        }

        #endregion
    }
}