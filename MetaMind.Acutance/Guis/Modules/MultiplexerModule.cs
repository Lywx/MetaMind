namespace MetaMind.Acutance.Guis.Modules
{
    using System;
    using System.Globalization;
    using System.ServiceModel;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Perseverance.Concepts.Cognitions;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<MultiplexerModuleSettings>
    {
        private readonly IView traceView;
        private readonly IView knowledgeView;

        private ISynchronization synchronization;

        private TimeSpan         synchronizationTimer = TimeSpan.Zero;

        public MultiplexerModule(MultiplexerModuleSettings settings)
            : base(settings)
        {
            this.traceView     = new View(this.Settings.TraceViewSettings, this.Settings.TraceItemSettings, this.Settings.TraceViewFactory);
            this.knowledgeView = new View(this.Settings.KnowledgeViewSettings, this.Settings.KnowledgeItemSettings, this.Settings.KnowledgeViewFactory);
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
                this.DrawSynchronizationName(this.synchronization);
                this.DrawSynchronizationTime(this.synchronization);
            }
            
            this.DrawTrace(gameTime, alpha);
        }

        private void DrawTrace(GameTime gameTime, byte alpha)
        {
            this.traceView    .Draw(gameTime, alpha);
            this.knowledgeView.Draw(gameTime, alpha);
        }

        public override void Load()
        {
            foreach (var trace in Acutance.Adventure.Tracelist.Traces.ToArray())
            {
                this.traceView.Control.AddItem(trace);
            }
        }

        public override void Unload()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.traceView    .UpdateInput(gameTime);
            //this.knowledgeView.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            if (this.synchronizationTimer < TimeSpan.FromMilliseconds(100))
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
                catch (CommunicationObjectAbortedException)
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

            this.traceView    .UpdateStructure(gameTime);
            this.knowledgeView.UpdateStructure(gameTime);
        }

        private void DrawSynchronizationTime(ISynchronization synchronization)
        {
            FontManager.DrawCenteredText(
                this.Settings.SynchronizationTimeFont,
                string.Format("{0:hh\\:mm\\:ss}:{1}", synchronization.ElapsedTimeSinceTransition, synchronization.ElapsedTimeSinceTransition.Milliseconds.ToString("00")),
                this.SynchronizationTimeCenter,
                this.Settings.SynchronizationColor,
                this.Settings.SynchronizationTimeSize);
        }

        private void DrawSynchronizationName(ISynchronization synchronization)
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
                    this.Settings.SynchronizationNameFont,
                    task.Name.ToUpper(),
                    this.SynchronizationNameCenter,
                    this.Settings.SynchronizationColor,
                    this.Settings.SynchronizationNameSize);
            }
        }
    }
}