namespace MetaMind.Engine.Gui.Control.Skin
{
    public abstract class SkinElement
    {
        public string Name;

        public bool Archive = false;

        #region Constructors 

        public SkinElement()
        {
        }

        public SkinElement(SkinElement source)
        {
            if (source == null)
            {
                return;
            }

            this.Archive = source.Archive;
            this.Name    = source.Name;
        }

        #endregion
    }
}