namespace MetaMind.Engine.Guis.Widgets.ViewItems
{

    public enum ViewItemLabelType
    {
        //------------------------------------------------------------------
        Name,

        //------------------------------------------------------------------
        LabelNum,
    }

    public static class ViewItemLabelTypeExtension
    {
        public static string GetLabelFrom( this ViewItemLabelType type, IViewItemData data )
        {
            return data.Labels[ ( int ) type ];
        }
    }
}