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
        public Image(ImageDesign design, Texture2D resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            this.Design = design;
            this.Resource = resource;
        }

        public ImageDesign Design { get; private set; }

        public Texture2D Resource { get; private set; }
    }
}