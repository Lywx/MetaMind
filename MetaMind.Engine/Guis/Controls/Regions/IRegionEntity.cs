namespace MetaMind.Engine.Guis.Controls.Regions
{
    using System;

    public interface IRegionEntity : IGameControllableEntity
    {
        bool[] States { get; }

        Func<bool> this[RegionState state] { get; set; }
    }
}