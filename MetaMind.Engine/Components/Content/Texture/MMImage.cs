namespace MetaMind.Engine.Components.Content.Texture
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MMImage
    {
        public MMImage(MMImageSettings settings, Texture2D resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            this.Settings = settings;
            this.Resource = resource;
        }

        public MMImageSettings Settings { get; private set; }

        public Texture2D Resource { get; private set; }
    }
}