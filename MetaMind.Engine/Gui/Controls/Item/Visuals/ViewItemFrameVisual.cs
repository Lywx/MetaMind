// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrameVisual.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Controls.Item.Visuals
{
    using Elements.Rectangles;
    using Frames;
    using Images;
    using Microsoft.Xna.Framework;
    using Service;

    public class ViewItemFrameVisual : ViewItemComponent, IViewItemVisual
    {
        private readonly FrameSettings frameSettings;

        public ViewItemFrameVisual(IViewItem item, IPressableRectangle rectangle, FrameSettings frameSettings)
            : base(item)
        {
            this.Rectangle         = rectangle;
            this.frameSettings = frameSettings;
            
            this.FrameBoxFilled = new ImageBox(
                () => this.Rectangle.Bounds.Crop(this.frameSettings.Margin),
                () =>
                    {
                        if (this.Item[ItemState.Item_Is_Pending]())
                        {
                            return this.frameSettings.PendingColor;
                        }

                        if (this.Item[ItemState.Item_Is_Editing]())
                        {
                            return this.frameSettings.ModificationColor;
                        }

                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                            this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.SelectionColor;
                        }

                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                           !this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.RegularColor;
                        }

                        if (this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.SelectionColor;
                        }

                        return this.frameSettings.RegularColor;
                    },
                () => true);

            this.FrameBoxDrawn = new ImageBox(
                () => this.Rectangle.Bounds.Crop(this.frameSettings.Margin),
                () =>
                    {
                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                            this.Item[ItemState.Item_Is_Editing]())
                        {
                            return this.frameSettings.MouseOverColor;
                        }
                        
                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                            this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.MouseOverColor;
                        }
                        
                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                           !this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.MouseOverColor;
                        }

                        return Color.Transparent;
                    },
                () => false);
        }

        protected ImageBox FrameBoxDrawn { get; }

        protected ImageBox FrameBoxFilled { get; }

        protected IPressableRectangle Rectangle { get; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            this.FrameBoxDrawn .Draw(graphics, time, alpha);
            this.FrameBoxFilled.Draw(graphics, time, alpha);
        }
    }
}