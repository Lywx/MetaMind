namespace MetaMind.Engine.Entities.Elements
{
    using Controls.Buttons;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    // TODO: Remove it from this namespace
    public static class IMMElementExtension
    {
        public static Func<MMRectangleButtonSettings, Texture2D> ImageSelector(
            this IMMInputElement element,
            MMRectangleButtonSettings settings)
        {
            return setting =>
            {
                if (element[MMInputElementDebugState.Mouse_Is_Left_Pressed]()
                    || element[MMInputElementDebugState.Mouse_Is_Right_Pressed]())
                {
                    return settings.Image.MousePressed;
                }

                if (element[MMInputElementDebugState.Mouse_Is_Left_Released]()
                    || element[MMInputElementDebugState.Mouse_Is_Left_Released]())
                {
                    return settings.Image.MouseReleased;
                }

                if (element[MMInputElementDebugState.Mouse_Is_Over]())
                {
                    return settings.Image.MouseOver;
                }

                return settings.Image.MouseOut;
            };
        }
    }
}