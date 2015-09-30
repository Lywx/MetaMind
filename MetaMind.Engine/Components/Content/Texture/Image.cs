// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Image.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.Content.Texture
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class Image
    {
        public Image(Designer designer, Texture2D resource)
        {
            if (designer == null)
            {
                throw new ArgumentNullException(nameof(designer));
            }


            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            this.Designer = designer;
            this.Resource = resource;
        }

        public Designer Designer { get; private set; }

        public Texture2D Resource { get; private set; }
    }
}