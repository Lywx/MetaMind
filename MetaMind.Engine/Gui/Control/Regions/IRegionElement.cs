namespace MetaMind.Engine.Gui.Control.Regions
{
    using System;

    public interface IRegionElement 
    {
        bool[] RegionStates { get; }

        Func<bool> this[RegionState state] { get; set; }
    }
}