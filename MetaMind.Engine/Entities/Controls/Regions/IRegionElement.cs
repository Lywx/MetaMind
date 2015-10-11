namespace MetaMind.Engine.Entities.Controls.Regions
{
    using System;

    public interface IRegionElement 
    {
        bool[] RegionStates { get; }

        Func<bool> this[RegionState state] { get; set; }
    }
}