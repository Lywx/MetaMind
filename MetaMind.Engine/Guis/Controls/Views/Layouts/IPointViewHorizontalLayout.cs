namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    public interface IPointViewHorizontalLayout : IViewLayout
    {
        int ColumnNum { get; }

        /// <summary>
        /// Zero based index item column number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int ColumnOf(int id);
    }
}