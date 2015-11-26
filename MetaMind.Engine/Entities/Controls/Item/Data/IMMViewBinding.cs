namespace MetaMind.Engine.Entities.Controls.Item.Data
{
    using System.Collections.Generic;

    public interface IMMViewBinding 
    {
        #region Data Delivery

        dynamic AddData(IMMViewItem item);

        dynamic RemoveData(IMMViewItem item);

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