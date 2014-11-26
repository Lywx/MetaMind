namespace MetaMind.Acutance.Guis.Modules
{
    using System;
    using System.ServiceModel;

    using C3.Primtive2DXna;

    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<MultiplexerModuleSettings>
    {
        private readonly IView view;

        private readonly TaskItemSettings itemSettings = new TaskItemSettings
                                                             {
                                                                 NameFrameSize = new Point(288, 24),
                                                             };

        private readonly TraceViewFactory viewFactory = new TraceViewFactory();

        private readonly TaskViewSettings viewSettings;

        private ISynchronization synchronization;

        private TimeSpan synchronizationTimer = TimeSpan.Zero;

        public MultiplexerModule(MultiplexerModuleSettings settings)
            : base(settings)
        {
            this.viewSettings = new TaskViewSettings
                                    {
                                        StartPoint = new Point(15, 15),
                                        RootMargin = new Point(
                                            this.itemSettings.NameFrameSize.X + this.itemSettings.IdFrameSize.X,
                                            this.itemSettings.NameFrameSize.Y),

                                        ColumnNumMax     = 4,
                                        ColumnNumDisplay = 4,

                                        RowNumDisplay = 29,
                                        RowNumMax     = 100,
                                    };

            this.view = new View(this.viewSettings, this.itemSettings, this.viewFactory);
        }

        public Vector2 SynchronizationNameCenter
        {
            get { return this.SynchronizationFrameRectangle.Center.ToVector2(); }
        }

        public Vector2 SynchronizationTimeCenter
        {
            get { return this.SynchronizationNameCenter + new Vector2(0, 20); }
        }

        private Rectangle SynchronizationFrameRectangle
        {
            get
            {
                return new Rectangle(
                    this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                    this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                    this.Settings.BarFrameSize.X,
                    this.Settings.BarFrameSize.Y);
            }
        }

        private Rectangle SynchronizationBarRectangle(ISynchronization synchronization)
        {
            return new Rectangle(
                this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                (int)(synchronization.ProgressPercent * this.Settings.BarFrameSize.X),
                this.Settings.BarFrameSize.Y);
        }


        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (this.synchronization != null)
            {
                this.DrawSynchronization(this.synchronization);
            }
            
            this.DrawTrace(gameTime, alpha);
        }

        private void DrawTrace(GameTime gameTime, byte alpha)
        {
            this.view.Draw(gameTime, alpha);
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.view.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            if (synchronizationTimer < TimeSpan.FromMilliseconds(100))
            {
                try
                {
                    this.synchronization = Acutance.Synchronization.Fetch() as ISynchronization;
                }
                catch (ServerTooBusyException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
                catch (EndpointNotFoundException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
                catch (TimeoutException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
            }
            else
            {
                this.synchronizationTimer += gameTime.ElapsedGameTime;
                if (this.synchronizationTimer > TimeSpan.FromSeconds(5))
                {
                    this.synchronizationTimer = TimeSpan.Zero;
                }
            }

            this.view.UpdateStructure(gameTime);
        }

        private void DrawSynchronization(ISynchronization synchronization)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.SynchronizationFrameRectangle,
                this.Settings.BarFrameBackgroundColor);

            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.SynchronizationBarRectangle(synchronization),
                synchronization.Enabled ? this.Settings.BarFrameAscendColor : this.Settings.BarFrameDescendColor);

            var task = synchronization.SynchronizedTask;
            if (task != null)
            {
                FontManager.DrawCenteredText(
                    this.Settings.SynchronizationFont,
                    task.Name,
                    this.SynchronizationNameCenter,
                    this.Settings.SynchronizationColor,
                    this.Settings.SynchronizationNameSize);
            }
            
            FontManager.DrawCenteredText(
                this.Settings.SynchronizationFont,
                synchronization.ElapsedTimeSinceTransition.ToString(),
                this.SynchronizationTimeCenter,
                this.Settings.SynchronizationColor,
                this.Settings.SynchronizationTimeSize);
        }
    }

    public class MultiplexerModuleSettings
    {
        public Point BarFrameSize            = new Point(500, 8);

        public int   BarFrameXC              = GraphicsSettings.Width / 2;

        public int   BarFrameYC              = 32;

        public Color BarFrameBackgroundColor = new Color(30, 30, 40, 10);

        public Color BarFrameAscendColor     = new Color(78, 255, 27, 200);

        public Color BarFrameDescendColor    = new Color(255, 0, 27, 200);
        
        //---------------------------------------------------------------------
        public Color   SynchronizationColor    = Color.White;

        public Font    SynchronizationFont     = Font.UiRegularFont;

        public float   SynchronizationNameSize = 0.7f;

        public float   SynchronizationTimeSize = 0.7f;
    }
}