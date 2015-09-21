namespace MetaMind.Engine.Component.Content.Asset
{
    public abstract class Asset
    {
        #region Constructors 

        protected Asset()
        {
        }

        #endregion

        public string Name { get; set; }

        public bool Archive { get; protected set; } = false;

    }
}