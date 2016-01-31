namespace MetaMind.Engine.Core.Entity.Control.Regions
{
    using System;

    public interface IRegionElement 
    {
        bool[] RegionStates { get; }

        Func<bool> this[RegionState state] { get; set; }
    }
}