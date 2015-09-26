namespace MetaMind.Engine.Gui.Controls.Regions
{
    using System;

    public interface IRegionElement 
    {
        bool[] RegionStates { get; }

        Func<bool> this[RegionState state] { get; set; }
    }
}