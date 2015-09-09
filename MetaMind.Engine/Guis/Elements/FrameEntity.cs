// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;
    using System.Linq;

    public abstract class FrameEntity : GameControllableEntity, IFrameEntity
    {
        #region Constructors and Finalizer

        protected FrameEntity()
        {
            this.InitializeStates();
        }

        #endregion Constructors and Destructors

        #region State Data

        /// TODO(Wuxiang): Enable read-only setter by implementing a permission array.
        /// <summary>
        /// Frame states as Func<bool> to replace messy things like active, 
        /// visible. In order to enable logic passing, I decided to make them 
        /// Func<bool>.
        /// </summary>
        private readonly Func<bool>[] frameStates = new Func<bool>[(int)FrameState.StateNum];

        internal bool[] FrameStates => this.frameStates.Select(state => state()).ToArray();

        public Func<bool> this[FrameState state]
        {
            get
            {
                return this.frameStates[(int)state];
            }

            protected set
            {
                this.frameStates[(int)state] = value;
            }
        }

        private void InitializeStates()
        {
            for (var i = 0; i < (int)FrameState.StateNum; i++)
            {
                this.frameStates[i] = () => false;
            }
        }

        #endregion State Data
    }
}