namespace MetaMind.Engine.Core.Entity.Control.Views.Layouts
{
    public interface IMMPointViewVerticalLayout : IMMViewLayout
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