namespace MetaMind.Engine.Components.Content.Texture
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MMImage
    {
        public MMImage(MMImageDesign design, Texture2D resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            this.Design = design;
            this.Resource = resource;
        }

        public MMImageDesign Design { get; private set; }

        public Texture2D Resource { get; private set; }
    }
}