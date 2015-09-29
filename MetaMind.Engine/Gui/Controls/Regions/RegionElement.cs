namespace MetaMind.Engine.Gui.Controls.Regions
{
    using System;
    using System.Linq;
    using Stateless;

    public abstract class RegionElement : GameInputableEntity, IRegionElement
    {
        protected RegionElement()
        {
            this.InitializeStates();
            this.InitializeMachine();
        }

        #region States

        private readonly Func<bool>[] regionStates = new Func<bool>[(int)RegionState.StateNum];

        public bool[] RegionStates { get { return this.regionStates.Select(state => state()).ToArray(); } }

        public Func<bool> this[RegionState state]
        {
            get
            {
                return this.regionStates[(int)state];
            }

            set
            {
                this.regionStates[(int)state] = value;
            }
        }

        #endregion

        #region State Machine

        protected enum RegionMachienState
        {
            HasFocus,
            LostFocus
        }

        protected enum RegionMachineTrigger
        {
            FocusInside,
            FocusOutside,
        }

        protected StateMachine<RegionMachienState, RegionMachineTrigger> RegionMachine { get; private set; }

        #endregion

        #region Initialization

        private void InitializeMachine()
        {
            this.RegionMachine = new StateMachine<RegionMachienState, RegionMachineTrigger>(RegionMachienState.LostFocus);

            this.RegionMachine.Configure(RegionMachienState.LostFocus).PermitReentry(RegionMachineTrigger.FocusOutside);
            this.RegionMachine.Configure(RegionMachienState.LostFocus).Permit(RegionMachineTrigger.FocusInside, RegionMachienState.HasFocus);

            this.RegionMachine.Configure(RegionMachienState.HasFocus).PermitReentry(RegionMachineTrigger.FocusInside);
            this.RegionMachine.Configure(RegionMachienState.HasFocus).Permit(RegionMachineTrigger.FocusOutside, RegionMachienState.LostFocus);
        }

        private void InitializeStates()
        {
            for (var i = 0; i < (int)RegionState.StateNum; i++)
            {
                this.regionStates[i] = () => false;
            }
        }

        #endregion
    }
}