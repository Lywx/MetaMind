namespace MetaMind.Engine.Guis.Controls.Items.Data
{
    using System.Collections.Generic;

    public interface IViewBinding 
    {
        #region Data Delivery

        dynamic AddData(IViewItem item);

        dynamic RemoveData(IViewItem item);

        /// <summary>
        /// Gets the covariant list.
        /// </summary>
        IReadOnlyList<object> AllData { get; }

        #endregion

        #region

        /// <summary>
        /// Bind the view related events.
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbind the view related events and provide rearranging of the processed data.
        /// </summary>
        void Unbind();

        #endregion
    }
}