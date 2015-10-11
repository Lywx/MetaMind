namespace MetaMind.Engine.Entities.Controls.Item
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class MMViewItemExtension
    {
        public static Func<Texture2D> GetImageSelector(this IMMViewItem item, MMViewItemRenderSettings settings)
        {
            return () =>
            {
                var image = settings.Image;

                if (MMViewItemState.Item_Is_Selected.Match(item)) return image.Selected;
                if (MMViewItemState.Item_Is_Pending .Match(item)) return image.Pending;
                if (MMViewItemState.Item_Is_Editing .Match(item)) return image.Editing;

                if (MMViewItemState.Item_Is_Mouse_Over.Match(item)) return image.MouseOver;
                else                                              return image.MouseOut;
            };
        }

        public static Func<Rectangle> GetBoundsSelector(this IMMViewItem item, MMViewItemRenderSettings settings, Rectangle bounds)
        {
            return () =>
            {
                var margin = settings.Margin;

                if (MMViewItemState.Item_Is_Selected.Match(item)) return bounds.Crop(margin.Selected); 
                if (MMViewItemState.Item_Is_Pending .Match(item)) return bounds.Crop(margin.Pending); 
                if (MMViewItemState.Item_Is_Editing .Match(item)) return bounds.Crop(margin.Editing); 

                if (MMViewItemState.Item_Is_Mouse_Over.Match(item)) return bounds.Crop(margin.MouseOver); 
                else                                              return bounds.Crop(margin.MouseOut); 
            };
        }

        public static Func<Color> GetColorSelector(this IMMViewItem item, MMViewItemRenderSettings settings)
        {
            return () =>
            {
                var color = settings.Color;

                if (MMViewItemState.Item_Is_Selected.Match(item)) return color.Selected;
                if (MMViewItemState.Item_Is_Pending .Match(item)) return color.Pending;
                if (MMViewItemState.Item_Is_Editing .Match(item)) return color.Editing;

                if (MMViewItemState.Item_Is_Mouse_Over.Match(item)) return color.MouseOver;
                else                                              return color.MouseOut;
            };
        }
    }
}