// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarkovCamera.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Cameras
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public interface IMarkovCamera : IInputable
    {
        Vector2 Movement { get; set; }
    }

    public class MarkovCamera : GameControllableEntity, IMarkovCamera
    {
        private readonly MarkovCameraSettings settings;

        public MarkovCamera(MarkovCameraSettings settings)
        {
            this.settings = settings;
        }

        public Vector2 Movement { get; set; }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            var mouse    = gameInput.State.Mouse.CurrentState;
            var keyboard = gameInput.State.Keyboard.CurrentState;

            var identityMovement = Vector2.Zero;
            identityMovement = AddMovementFromKeyboardInput(keyboard, identityMovement);
            identityMovement = AddMovementFromMouse(mouse, identityMovement);

            Movement = FinalMovement(identityMovement);
        }

        public override void Update(GameTime gameTime)
        {
        }

        private Vector2 AddMovementFromKeyboardInput(KeyboardState keyboard, Vector2 identityMovement)
        {
            if (keyboard.IsKeyDown(Keys.Left))
            {
                identityMovement.X++;
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                identityMovement.X--;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                identityMovement.Y--;
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                identityMovement.Y--;
            }

            return identityMovement;
        }

        private Vector2 AddMovementFromMouse(MouseState mouse, Vector2 identityMovement)
        {
            // allow movement when mouse is on sides
            // forbid movement when mouse is at corners
            if (mouse.X < settings.PanRegionWidth && !(mouse.Y < settings.PanForbiddenHeight)
                && !(mouse.Y > GameEngine.GraphicsSettings.Height - settings.PanForbiddenHeight))
            {
                identityMovement.X++;
            }

            if (mouse.X > GameEngine.GraphicsSettings.Width - settings.PanRegionWidth && !(mouse.Y < settings.PanForbiddenHeight)
                && !(mouse.Y > GameEngine.GraphicsSettings.Height - settings.PanForbiddenHeight))
            {
                identityMovement.X--;
            }

            return identityMovement;
        }

        private Vector2 FinalMovement(Vector2 identityMovement)
        {
            if (identityMovement.Length() > 1)
            {
                identityMovement.Normalize();
            }

            return settings.PanVelocity * identityMovement;
        }
    }
}