// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationGroupSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class SynchronizationGroupSettings
    {
        public Color BarFrameAscendColor = new Color(78, 255, 27, 200);

        public Color BarFrameBackgroundColor = new Color(30, 30, 40, 10);

        public Color BarFrameDescendColor = new Color(255, 0, 27, 200);

        public Point BarFrameSize = new Point(500, 8);

        public int   BarFrameXC = GraphicsSettings.Width / 2;

        public int   BarFrameYC = 16;

        public Color StateColor = Color.White;

        public Font  StateFont = Font.UiRegularFont;

        public Point StateMargin = new Point(0, 1);

        public float StateSize = 1.1f;

        public Color SynchronizationTimeColor = Color.White;

        public Font  SynchronizationTimeFont = Font.UiRegularFont;

        public float SynchronizationTimeSize = 0.7f;

        public Color TaskColor = Color.White;

        public Font  TaskFont = Font.UiContentFont;

        public Point TaskMargin = new Point(0, 34);

        public float TaskSize = 0.7f;
    }
}