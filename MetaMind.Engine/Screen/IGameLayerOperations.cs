namespace MetaMind.Engine.Screen
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IGameLayerOperations : IInputableOperations, IOuterUpdateableOperations, IInteroperableOperations 
    {
        void FadeIn(TimeSpan time);

        void FadeOut(TimeSpan time);

        void UpdateTransition(GameTime time);
    }
}