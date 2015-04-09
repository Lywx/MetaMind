namespace MetaMind.Engine.Components
{
    using System;

    public interface IGameEngineComponent : IDisposable
    {
        /// <summary>
        /// Load the fonts from the content pipeline.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        void UnloadContent();
    }
}