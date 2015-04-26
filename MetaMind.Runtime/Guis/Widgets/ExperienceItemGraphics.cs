﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExperienceItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ExperienceItemGraphics : ViewItemGraphics
    {
        private readonly string HelpInformation = "N:Name";

        public ExperienceItemGraphics(IViewItem item)
            : base(item)
        {
            this.IdCenterPosition = () => new Vector2(this.RootFrame.Rectangle.Center.X, this.RootFrame.Rectangle.Top - 15);
            this.NameCenterPosition = () => this.RootFrame.Rectangle.Center.ToVector2() + this.ItemSettings.NameMargin.ToVector2();
            this.HelpCenterPosition = this.NameCenterPosition;

            this.IdLabel.TextColor = () =>
                !this.Item.IsEnabled(ItemState.Item_Pending)
                    ? (Color)this.ItemSettings.IdColor
                    : (Color)this.ItemSettings.IdPendingColor;

            this.NameLabel = new Label(
                () => this.Item.IsEnabled(ItemState.Item_Pending) ? this.ItemSettings.HelpFont : this.ItemSettings.NameFont,
                () => this.Item.IsEnabled(ItemState.Item_Pending) ? this.HelpInformation : this.ItemData.Name,
                this.NameCenterPosition,
                () => this.Item.IsEnabled(ItemState.Item_Pending) ? this.ItemSettings.HelpColor : this.ItemSettings.NameColor,
                () => this.Item.IsEnabled(ItemState.Item_Pending) ? this.ItemSettings.HelpSize : this.ItemSettings.NameSize,
                StringHAlign.Center,
                StringVAlign.Center,
                false);
        }

        protected Label NameLabel { get; set; }

        protected Func<Vector2> NameCenterPosition { get; set; }

        protected Func<Vector2> HelpCenterPosition { get; set; }

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!ItemControl.Active && 
                !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.NameDrawnBox .Draw(graphics, time, alpha);
            this.NameFilledBox.Draw(graphics, time, alpha);

            this.IdLabel  .Draw(graphics, time, alpha);
            this.NameLabel.Draw(graphics, time, alpha);
        }

        #endregion
    }
}