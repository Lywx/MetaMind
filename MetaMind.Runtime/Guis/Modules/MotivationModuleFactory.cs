namespace MetaMind.Runtime.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;
    using MetaMind.Runtime.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MotivationModuleFactory : GameVisualEntity, IParameterLoader<GraphicsSettings>
    {
        private Point IntelligenceViewStartPoint;

        private int IntelligenceViewColumnNum;

        private MotivationViewSettings IntelligenceViewSettings;

        #region Parameters

        private bool IsFullscreen { get; set; }

        private int Width { get; set; }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.IsFullscreen = parameter.IsFullscreen;
            this.Width = parameter.Width;
        }

        #endregion Parameters

        #region Constructors

        public MotivationModuleFactory()
        {
            this.LoadParameter(this.GameGraphics.Settings);
        }

        #endregion Constructors

        private MotivationModule CreateModule()
        {
            if (this.IsFullscreen)
            {
                this.IntelligenceViewStartPoint = new Point(this.Width / 2, 160);
                this.IntelligenceViewColumnNum  = 9;
            }
            else
            {
                this.IntelligenceViewStartPoint = new Point(160 + 270, 160);
                this.IntelligenceViewColumnNum  = 18;
            }

            this.IntelligenceViewSettings = new MotivationViewSettings(this.IntelligenceViewStartPoint)
                                                { 
                                                    ColumnNumDisplay = this.IntelligenceViewColumnNum
                                                };

            return new MotivationModule(new MotivationModuleSettings(this.IntelligenceViewSettings));
        }
    }
}