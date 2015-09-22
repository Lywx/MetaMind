namespace MetaMind.Engine.Component.Content.Asset
{
    using System;
    using System.IO;
    using System.Xml;
    using File;
    using Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using NLog;
    using Texture;

    // TODO(Critical): Message / Dialog system
    // TODO(Critical): Test query system
    public class AssetManager : GameControllableComponent, IAssetManager
    {
        #region Logger

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Dependency

        protected ContentManager Content => this.Interop.Content;

        protected IFileManager File => this.Interop.File;

        #endregion

        #region Asset Data

        public IFontManager Fonts { get; private set; }

        public ITextureManager Texture { get; private set; }

        private AssetDictionary Assets { get; set; } = new AssetDictionary();

        #endregion

        #region Constructors

        public AssetManager(GameEngine engine) : base(engine)
        {
            this.Fonts   = new FontManager(engine);
            this.Texture = new TextureManager(engine);
        }

        #endregion

        #region Path

        private string AssetPath(string catalog, string asset) => Path.Combine(this.AssetCatalogPath(catalog), asset);

        private string AssetCatalogPath(string catalog) => this.File.ContentPath(catalog) + @"/";

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base        .Initialize();
            this.Fonts  .Initialize();
            this.Texture.Initialize();

            //for (int i = 0; i < this.Controls.Count; i++)
            //{
            //    for (int j = 0; j < this.Controls[i].Layers.Count; j++)
            //    {
            //        if (this.Controls[i].Layers[j].Image.Name != null)
            //        {
            //            this.Controls[i].Layers[j].Image = this.TextureAssets[this.Controls[i].Layers[j].Image.Name];
            //        }
            //        else
            //        {
            //            this.Controls[i].Layers[j].Image = this.TextureAssets[0];
            //        }

            //        if (this.Controls[i].Layers[j].Text.Name != null)
            //        {
            //            this.Controls[i].Layers[j].Text.Font = this.Fonts[this.Controls[i].Layers[j].Text.Name];
            //        }
            //        else
            //        {
            //            this.Controls[i].Layers[j].Text.Font = this.Fonts[0];
            //        }
            //    }
            //}
        }

        #endregion

        #region Load and Unload

        protected override void LoadContent()
        {
            this.LoadPackage("Engine.Persistent");

            base.LoadContent();
        }

        //private void LoadControls()
        //{
        //    if (this.Packages["Skin"]["Controls"] == null) return;

        //    XmlNodeList l = this.Packages["Skin"]["Controls"].GetElementsByTagName("Control");

        //    if (l != null && l.Count > 0)
        //    {
        //        foreach (XmlElement e in l)
        //        {
        //            SkinControl sc = null;
        //            string parent = this.ReadAttribute(e, "Inherits", null, false);
        //            bool inh = false;

        //            if (parent != null)
        //            {
        //                sc = new SkinControl(this.Controls[parent]);
        //                sc.Inherits = parent;
        //                inh = true;
        //            }
        //            else
        //            {
        //                sc = new SkinControl();
        //            }

        //            ReadAttribute(ref sc.Name, inh, e, "Name", null, true);

        //            ReadAttributeInt(ref sc.DefaultSize.Width, inh, e["DefaultSize"], "Width", 0, false);
        //            ReadAttributeInt(ref sc.DefaultSize.Height, inh, e["DefaultSize"], "Height", 0, false);

        //            ReadAttributeInt(ref sc.MinimumSize.Width, inh, e["MinimumSize"], "Width", 0, false);
        //            ReadAttributeInt(ref sc.MinimumSize.Height, inh, e["MinimumSize"], "Height", 0, false);

        //            this.ReadAttributeInt(ref sc.OriginMargins.Left, inh, e["OriginMargins"], "Left", 0, false);
        //            this.ReadAttributeInt(ref sc.OriginMargins.Top, inh, e["OriginMargins"], "Top", 0, false);
        //            this.ReadAttributeInt(ref sc.OriginMargins.Right, inh, e["OriginMargins"], "Right", 0, false);
        //            this.ReadAttributeInt(ref sc.OriginMargins.Bottom, inh, e["OriginMargins"], "Bottom", 0, false);

        //            this.ReadAttributeInt(ref sc.ClientMargins.Left, inh, e["ClientMargins"], "Left", 0, false);
        //            this.ReadAttributeInt(ref sc.ClientMargins.Top, inh, e["ClientMargins"], "Top", 0, false);
        //            this.ReadAttributeInt(ref sc.ClientMargins.Right, inh, e["ClientMargins"], "Right", 0, false);
        //            this.ReadAttributeInt(ref sc.ClientMargins.Bottom, inh, e["ClientMargins"], "Bottom", 0, false);

        //            this.ReadAttributeInt(ref sc.ResizerSize, inh, e["ResizerSize"], "Value", 0, false);

        //            if (e["Layers"] != null)
        //            {
        //                XmlNodeList l2 = e["Layers"].GetElementsByTagName("Layer");
        //                if (l2 != null && l2.Count > 0)
        //                {
        //                    this.LoadLayers(sc, l2);
        //                }
        //            }

        //            if (e["Attributes"] != null)
        //            {
        //                XmlNodeList l3 = e["Attributes"].GetElementsByTagName("Attribute");
        //                if (l3 != null && l3.Count > 0)
        //                {
        //                    this.LoadControlAttributes(sc, l3);
        //                }
        //            }

        //            this.Controls.Add(sc);
        //        }
        //    }
        //}

        //private void LoadLayers(SkinControl sc, XmlNodeList l)
        //{
        //    foreach (XmlElement e in l)
        //    {
        //        string name = this.ReadAttribute(e, "Name", null, true);
        //        bool over = this.ReadAttributeBool(e, "Override", false, false);
        //        SkinLayer sl = sc.Layers[name];
        //        bool inh = true;

        //        if (sl == null)
        //        {
        //            sl = new SkinLayer();
        //            inh = false;
        //        }

        //        if (inh && over)
        //        {
        //            sl = new SkinLayer();
        //            sc.Layers[name] = sl;
        //        }

        //        ReadAttribute(ref sl.Name, inh, e, "Name", null, true);
        //        ReadAttribute(ref sl.Image.Name, inh, e, "Image", "Control", false);
        //        ReadAttributeInt(ref sl.Width, inh, e, "Width", 0, false);
        //        ReadAttributeInt(ref sl.Height, inh, e, "Height", 0, false);

        //        string temp = sl.Alignment.ToString();
        //        this.ReadAttribute(ref temp, inh, e, "Alignment", "MiddleCenter", false);
        //        sl.Alignment = (Alignment)Enum.Parse(typeof(Alignment), temp, true);

        //        ReadAttributeInt(ref sl.OffsetX, inh, e, "OffsetX", 0, false);
        //        ReadAttributeInt(ref sl.OffsetY, inh, e, "OffsetY", 0, false);

        //        ReadAttributeInt(ref sl.SizingMargins.Left, inh, e["SizingMargins"], "Left", 0, false);
        //        ReadAttributeInt(ref sl.SizingMargins.Top, inh, e["SizingMargins"], "Top", 0, false);
        //        ReadAttributeInt(ref sl.SizingMargins.Right, inh, e["SizingMargins"], "Right", 0, false);
        //        ReadAttributeInt(ref sl.SizingMargins.Bottom, inh, e["SizingMargins"], "Bottom", 0, false);

        //        ReadAttributeInt(ref sl.ContentMargins.Left, inh, e["ContentMargins"], "Left", 0, false);
        //        ReadAttributeInt(ref sl.ContentMargins.Top, inh, e["ContentMargins"], "Top", 0, false);
        //        ReadAttributeInt(ref sl.ContentMargins.Right, inh, e["ContentMargins"], "Right", 0, false);
        //        ReadAttributeInt(ref sl.ContentMargins.Bottom, inh, e["ContentMargins"], "Bottom", 0, false);

        //        if (e["States"] != null)
        //        {
        //            ReadAttributeInt(ref sl.States.Enabled.Index, inh, e["States"]["Enabled"], "Index", 0, false);
        //            int di = sl.States.Enabled.Index;
        //            ReadAttributeInt(ref sl.States.Hovered.Index, inh, e["States"]["Hovered"], "Index", di, false);
        //            ReadAttributeInt(ref sl.States.Pressed.Index, inh, e["States"]["Pressed"], "Index", di, false);
        //            ReadAttributeInt(ref sl.States.Focused.Index, inh, e["States"]["Focused"], "Index", di, false);
        //            ReadAttributeInt(ref sl.States.Disabled.Index, inh, e["States"]["Disabled"], "Index", di, false);

        //            ReadAttributeColor(ref sl.States.Enabled.Color, inh, e["States"]["Enabled"], "Color", Color.White, false);
        //            Color dc = sl.States.Enabled.Color;
        //            ReadAttributeColor(ref sl.States.Hovered.Color, inh, e["States"]["Hovered"], "Color", dc, false);
        //            ReadAttributeColor(ref sl.States.Pressed.Color, inh, e["States"]["Pressed"], "Color", dc, false);
        //            ReadAttributeColor(ref sl.States.Focused.Color, inh, e["States"]["Focused"], "Color", dc, false);
        //            ReadAttributeColor(ref sl.States.Disabled.Color, inh, e["States"]["Disabled"], "Color", dc, false);

        //            ReadAttributeBool(ref sl.States.Enabled.Overlay, inh, e["States"]["Enabled"], "Overlay", false, false);
        //            bool dv = sl.States.Enabled.Overlay;
        //            ReadAttributeBool(ref sl.States.Hovered.Overlay, inh, e["States"]["Hovered"], "Overlay", dv, false);
        //            ReadAttributeBool(ref sl.States.Pressed.Overlay, inh, e["States"]["Pressed"], "Overlay", dv, false);
        //            ReadAttributeBool(ref sl.States.Focused.Overlay, inh, e["States"]["Focused"], "Overlay", dv, false);
        //            ReadAttributeBool(ref sl.States.Disabled.Overlay, inh, e["States"]["Disabled"], "Overlay", dv, false);
        //        }

        //        if (e["Overlays"] != null)
        //        {
        //            ReadAttributeInt(ref sl.Overlays.Enabled.Index, inh, e["Overlays"]["Enabled"], "Index", 0, false);
        //            int di = sl.Overlays.Enabled.Index;
        //            ReadAttributeInt(ref sl.Overlays.Hovered.Index, inh, e["Overlays"]["Hovered"], "Index", di, false);
        //            ReadAttributeInt(ref sl.Overlays.Pressed.Index, inh, e["Overlays"]["Pressed"], "Index", di, false);
        //            ReadAttributeInt(ref sl.Overlays.Focused.Index, inh, e["Overlays"]["Focused"], "Index", di, false);
        //            ReadAttributeInt(ref sl.Overlays.Disabled.Index, inh, e["Overlays"]["Disabled"], "Index", di, false);

        //            ReadAttributeColor(ref sl.Overlays.Enabled.Color, inh, e["Overlays"]["Enabled"], "Color", Color.White, false);
        //            Color dc = sl.Overlays.Enabled.Color;
        //            ReadAttributeColor(ref sl.Overlays.Hovered.Color, inh, e["Overlays"]["Hovered"], "Color", dc, false);
        //            ReadAttributeColor(ref sl.Overlays.Pressed.Color, inh, e["Overlays"]["Pressed"], "Color", dc, false);
        //            ReadAttributeColor(ref sl.Overlays.Focused.Color, inh, e["Overlays"]["Focused"], "Color", dc, false);
        //            ReadAttributeColor(ref sl.Overlays.Disabled.Color, inh, e["Overlays"]["Disabled"], "Color", dc, false);
        //        }

        //        if (e["Text"] != null)
        //        {
        //            ReadAttribute(ref sl.Text.Name, inh, e["Text"], "Font", null, true);
        //            ReadAttributeInt(ref sl.Text.OffsetX, inh, e["Text"], "OffsetX", 0, false);
        //            ReadAttributeInt(ref sl.Text.OffsetY, inh, e["Text"], "OffsetY", 0, false);

        //            temp = sl.Text.Alignment.ToString();
        //            this.ReadAttribute(ref temp, inh, e["Text"], "Alignment", "MiddleCenter", false);
        //            sl.Text.Alignment = (Alignment)Enum.Parse(typeof(Alignment), temp, true);

        //            LoadColors(inh, e["Text"], ref sl.Text.Colors);
        //        }
        //        if (e["Attributes"] != null)
        //        {
        //            XmlNodeList l2 = e["Attributes"].GetElementsByTagName("Attribute");
        //            if (l2 != null && l2.Count > 0)
        //            {
        //                this.LoadLayerAttributes(sl, l2);
        //            }
        //        }
        //        if (!inh) sc.Layers.Add(sl);
        //    }
        //}

        //private void LoadColors(bool inherited, XmlElement e, ref SkinStates<Color> colors)
        //{
        //    if (e != null)
        //    {
        //        ReadAttributeColor(ref colors.Enabled, inherited, e["Colors"]["Enabled"], "Color", Color.White, false);
        //        ReadAttributeColor(ref colors.Hovered, inherited, e["Colors"]["Hovered"], "Color", colors.Enabled, false);
        //        ReadAttributeColor(ref colors.Pressed, inherited, e["Colors"]["Pressed"], "Color", colors.Enabled, false);
        //        ReadAttributeColor(ref colors.Focused, inherited, e["Colors"]["Focused"], "Color", colors.Enabled, false);
        //        ReadAttributeColor(ref colors.Disabled, inherited, e["Colors"]["Disabled"], "Color", colors.Enabled, false);
        //    }
        //}

        //private void LoadControlAttributes(SkinControl sc, XmlNodeList l)
        //{
        //    foreach (XmlElement e in l)
        //    {
        //        string name = this.ReadAttribute(e, "Name", null, true);
        //        SkinAttribute sa = sc.Attributes[name];
        //        bool inh = true;

        //        if (sa == null)
        //        {
        //            sa = new SkinAttribute();
        //            inh = false;
        //        }

        //        sa.Name = name;
        //        ReadAttribute(ref sa.Value, inh, e, "Value", null, true);

        //        if (!inh) sc.Attributes.Add(sa);
        //    }
        //}

        //private void LoadLayerAttributes(SkinLayer sl, XmlNodeList l)
        //{
        //    foreach (XmlElement e in l)
        //    {
        //        string name = this.ReadAttribute(e, "Name", null, true);
        //        SkinAttribute sa = sl.Attributes[name];
        //        bool inh = true;

        //        if (sa == null)
        //        {
        //            sa = new SkinAttribute();
        //            inh = false;
        //        }

        //        sa.Name = name;
        //        ReadAttribute(ref sa.Value, inh, e, "Value", null, true);

        //        if (!inh) sl.Attributes.Add(sa);
        //    }
        //}

        #endregion

        #region Load and Unload Package

        public void LoadPackage(string packageName, bool async = false)
        {
            this.ReadPackage(packageName);

            foreach (var font in this.Assets[packageName].Fonts.Values)
            {
                this.LoadFont(font, async);
            }

            foreach (var texture in this.Assets[packageName].Texture.Values)
            {
                this.LoadImage(texture, async);
            }
        }

        public void UnloadPackage(string packageName)
        {
            foreach (var font in this.Assets[packageName].Fonts.Values)
            {
                this.UnloadFont(font);
            }

            foreach (var texture in this.Assets[packageName].Texture.Values)
            {
                this.UnloadImage(texture);
            }
        }

        #endregion

        #region Load and Unload Font  

        private void LoadFont(FontAsset font, bool async = false)
        {
            var spriteFontPath = this.AssetPath("Fonts", font.Asset);

            if (async)
            {
                this.Content.LoadAsync<SpriteFont>(
                    spriteFontPath,
                    spriteFont => font.Resource = spriteFont);
            }
            else
            {
                font.Resource = this.Content.Load<SpriteFont>(spriteFontPath);
            }

            this.Fonts.Add(font);
        }

        private void UnloadFont(FontAsset font)
        {
            this.Assets.GetFont(font.Name).Resource = null;
            this.Fonts.Remove(font);
        }

        #endregion

        #region Load and Unload Image

        private void LoadImage(ImageAsset image, bool async = false)
        {
            var texturePath = this.AssetPath("Texture", image.Asset);

            if (async)
            {
                this.Content.LoadAsync<Texture2D>(
                    texturePath,
                    texture2D => image.Resource = texture2D);

            }
            else
            {
                image.Resource = this.Content.Load<Texture2D>(texturePath);
            }

            this.Texture.Add(image);
        }

        private void UnloadImage(ImageAsset image)
        {
            this.Assets.GetTexture(image.Name).Resource = null;
            this.Texture.Remove(image);
        }

        #endregion

        #region XML Package Operations

        public void ReadPackage(string packageName)
        {
            if (this.Assets.ContainsKey(packageName))
            {
                return;
            }

            var packageDocument = this.Content.Load<PackageXmlDocument>(packageName);

            try
            {
                var e = packageDocument["Package"];
                if (e != null)
                {
                    var package = new PackageAsset(packageName);
                    this.Assets.Add(packageName, package);

                    this.ReadFonts(package, e);
                    this.ReadImages(package, e);
                }
                else
                {
                    logger.Error($@"Unable to load package file: cannot find ""Package"" element in {packageName}.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to load package file. {e.Message}");
            }
        }

        private void ReadFonts(PackageAsset package, XmlElement packageElement)
        {
            var fontList = this.ReadAssetList(packageElement, "Fonts", "Font");
            if (fontList?.Count > 0)
            {
                foreach (XmlElement fontElement in fontList)
                {
                    var fontName  = this.ReadAttribute(fontElement, "Name", null, true);
                    var fontSize  = this.ReadAttribute(fontElement, "Size", null, true);
                    var fontAsset = this.ReadAttribute(fontElement, "Asset", null, true);

                    var font = new FontAsset(fontName, int.Parse(fontSize), fontAsset);
                    if (!package.Fonts.ContainsKey(font.Name))
                    {
                        package.Add(font);
                    }
                }
            }
        }

        private void ReadImages(PackageAsset package, XmlElement packageElement)
        {
            var imageList = this.ReadAssetList(packageElement, "Texture", "Image");
            if (imageList?.Count > 0)
            {
                foreach (XmlElement imageElement in imageList)
                {
                    var imageName = this.ReadAttribute(imageElement, "Name", null, true);
                    var imageAsset = this.ReadAttribute(imageElement, "Asset", null, true);

                    var image = new ImageAsset(imageName, imageAsset);

                    if (!package.Texture.ContainsKey(image.Name))
                    {
                        package.Add(image);
                    }
                }
            }
        }

        private XmlNodeList ReadAssetList(XmlElement contentElement, string catalogName, string itemName)
        {
            var catalogElement = contentElement[catalogName];
            if (catalogElement == null)
            {
                logger.Warn($@"Failed to load catalog ""{catalogName}""");

                return null;
            }

            var itemList = catalogElement.GetElementsByTagName(itemName);
            return itemList;
        }

        #endregion

        #region XML Universal Operations

        private string ReadAttribute(XmlElement element, string attribute, string defaultValue, bool needed)
        {
            if (element != null && element.HasAttribute(attribute))
            {
                return element.Attributes[attribute].Value;
            }
            else if (needed)
            {
                throw new Exception("Missing required attribute \"" + attribute + "\" in the file.");
            }

            return defaultValue;
        }
       
        private void ReadAttribute(ref string returnValue, bool inherited, XmlElement element, string attribute, string defaultValue, bool needed)
        {
            if (element != null && element.HasAttribute(attribute))
            {
                returnValue = element.Attributes[attribute].Value;
            }
            else if (inherited)
            {
            }
            else if (needed)
            {
                throw new Exception("Missing required attribute \"" + attribute + "\" in the skin file.");
            }
            else
            {
                returnValue = defaultValue;
            }
        }

        private int ReadAttributeInt(XmlElement element, string attribute, int defaultValue, bool needed)
        {
            return int.Parse(this.ReadAttribute(element, attribute, defaultValue.ToString(), needed));
        }
   
        private void ReadAttributeInt(ref int returnValue, bool inherited, XmlElement element, string attribute, int defaultValue, bool needed)
        {
            string temp = returnValue.ToString();
            this.ReadAttribute(ref temp, inherited, element, attribute, defaultValue.ToString(), needed);
            returnValue = int.Parse(temp);
        }
   
        private bool ReadAttributeBool(XmlElement element, string attribute, bool defaultValue, bool needed)
        {
            return bool.Parse(this.ReadAttribute(element, attribute, defaultValue.ToString(), needed));
        }
   
        private void ReadAttributeBool(ref bool returnValue, bool inherited, XmlElement element, string attribute, bool defaultValue, bool needed)
        {
            string temp = returnValue.ToString();
            this.ReadAttribute(ref temp, inherited, element, attribute, defaultValue.ToString(), needed);
            returnValue = bool.Parse(temp);
        }
      
        private byte ReadAttributeByte(XmlElement element, string attribute, byte defaultValue, bool needed)
        {
            return byte.Parse(this.ReadAttribute(element, attribute, defaultValue.ToString(), needed));
        }

        private void ReadAttributeByte(ref byte returnValue, bool inherited, XmlElement element, string attribute, byte defaultValue, bool needed)
        {
            var temp = returnValue.ToString();
            this.ReadAttribute(ref temp, inherited, element, attribute, defaultValue.ToString(), needed);
            returnValue = byte.Parse(temp);
        }

        private void ReadAttributeColor(ref Color returnValue, bool inherited, XmlElement element, string attribute, Color defaultValue, bool needed)
        {
            var temp = ColorUtils.ToString(returnValue);
            this.ReadAttribute(ref temp, inherited, element, attribute, ColorUtils.ToString(defaultValue), needed);
            returnValue = ColorUtils.Parse(temp);
        }

        #endregion

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
                        this.Fonts?.Dispose();
                        this.Fonts = null;
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
