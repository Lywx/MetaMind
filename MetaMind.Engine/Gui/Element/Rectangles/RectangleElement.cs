// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Element.Rectangles
{
    using System;
    using System.Linq;
    using Control;

    public abstract class RectangleElement : ControlComponent
    {
        #region Constructors and Finalizer

        protected RectangleElement()
        {
            this.InitializeStates();
        }

        #endregion Constructors and Destructors

        #region State Data

        /// TODO(Minor): Enable read-only setter by implementing a permission array.
        /// TODO(Minor): Migrate some advanced states to fields and properties.
        /// <summary>
        /// Frame states as Func<bool> to replace messy things like active, 
        /// visible. In order to enable logic passing, I decided to make them 
        /// Func<bool>.
        /// </summary>
        private readonly Func<bool>[] frameStates = new Func<bool>[(int)ElementState.StateNum];

        internal bool[] FrameStates => this.frameStates.Select(state => state()).ToArray();

        public Func<bool> this[ElementState state]
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
            for (var i = 0; i < (int)ElementState.StateNum; i++)
            {
                this.frameStates[i] = () => false;
            }
        }

        #endregion State Data

        #region Initialization

        #endregion
    }
}