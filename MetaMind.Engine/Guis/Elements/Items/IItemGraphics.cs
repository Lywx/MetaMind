// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MetaMind.Engine.Guis.Elements.Items
{
    using Microsoft.Xna.Framework;

    public interface IItemGraphics
    {
        #region Public Methods and Operators

        void Draw(GameTime gameTime, byte alpha);

        void Update(GameTime gameTime);

        #endregion
    }
}