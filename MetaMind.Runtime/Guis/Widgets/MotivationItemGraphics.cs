// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Guis.Widgets
{
    using System.Globalization;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class MotivationItemGraphics : ViewItemGraphics
    {
        public MotivationItemGraphics(IViewItem item)
            : base(item)
        {
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
                return ExtPoint.ToVector2(this.ItemControl.RootFrame.Rectangle.Center)
                       + ExtPoint.ToVector2(this.ItemSettings.NameMargin);
            }
        }

        #endregion

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.ItemControl.Active)
            {
                return;
            }


            this.DrawName(alpha);
            this.DrawId(graphics, alpha);
        }

        public override void Update(GameTime time)
        {
        }

        protected override void DrawId(IGameGraphicsService graphics, byte alpha)
        {
            graphics.StringDrawer.DrawString(
                this.ItemSettings.IdFont,
                this.ItemControl.Id.ToString(new CultureInfo("en-US")),
                this.IdCenterPosition(),
                !this.Item.IsEnabled(ItemState.Item_Pending) ? this.ItemSettings.IdColor : this.ItemSettings.IdPendingColor,
                this.ItemSettings.IdSize,
                StringHAlign.Center,
                StringVAlign.Center);
        }

        private void DrawName(byte alpha)
        {
            if (!this.Item.IsEnabled(ItemState.Item_Selected))
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