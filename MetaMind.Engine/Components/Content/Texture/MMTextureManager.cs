namespace MetaMind.Engine.Components.Content.Texture
{
    using System.Collections.Generic;

    public class MMTextureManager : MMInputableComponent, IMMTextureManager
    {
        #region Font 

        public MMImage this[string index] => this.Image[index];

        private Dictionary<string, MMImage> Image { get; set; } = new Dictionary<string, MMImage>();

        #endregion

        #region Constructors

        public MMTextureManager(MMEngine engine)
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

        public void Add(MMImageAsset imageAsset)
        {
            if (!this.Image.ContainsKey(imageAsset.Name))
            {
                this.Image.Add(imageAsset.Name, imageAsset.ToImage());
            }
        }

        public void Remove(MMImageAsset imageAsset)
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