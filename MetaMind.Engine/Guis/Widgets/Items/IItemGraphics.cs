// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IItemGraphics.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MetaMind.Engine.Guis.Widgets.Items
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