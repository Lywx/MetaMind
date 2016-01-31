namespace MetaMind.Engine.Core.Backend.Content.Skins
{
    using Asset;
    using Microsoft.Xna.Framework;

    public class ControlSkinAsset : MMAsset
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

        public Point DefaultSize { get; set; }

        #endregion

    }
}