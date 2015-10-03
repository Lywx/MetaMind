// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputableElementExtension.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Elements
{
    using System;
    using Controls.Buttons;
    using Microsoft.Xna.Framework.Graphics;

    public static class IMMElementExtension
    {
        public static Func<RectangleButtonSettings, Texture2D> ImageSelector(this IMMElement element, RectangleButtonSettings settings)
        {
            return (setting) =>
            {
                if (element[MMElementState.Mouse_Is_Left_Pressed]() ||
                    element[MMElementState.Mouse_Is_Right_Pressed]())
                {
                    return settings.Image.MousePressed;
                }
                if (element[MMElementState.Mouse_Is_Left_Released]() ||
                    element[MMElementState.Mouse_Is_Left_Released]())
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