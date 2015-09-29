namespace MetaMind.Engine.Components.Content.Texture
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;

    public class TextureManager : GameInputableComponent, ITextureManager
    {
        #region Font 

        public Texture2D this[string index] => this.Image[index];

        private Dictionary<string, Texture2D> Image { get; set; } = new Dictionary<string, Texture2D>();

        #endregion

        #region Constructors

        public TextureManager(GameEngine engine)
            : base(engine)
        {
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region Load and Unload

        protected override void LoadContent()
        {
        }

        public void Add(ImageAsset imageAsset)
        {
            if (!this.Image.ContainsKey(imageAsset.Name))
            {
                this.Image.Add(imageAsset.Name, imageAsset.Resource);
            }
        }

        public void Remove(ImageAsset imageAsset)
        {
            if (this.Image.ContainsKey(imageAsset.Name))
            {
                this.Image.Remove(imageAsset.Name);
            }
        }

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        protected override void UnloadContent()
        {
            this.Image.Clear();
        }

        #endregion Load and Unload

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.UnloadContent();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}