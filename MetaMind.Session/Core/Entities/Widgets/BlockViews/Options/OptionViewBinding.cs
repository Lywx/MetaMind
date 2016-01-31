namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using System;
    using System.Collections.Generic;
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Data;
    using Operations;

    public class OptionViewBinding : IMMViewBinding
    {
        private List<IOption> options;

        public OptionViewBinding(List<IOption> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            this.options = options;
        }

        public dynamic AddData(IMMViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IMMViewItem item)
        {
            return null;
        }

        public IReadOnlyList<object> AllData { get { return this.options; } }

        public void Bind()
        {
        }

        public void Unbind()
        {
        }
    }
}