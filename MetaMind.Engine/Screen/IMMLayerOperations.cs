namespace MetaMind.Engine.Screen
{
    using System;
    using Entities;
    using Microsoft.Xna.Framework;

    public interface IMMLayerOperations : IMMInputOperations, IMMUpdateableOperations, IMMInteropOperations 
    {
        void FadeIn(TimeSpan time);

        void FadeOut(TimeSpan time);

        void UpdateTransition(GameTime time);
    }
}