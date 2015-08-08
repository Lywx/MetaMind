namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;

    public class OptionViewBinding : IViewBinding
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

        public dynamic AddData(IViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IViewItem item)
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