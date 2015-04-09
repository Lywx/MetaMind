namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MotivationModuleFactory : IConfigurationParameterLoader<GraphicsSettings>
    {
        private Point IntelligenceViewStartPoint;

        private int IntelligenceViewColumnNum;

        private MotivationViewSettings IntelligenceViewSettings;

        #region Parameters

        private bool IsFullscreen { get; set; }

        private int Width { get; set; }

        public void ParameterLoad(GraphicsSettings parameter)
        {
            this.IsFullscreen = parameter.IsFullscreen;
            this.Width = parameter.Width;
        }

        #endregion Parameters

        #region Constructors

        public MotivationModuleFactory()
        {
            this.ParameterLoad(GameEngine.GraphicsSettings);
        }

        #endregion Constructors

        private MotivationModule CreateModule()
        {
            if (this.IsFullscreen)
            {
                this.IntelligenceViewStartPoint = new Point(this.Width / 2, 160);
                this.IntelligenceViewColumnNum = 9;
            }
            else
            {
                this.IntelligenceViewStartPoint = new Point(160 + 270, 160);
                this.IntelligenceViewColumnNum = 18;
            }

            this.IntelligenceViewSettings = new MotivationViewSettings(this.IntelligenceViewStartPoint)
                                                { 
                                                    ColumnNumDisplay = this.IntelligenceViewColumnNum
                                                };

            return new MotivationModule(new MotivationModuleSettings(this.IntelligenceViewSettings));
        }
    }
}