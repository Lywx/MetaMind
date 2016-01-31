namespace MetaMind.Engine.Core.Entity.Control.Item
{
    using System;
    using System.Linq;
    using Entity.Node.Model;
    using Microsoft.Xna.Framework;

    public abstract class MMViewItemStateHolder : MMNode
    {
        protected MMViewItemStateHolder()
        {
            for (var i = 0; i < (int)MMViewItemState.StateNum; i++)
            {
                this.itemStates[i] = () => false;
            }
        }

        #region States

        private readonly Func<bool>[] itemStates = new Func<bool>[(int)MMViewItemState.StateNum];

        public bool[] ItemStates
        {
            get
            {
                return this.itemStates.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[MMViewItemState state]
        {
            get
            {
                return this.itemStates[(int)state];
            }

            set
            {
                this.itemStates[(int)state] = value;
            }
        }

        #endregion States

        #region Update and Draw

        public abstract void UpdateView(GameTime time);

        #endregion Update and Draw
    }
}