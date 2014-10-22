using System;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Engine.Settings
{
    public class MessageSettings
    {
        public Font MessageFont = Font.UiRegularFont;
        public float MessageSize = 1f;
        public Color MessageColor = Color.White;
        public TimeSpan MessageLastingPeriod = TimeSpan.FromSeconds( 1.5 );
        public Vector2 MessagePosition = new Vector2( 5, 5 );

        /// <summary>
        /// Gets the default message setting
        /// </summary>
        /// <remarks>
        /// Must create instance after <seealso cref="FontManager"/>
        /// </remarks>
        public static MessageSettings Default
        {
            get { return new MessageSettings(); }
        }
    }
}