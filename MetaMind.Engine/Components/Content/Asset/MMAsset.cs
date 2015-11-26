namespace MetaMind.Engine.Components.Content.Asset
{
    using System;

    public abstract class MMAsset : MMObject
    {
        #region Constructors 

        protected MMAsset(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        #endregion

        public bool Archive { get; protected set; } = false;

        public string Name { get; set; }
    }
}