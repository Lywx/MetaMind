namespace MetaMind.Engine.Components.Content.Asset
{
    using System;

    public abstract class Asset
    {
        #region Constructors 

        protected Asset(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        #endregion

        public string Name { get; set; }

        public bool Archive { get; protected set; } = false;
    }
}