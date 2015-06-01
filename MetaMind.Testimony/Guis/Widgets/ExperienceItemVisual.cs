// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExperienceItemVisual.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Guis.Widgets
{
    using System;
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Visuals;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class ExperienceItemVisual : ViewItemVisual
    {
        public ExperienceItemVisual(IViewItem item)
            : base(item)
        {
            this.RootFrame = 

            this.IdCenterPosition   = () => new Vector2(this.RootFrame.Rectangle.Center.X, this.RootFrame.Rectangle.Top - 15);
            this.NameCenterPosition = () => this.RootFrame.Rectangle.Center.ToVector2() + this.ItemSettings.NameMargin.ToVector2();
            this.HelpCenterPosition = this.NameCenterPosition;

            this.IdLabel.TextColor = () =>
                !this.Item[ItemState.Item_Is_Pending]()
                    ? (Color)this.ItemSettings.IdColor
                    : (Color)this.ItemSettings.IdPendingColor;

            this.NameLabel = new Label(
                () => this.Item[ItemState.Item_Is_Pending]() ? this.ItemSettings.HelpFont : this.ItemSettings.NameFont,
                () => this.Item[ItemState.Item_Is_Pending]() ? this.HelpInformation : this.ItemData.Name,
                this.NameCenterPosition,
                () => this.Item[ItemState.Item_Is_Pending]() ? this.ItemSettings.HelpColor : this.ItemSettings.NameColor,
                () => this.Item[ItemState.Item_Is_Pending]() ? this.ItemSettings.HelpSize : this.ItemSettings.NameSize,
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
            if (!this.ItemLogic.Active && 
                !Item[ItemState.Item_Is_Dragging]())
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