// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationGroupSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class SynchronizationGroupSettings : IParameterLoader<GraphicsSettings>
    {
        public Color BarFrameAscendColor = new Color(78, 255, 27, 200);

        public Color BarFrameBackgroundColor = new Color(30, 30, 40, 10);

        public Color BarFrameDescendColor = new Color(255, 0, 27, 200);

        public Point BarFrameSize = new Point(500, 8);

        public int   BarFrameXC;

        public int   BarFrameYC = 16;

        public Color StateColor = Color.White;

        public Font  StateFont = Font.UiRegular;

        public Point StateMargin = new Point(0, 1);

        public float StateSize = 1.1f;

        public Color SynchronizationTimeColor = Color.White;

        public Font  SynchronizationTimeFont = Font.UiRegular;

        public float SynchronizationTimeSize = 0.7f;

        public Color TaskColor = Color.White;

        public Font  TaskFont = Font.ContentRegular;

        public Point TaskMargin = new Point(0, 34);

        public float TaskSize = 0.7f;

        public SynchronizationGroupSettings()
        {
            this.LoadParameter(GameEngine.GraphicsSettings);

            this.BarFrameXC = this.Width / 2;
        }

        #region Constructors

        #endregion

        #region Parameters

        private int Width { get; set; }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.Width = parameter.Width;
        }

        #endregion
    }
}