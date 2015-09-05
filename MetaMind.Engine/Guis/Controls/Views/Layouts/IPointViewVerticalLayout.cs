namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    public interface IPointViewVerticalLayout : IViewLayout
    {
        int RowNum { get; }

        /// <summary>
        /// Zero based index item row number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int RowOf(int id);

        int RowIn(int id);
    }
}