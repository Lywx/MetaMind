namespace MetaMind.Engine.Core.Entity
{
    using System;
    using Control.Buttons;
    using Entity.Input;
    using Microsoft.Xna.Framework.Graphics;

    // TODO: Remove it from this namespace
    public static class IMMElementExtension
    {
        public static Func<MMRectangleButtonSettings, Texture2D> ImageSelector(
            this IMMElement element,
            MMRectangleButtonSettings settings)
        {
            return setting =>
            {
                if (element[MMElementState.Mouse_Is_Left_Pressed]()
                    || element[MMElementState.Mouse_Is_Right_Pressed]())
                {
                    return settings.Image.MousePressed;
                }

                if (element[MMElementState.Mouse_Is_Left_Released]()
                    || element[MMElementState.Mouse_Is_Left_Released]())
                {
                    return settings.Image.MouseReleased;
                }

                if (element[MMElementState.Mouse_Is_Over]())
                {
                    return settings.Image.MouseOver;
                }

                return settings.Image.MouseOut;
            };
        }
    }
}