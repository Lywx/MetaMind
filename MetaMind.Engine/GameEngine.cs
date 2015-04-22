// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngine.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Consoles;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class GameEngine : Microsoft.Xna.Framework.Game, IGameEngine
    {
        public GameEngine()
        {
            // Graphics need to be constructed before Interop and Input 
            // for Interop.Screen relies on Graphics.SpriteBatch.
            this.Graphics = new GameEngineGraphics(this);

            this.Interop = new GameEngineInterop(this);
            this.Input   = new GameEngineInput(this);
            
            this.Numerical = new GameEngineNumerical();

            // Service is loaded after GameEngine.Initialize. But it has to 
            // be constructed after Components.
            Service = new GameEngineService(
                new GameEngineGraphicsService(this.Graphics),
                new GameEngineInputService(this.Input),
                new GameEngineInteropService(this.Interop),
                new GameEngineNumericalService(this.Numerical));

            this.Content.RootDirectory = "Content";
        }

        public static IGameService Service { get; private set; }

        public IGameInput Input { get; private set; }

        public IGameInterop Interop { get; private set; }

        public IGameGraphics Graphics { get; private set; }

        public IGameNumerical Numerical { get; private set; }

        protected override void Initialize()
        {
            this.Graphics.Initialize();

            this.Input    .Initialize();
            this.Interop  .Initialize();
            this.Numerical.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // FIXME: Need to contruct after FontManager LoadContent.
            // TODO:  It may be possible to change the underlying mechanism to move this code inside the contruction of GameEngine.Interop
            this.Interop.Console = new GameConsole(this, this.Graphics.SpriteBatch, this.Graphics.String, new GameConsoleOptions { Font = Font.UiConsole });
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            this.UpdateInput(gameTime);
            base.Update(gameTime);
        }

        protected void UpdateInput(GameTime gameTime)
        {
            // TODO: Playtest
            this.Input.UpdateInput(gameTime);

            // TODO: More
            this.Interop.UpdateInput(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            this.Interop.OnExiting();
            base.        OnExiting(sender, args);
        }
    }
}