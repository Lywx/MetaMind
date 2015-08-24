namespace MetaMind.Engine.Guis.Layers
{
    using System;

    public interface ICircularLayerOperations
    {
        void Next();

        void Next(TimeSpan time);

        void Previous();

        void Previous(TimeSpan time);
    }
}