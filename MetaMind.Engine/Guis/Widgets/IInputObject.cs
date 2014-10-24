using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets
{
    public interface IInputObject
    {
        bool IsHandlingInput { get; }

        bool IsUpdating { get; }

        void Draw(GameTime gameTime);

        /// <summary>
        /// Trigger to update the input part of updating.
        /// </summary>
        void HandleInput();

        /// <summary>
        /// Handles the input part of updating.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void UpdateInput(GameTime gameTime);

        /// <summary>
        /// Handles the structure part of updating.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void UpdateStructure(GameTime gameTime);

        void Update(GameTime gameTime);
    }
}