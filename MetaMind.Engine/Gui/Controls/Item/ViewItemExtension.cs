// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemExtension.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class ViewItemExtension
    {
        public static Func<Texture2D> GetImageSelector(this IViewItem item, ViewItemVisualSettings settings)
        {
            return () =>
            {
                var image = settings.Image;

                if (ViewItemState.Item_Is_Selected.Match(item)) return image.Selected;
                if (ViewItemState.Item_Is_Pending .Match(item)) return image.Pending;
                if (ViewItemState.Item_Is_Editing .Match(item)) return image.Editing;

                if (ViewItemState.Item_Is_Mouse_Over.Match(item)) return image.MouseOver;
                else                                              return image.MouseOut;
            };
        }

        public static Func<Rectangle> GetBoundsSelector(this IViewItem item, ViewItemVisualSettings settings, Rectangle bounds)
        {
            return () =>
            {
                var margin = settings.Margin;

                if (ViewItemState.Item_Is_Selected.Match(item)) return bounds.Crop(margin.Selected); 
                if (ViewItemState.Item_Is_Pending .Match(item)) return bounds.Crop(margin.Pending); 
                if (ViewItemState.Item_Is_Editing .Match(item)) return bounds.Crop(margin.Editing); 

                if (ViewItemState.Item_Is_Mouse_Over.Match(item)) return bounds.Crop(margin.MouseOver); 
                else                                              return bounds.Crop(margin.MouseOut); 
            };
        }

        public static Func<Color> GetColorSelector(this IViewItem item, ViewItemVisualSettings settings)
        {
            return () =>
            {
                var color = settings.Color;

                if (ViewItemState.Item_Is_Selected.Match(item)) return color.Selected;
                if (ViewItemState.Item_Is_Pending .Match(item)) return color.Pending;
                if (ViewItemState.Item_Is_Editing .Match(item)) return color.Editing;

                if (ViewItemState.Item_Is_Mouse_Over.Match(item)) return color.MouseOver;
                else                                              return color.MouseOut;
            };
        }
    }
}