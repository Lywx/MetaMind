// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputableElementExtension.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Elements
{
    using System;
    using Controls.Buttons;
    using Microsoft.Xna.Framework.Graphics;

    public static class IInputableElementExtension
    {
        public static Func<RectangleButtonSettings, Texture2D> ImageSelector(this IInputableElement element, RectangleButtonSettings settings)
        {
            return (setting) =>
            {
                if (element[ElementState.Mouse_Is_Left_Pressed]() ||
                    element[ElementState.Mouse_Is_Right_Pressed]())
                {
                    return settings.Image.MousePressed;
                }
                if (element[ElementState.Mouse_Is_Left_Released]() ||
                    element[ElementState.Mouse_Is_Left_Released]())
                {
                    return settings.Image.MouseReleased;
                }
                if (element[ElementState.Mouse_Is_Over]())
                {
                    return settings.Image.MouseOver;
                }

                return settings.Image.MouseOut;
            };
        }
    }
}