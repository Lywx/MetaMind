namespace MetaMind.Engine.Guis.Controls.Regions
{
    using System;
    using System.Linq;

    public abstract class RegionEntity : GameControllableEntity, IRegionEntity
    {
        #region States

        private readonly Func<bool>[] states = new Func<bool>[(int)RegionState.StateNum];

        public bool[] States { get { return this.states.Select(state => state()).ToArray(); } }

        protected RegionEntity()
        {
            for (var i = 0; i < (int)RegionState.StateNum; i++)
            {
                this.states[i] = () => false;
            }
        }

        public Func<bool> this[RegionState state]
        {
            get
            {
                return this.states[(int)state];
            }

            set
            {
                this.states[(int)state] = value;
            }
        }

        #endregion States
    }
}