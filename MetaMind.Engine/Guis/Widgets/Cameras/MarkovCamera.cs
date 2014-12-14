// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarkovCamera.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Cameras
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public interface IMarkovCamera : IWidget
    {
        Vector2 Movement { get; set; }
    }

    public class MarkovCamera : Widget, IMarkovCamera
    {
        private readonly CameraSettings settings;

        public MarkovCamera(CameraSettings settings)
        {
            this.settings = settings;
        }

        public Vector2 Movement { get; set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            var mouse = InputSequenceManager.Mouse.CurrentState;
            var keyboard = InputSequenceManager.Keyboard.CurrentState;

            var identityMovement = Vector2.Zero;
            identityMovement = AddMovementFromKeyboardInput(keyboard, identityMovement);
            identityMovement = AddMovementFromMouse(mouse, identityMovement);

            Movement = FinalMovement(identityMovement);
        }

        public override void UpdateStructure(GameTime gameTime)
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
                && !(mouse.Y > GraphicsSettings.Height - settings.PanForbiddenHeight))
            {
                identityMovement.X++;
            }

            if (mouse.X > GraphicsSettings.Width - settings.PanRegionWidth && !(mouse.Y < settings.PanForbiddenHeight)
                && !(mouse.Y > GraphicsSettings.Height - settings.PanForbiddenHeight))
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