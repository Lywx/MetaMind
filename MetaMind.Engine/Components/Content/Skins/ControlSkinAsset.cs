// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlSkinAsset.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.Content.Skins
{
    using Asset;
    using Geometry;

    public class ControlSkinAsset : Asset
    {
        #region Constructors

        /// <summary>
        /// Empty constructor for control skin.
        /// </summary>
        /// <param name="name"></param>
        public ControlSkinAsset(string name) : base(name)
        {
        }

        /// <summary>
        /// Copy constructor for control skin.
        /// </summary>
        /// <param name="source"></param>
        public ControlSkinAsset(ControlSkinAsset source)
            : base(source.Name)
        {
            this.Inherits = source.Inherits;

            this.DefaultSize = source.DefaultSize;
        }

        #endregion

        /// <summary>
        /// Control skin this may inherit from.
        /// </summary>
        public string Inherits { get; set; }

        #region Geometry Data

        public MMSize DefaultSize { get; set; }

        #endregion

    }
}