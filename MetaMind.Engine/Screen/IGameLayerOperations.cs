namespace MetaMind.Engine.Screen
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IGameLayerOperations : IMMInputableOperations, IMMUpdateableOperations, IMMInteroperableOperations 
    {
        void FadeIn(TimeSpan time);

        void FadeOut(TimeSpan time);

        void UpdateTransition(GameTime time);
    }
}