namespace MetaMind.Engine.Components.Content.Fonts
{
    using System.Collections.Generic;
    using Extensions;

    /// <summary>
    ///     Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class MMFontManager : MMInputableComponent, IMMFontManager
    {
        #region Constructors

        public MMFontManager(MMEngine engine)
            : base(engine) {}

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            StringUtils.Initialize(this);
        }

        #endregion

        #region Font 

        public MMFont this[string index] => this.Fonts[index];

        private Dictionary<string, MMFont> Fonts { get; } =
            new Dictionary<string, MMFont>();

        #endregion

        #region Load and Unload

        protected override void LoadContent() {}

        public void Add(MMFontAsset fontAsset)
        {
            if (!this.Fonts.ContainsKey(fontAsset.Name))
            {
                this.Fonts.Add(fontAsset.Name, new MMFont(fontAsset));
            }
        }

        public void Remove(MMFontAsset fontAsset)
        {
            if (this.Fonts.ContainsKey(fontAsset.Name))
            {
                this.Fonts.Remove(fontAsset.Name);
            }
        }

        /// <summary>
        ///     Release all references to the fonts.
        /// </summary>
        protected override void UnloadContent()
        {
            this.Fonts.Clear();
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
