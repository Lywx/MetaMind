namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    public interface IPointViewSelectionControl
    {
        bool HasSelected { get; }

        bool HasPreviouslySelected { get; }

        int? SelectedId { get; }

        int? PreviousSelectedId { get; }

        void Clear();

        bool IsSelected(int id);

        /// <summary>
        /// Selects the specified id.
        /// </summary>
        /// <remarks>
        /// All the selection has to be done by this function to enforce uniformity.
        /// </remarks>
        void Select(int id);
    }
}