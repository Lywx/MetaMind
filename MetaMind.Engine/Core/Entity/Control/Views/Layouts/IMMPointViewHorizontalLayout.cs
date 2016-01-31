namespace MetaMind.Engine.Core.Entity.Control.Views.Layouts
{
    public interface IMMPointViewHorizontalLayout : IMMViewLayout
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