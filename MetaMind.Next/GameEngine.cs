// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngine.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Next
{
    using Microsoft.Xna.Framework;
    
    public class GameEngine : Game
    {
        IGameInput Input { get; set; }
        
        IGameInterop Interop { get; set; }

        IGameRender Render { get; set; }

        public GameEngine()
        {
            this.Input   = new NullGameInput();
            this.Interop = new NullGameInterop();
            this.Render  = new NullGameRender();

            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Input  .Initialize();
            Interop.Initialize();
            Render .Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}