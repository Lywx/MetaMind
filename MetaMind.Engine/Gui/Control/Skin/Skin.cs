namespace MetaMind.Engine.Gui.Control.Skin
{
    using System;
    using System.IO;
    using System.Xml;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Skin : ControlComponent
    {
        #region Dependency

        protected ContentManager content => this.Interop.Content;

        #endregion

        SkinXmlDocument doc = null;

        private string name = null;

        private Version version = null;

        private SkinInformation info;

        private SkinList<SkinControl> controls = null;

        private SkinList<SkinFont> fonts = null;

        private SkinList<SkinCursor> cursors = null;

        private SkinList<SkinImage> images = null;

        public virtual string Name { get { return this.name; } }

        public virtual Version Version { get { return this.version; } }

        public virtual SkinInformation Info { get { return this.info; } }

        public virtual SkinList<SkinControl> Controls { get { return this.controls; } }

        public virtual SkinList<SkinFont> Fonts { get { return this.fonts; } }

        public virtual SkinList<SkinCursor> Cursors { get { return this.cursors; } }

        public virtual SkinList<SkinImage> Images { get { return this.images; } }
      
        #region Constructors
     
        public Skin(ControlManager manager, string name)
            : base(manager)
        {
            this.name = name;

            this.content = new ContentManager(this.Manager.Game.Services, this.GetArchiveLocation(name + this.Manager.SkinExtension));
            this.content.RootDirectory = this.GetFolder();

            this.doc = new SkinXmlDocument();
            this.controls = new SkinList<SkinControl>();
            this.fonts = new SkinList<SkinFont>();
            this.images = new SkinList<SkinImage>();
            this.cursors = new SkinList<SkinCursor>();

            this.LoadSkin(null, this.content.UseArchive);

            string folder = this.GetAddonsFolder();
            if (folder == "")
            {
                this.content.UseArchive = true;
                folder = "Addons\\";
            }
            else
            {
                this.content.UseArchive = false;
            }

            string[] addons = this.content.GetDirectories(folder);

            if (addons != null && addons.Length > 0)
            {
                for (int i = 0; i < addons.Length; i++)
                {
                    DirectoryInfo d = new DirectoryInfo(this.GetAddonsFolder() + addons[i]);
                    if (!((d.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) || this.content.UseArchive)
                    {
                        this.LoadSkin(addons[i].Replace("\\", ""), this.content.UseArchive);
                    }
                }
            }
        }

        #endregion

        #region //// Destructors ///////

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.content != null)
                {
                    this.content.Unload();
                    this.content.Dispose();
                    this.content = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region //// Methods ///////////

        private string GetArchiveLocation(string name)
        {
            string path = Path.GetFullPath(this.Manager.SkinDirectory) + Path.GetFileNameWithoutExtension(name) + "\\";
            if (!Directory.Exists(path) || !File.Exists(path + "Skin.xnb"))
            {
                path = Path.GetFullPath(this.Manager.SkinDirectory) + name;
                return path;
            }

            return null;
        }

        private string GetFolder()
        {
            string path = Path.GetFullPath(this.Manager.SkinDirectory) + this.name + "\\";
            if (!Directory.Exists(path) || !File.Exists(path + "Skin.xnb"))
            {
                path = "";
            }

            return path;
        }
   
        private string GetAddonsFolder()
        {
            string path = Path.GetFullPath(this.Manager.SkinDirectory) + this.name + "\\Addons\\";
            if (!Directory.Exists(path))
            {
                path = Path.GetFullPath(".\\Content\\Skins\\") + this.name + "\\Addons\\";
                if (!Directory.Exists(path))
                {
                    path = Path.GetFullPath(".\\Skins\\") + this.name + "\\Addons\\";
                }
            }

            return path;
        }
   
        private string GetFolder(string type)
        {
            return this.GetFolder() + type + "\\";
        }

        private string GetAsset(string type, string asset, string addon)
        {
            string ret = this.GetFolder(type) + asset;
            if (addon != null && addon != "")
            {
                ret = this.GetAddonsFolder() + addon + "\\" + type + "\\" + asset;
            }
            return ret;
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < this.fonts.Count; i++)
            {
                this.content.UseArchive = this.fonts[i].Archive;
                string asset = this.GetAsset("Fonts", this.fonts[i].Asset, this.fonts[i].Addon);
                asset = this.content.UseArchive ? asset : Path.GetFullPath(asset);

                (this.fonts[i].Resource) = this.content.Load<SpriteFont>(asset);
            }

#if (!XBOX && !XBOX_FAKE)
            for (int i = 0; i < this.cursors.Count; i++)
            {
                this.content.UseArchive = this.cursors[i].Archive;
                string asset = this.GetAsset("Cursors", this.cursors[i].Asset, this.cursors[i].Addon);
                asset = this.content.UseArchive ? asset : Path.GetFullPath(asset);
                this.cursors[i].Resource = this.content.Load<Cursor>(asset);
            }
#endif

            for (int i = 0; i < this.images.Count; i++)
            {
                this.content.UseArchive = this.images[i].Archive;
                string asset = this.GetAsset("Images", this.images[i].Asset, this.images[i].Addon);
                asset = this.content.UseArchive ? asset : Path.GetFullPath(asset);
                this.images[i].Resource = this.content.Load<Texture2D>(asset);
            }

            for (int i = 0; i < this.controls.Count; i++)
            {
                for (int j = 0; j < this.controls[i].Layers.Count; j++)
                {
                    if (this.controls[i].Layers[j].Image.Name != null)
                    {
                        this.controls[i].Layers[j].Image = this.images[this.controls[i].Layers[j].Image.Name];
                    }
                    else
                    {
                        this.controls[i].Layers[j].Image = this.images[0];
                    }

                    if (this.controls[i].Layers[j].Text.Name != null)
                    {
                        this.controls[i].Layers[j].Text.Font = this.fonts[this.controls[i].Layers[j].Text.Name];
                    }
                    else
                    {
                        this.controls[i].Layers[j].Text.Font = this.fonts[0];
                    }
                }
            }
        }

        #region XML Operations

        private string ReadAttribute(XmlElement element, string attribute, string defaultValue, bool needed)
        {
            if (element != null && element.HasAttribute(attribute))
            {
                return element.Attributes[attribute].Value;
            }
            else if (needed)
            {
                throw new Exception("Missing required attribute \"" + attribute + "\" in the skin file.");
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
            string temp = returnValue.ToString();
            this.ReadAttribute(ref temp, inherited, element, attribute, defaultValue.ToString(), needed);
            returnValue = byte.Parse(temp);
        }

        private void ReadAttributeColor(ref Color returnValue, bool inherited, XmlElement element, string attribute, Color defaultValue, bool needed)
        {
            string temp = this.ColorToString(returnValue);
            this.ReadAttribute(ref temp, inherited, element, attribute, this.ColorToString(defaultValue), needed);
            returnValue = Utilities.ParseColor(temp);
        }

        #endregion

        private string ColorToString(Color c)
        {
            return $"{c.R};{c.G};{c.B};{c.A}";
        }

        private void LoadSkin(string addon, bool archive)
        {
            try
            {
                bool isaddon = addon != null && addon != "";
                string file = this.GetFolder();
                if (isaddon)
                {
                    file = this.GetAddonsFolder() + addon + "\\";
                }
                file += "Skin";

                file = archive ? file : Path.GetFullPath(file);
                this.doc = this.content.Load<SkinXmlDocument>(file);

                XmlElement e = this.doc["Skin"];
                if (e != null)
                {
                    string xname = this.ReadAttribute(e, "Name", null, true);
                    if (!isaddon)
                    {
                        if (this.name.ToLower() != xname.ToLower())
                        {
                            throw new Exception("Skin name defined in the skin file doesn't match requested skin.");
                        }
                        else
                        {
                            this.name = xname;
                        }
                    }
                    else
                    {
                        if (addon.ToLower() != xname.ToLower())
                        {
                            throw new Exception("Skin name defined in the skin file doesn't match addon name.");
                        }
                    }

                    Version xversion = null;
                    try
                    {
                        xversion = new Version(this.ReadAttribute(e, "Version", "0.0.0.0", false));
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Unable to resolve skin file version. " + x.Message);
                    }

                    if (xversion != this.Manager._SkinVersion)
                    {
                        throw new Exception("This version of Neoforce Controls can only read skin files in version of " + this.Manager._SkinVersion.ToString() + ".");
                    }
                    else if (!isaddon)
                    {
                        this.version = xversion;
                    }

                    if (!isaddon)
                    {
                        XmlElement ei = e["Info"];
                        if (ei != null)
                        {
                            if (ei["Name"] != null) this.info.Name = ei["Name"].InnerText;
                            if (ei["Description"] != null) this.info.Description = ei["Description"].InnerText;
                            if (ei["Author"] != null) this.info.Author = ei["Author"].InnerText;
                            if (ei["Version"] != null) this.info.Version = ei["Version"].InnerText;
                        }
                    }

                    this.LoadImages(addon, archive);
                    this.LoadFonts(addon, archive);
                    this.LoadCursors(addon, archive);
                    this.LoadSkinAttributes();
                    this.LoadControls();
                }
            }
            catch (Exception x)
            {
                throw new Exception("Unable to load skin file. " + x.Message);
            }
        }

        private void LoadSkinAttributes()
        {
            if (this.doc["Skin"]["Attributes"] == null) return;

            XmlNodeList l = this.doc["Skin"]["Attributes"].GetElementsByTagName("Attribute");

            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    SkinAttribute sa = new SkinAttribute();
                    sa.Name = this.ReadAttribute(e, "Name", null, true);
                    sa.Value = this.ReadAttribute(e, "Value", null, true);
                    this.attributes.Add(sa);
                }
            }
        }

        private void LoadControls()
        {
            if (this.doc["Skin"]["Controls"] == null) return;

            XmlNodeList l = this.doc["Skin"]["Controls"].GetElementsByTagName("Control");

            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    SkinControl sc = null;
                    string parent = this.ReadAttribute(e, "Inherits", null, false);
                    bool inh = false;

                    if (parent != null)
                    {
                        sc = new SkinControl(this.controls[parent]);
                        sc.Inherits = parent;
                        inh = true;
                    }
                    else
                    {
                        sc = new SkinControl();
                    }

                    ReadAttribute(ref sc.Name, inh, e, "Name", null, true);

                    ReadAttributeInt(ref sc.DefaultSize.Width, inh, e["DefaultSize"], "Width", 0, false);
                    ReadAttributeInt(ref sc.DefaultSize.Height, inh, e["DefaultSize"], "Height", 0, false);

                    ReadAttributeInt(ref sc.MinimumSize.Width, inh, e["MinimumSize"], "Width", 0, false);
                    ReadAttributeInt(ref sc.MinimumSize.Height, inh, e["MinimumSize"], "Height", 0, false);

                    this.ReadAttributeInt(ref sc.OriginMargins.Left, inh, e["OriginMargins"], "Left", 0, false);
                    this.ReadAttributeInt(ref sc.OriginMargins.Top, inh, e["OriginMargins"], "Top", 0, false);
                    this.ReadAttributeInt(ref sc.OriginMargins.Right, inh, e["OriginMargins"], "Right", 0, false);
                    this.ReadAttributeInt(ref sc.OriginMargins.Bottom, inh, e["OriginMargins"], "Bottom", 0, false);

                    this.ReadAttributeInt(ref sc.ClientMargins.Left, inh, e["ClientMargins"], "Left", 0, false);
                    this.ReadAttributeInt(ref sc.ClientMargins.Top, inh, e["ClientMargins"], "Top", 0, false);
                    this.ReadAttributeInt(ref sc.ClientMargins.Right, inh, e["ClientMargins"], "Right", 0, false);
                    this.ReadAttributeInt(ref sc.ClientMargins.Bottom, inh, e["ClientMargins"], "Bottom", 0, false);

                    this.ReadAttributeInt(ref sc.ResizerSize, inh, e["ResizerSize"], "Value", 0, false);

                    if (e["Layers"] != null)
                    {
                        XmlNodeList l2 = e["Layers"].GetElementsByTagName("Layer");
                        if (l2 != null && l2.Count > 0)
                        {
                            this.LoadLayers(sc, l2);
                        }
                    }

                    if (e["Attributes"] != null)
                    {
                        XmlNodeList l3 = e["Attributes"].GetElementsByTagName("Attribute");
                        if (l3 != null && l3.Count > 0)
                        {
                            this.LoadControlAttributes(sc, l3);
                        }
                    }

                    this.controls.Add(sc);
                }
            }
        }

        private void LoadFonts(string addon, bool archive)
        {
            if (this.doc["Skin"]["Fonts"] == null) return;

            XmlNodeList l = this.doc["Skin"]["Fonts"].GetElementsByTagName("Font");
            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    SkinFont sf = new SkinFont();
                    sf.Name = this.ReadAttribute(e, "Name", null, true);
                    sf.Archive = archive;
                    sf.Asset = this.ReadAttribute(e, "Asset", null, true);
                    sf.Addon = addon;
                    this.fonts.Add(sf);
                }
            }
        }

        private void LoadCursors(string addon, bool archive)
        {
            if (this.doc["Skin"]["Cursors"] == null) return;

            XmlNodeList l = this.doc["Skin"]["Cursors"].GetElementsByTagName("Cursor");
            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    SkinCursor sc = new SkinCursor();
                    sc.Name = this.ReadAttribute(e, "Name", null, true);
                    sc.Archive = archive;
                    sc.Asset = this.ReadAttribute(e, "Asset", null, true);
                    sc.Addon = addon;
                    this.cursors.Add(sc);
                }
            }
        }

        private void LoadImages(string addon, bool archive)
        {
            if (this.doc["Skin"]["Images"] == null) return;
            XmlNodeList l = this.doc["Skin"]["Images"].GetElementsByTagName("Image");
            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    SkinImage si = new SkinImage();
                    si.Name = this.ReadAttribute(e, "Name", null, true);
                    si.Archive = archive;
                    si.Asset = this.ReadAttribute(e, "Asset", null, true);
                    si.Addon = addon;
                    this.images.Add(si);
                }
            }
        }

        private void LoadLayers(SkinControl sc, XmlNodeList l)
        {
            foreach (XmlElement e in l)
            {
                string name = this.ReadAttribute(e, "Name", null, true);
                bool over = this.ReadAttributeBool(e, "Override", false, false);
                SkinLayer sl = sc.Layers[name];
                bool inh = true;

                if (sl == null)
                {
                    sl = new SkinLayer();
                    inh = false;
                }

                if (inh && over)
                {
                    sl = new SkinLayer();
                    sc.Layers[name] = sl;
                }

                ReadAttribute(ref sl.Name, inh, e, "Name", null, true);
                ReadAttribute(ref sl.Image.Name, inh, e, "Image", "Control", false);
                ReadAttributeInt(ref sl.Width, inh, e, "Width", 0, false);
                ReadAttributeInt(ref sl.Height, inh, e, "Height", 0, false);

                string temp = sl.Alignment.ToString();
                this.ReadAttribute(ref temp, inh, e, "Alignment", "MiddleCenter", false);
                sl.Alignment = (Alignment)Enum.Parse(typeof(Alignment), temp, true);

                ReadAttributeInt(ref sl.OffsetX, inh, e, "OffsetX", 0, false);
                ReadAttributeInt(ref sl.OffsetY, inh, e, "OffsetY", 0, false);

                ReadAttributeInt(ref sl.SizingMargins.Left, inh, e["SizingMargins"], "Left", 0, false);
                ReadAttributeInt(ref sl.SizingMargins.Top, inh, e["SizingMargins"], "Top", 0, false);
                ReadAttributeInt(ref sl.SizingMargins.Right, inh, e["SizingMargins"], "Right", 0, false);
                ReadAttributeInt(ref sl.SizingMargins.Bottom, inh, e["SizingMargins"], "Bottom", 0, false);

                ReadAttributeInt(ref sl.ContentMargins.Left, inh, e["ContentMargins"], "Left", 0, false);
                ReadAttributeInt(ref sl.ContentMargins.Top, inh, e["ContentMargins"], "Top", 0, false);
                ReadAttributeInt(ref sl.ContentMargins.Right, inh, e["ContentMargins"], "Right", 0, false);
                ReadAttributeInt(ref sl.ContentMargins.Bottom, inh, e["ContentMargins"], "Bottom", 0, false);

                if (e["States"] != null)
                {
                    ReadAttributeInt(ref sl.States.Enabled.Index, inh, e["States"]["Enabled"], "Index", 0, false);
                    int di = sl.States.Enabled.Index;
                    ReadAttributeInt(ref sl.States.Hovered.Index, inh, e["States"]["Hovered"], "Index", di, false);
                    ReadAttributeInt(ref sl.States.Pressed.Index, inh, e["States"]["Pressed"], "Index", di, false);
                    ReadAttributeInt(ref sl.States.Focused.Index, inh, e["States"]["Focused"], "Index", di, false);
                    ReadAttributeInt(ref sl.States.Disabled.Index, inh, e["States"]["Disabled"], "Index", di, false);

                    ReadAttributeColor(ref sl.States.Enabled.Color, inh, e["States"]["Enabled"], "Color", Color.White, false);
                    Color dc = sl.States.Enabled.Color;
                    ReadAttributeColor(ref sl.States.Hovered.Color, inh, e["States"]["Hovered"], "Color", dc, false);
                    ReadAttributeColor(ref sl.States.Pressed.Color, inh, e["States"]["Pressed"], "Color", dc, false);
                    ReadAttributeColor(ref sl.States.Focused.Color, inh, e["States"]["Focused"], "Color", dc, false);
                    ReadAttributeColor(ref sl.States.Disabled.Color, inh, e["States"]["Disabled"], "Color", dc, false);

                    ReadAttributeBool(ref sl.States.Enabled.Overlay, inh, e["States"]["Enabled"], "Overlay", false, false);
                    bool dv = sl.States.Enabled.Overlay;
                    ReadAttributeBool(ref sl.States.Hovered.Overlay, inh, e["States"]["Hovered"], "Overlay", dv, false);
                    ReadAttributeBool(ref sl.States.Pressed.Overlay, inh, e["States"]["Pressed"], "Overlay", dv, false);
                    ReadAttributeBool(ref sl.States.Focused.Overlay, inh, e["States"]["Focused"], "Overlay", dv, false);
                    ReadAttributeBool(ref sl.States.Disabled.Overlay, inh, e["States"]["Disabled"], "Overlay", dv, false);
                }

                if (e["Overlays"] != null)
                {
                    ReadAttributeInt(ref sl.Overlays.Enabled.Index, inh, e["Overlays"]["Enabled"], "Index", 0, false);
                    int di = sl.Overlays.Enabled.Index;
                    ReadAttributeInt(ref sl.Overlays.Hovered.Index, inh, e["Overlays"]["Hovered"], "Index", di, false);
                    ReadAttributeInt(ref sl.Overlays.Pressed.Index, inh, e["Overlays"]["Pressed"], "Index", di, false);
                    ReadAttributeInt(ref sl.Overlays.Focused.Index, inh, e["Overlays"]["Focused"], "Index", di, false);
                    ReadAttributeInt(ref sl.Overlays.Disabled.Index, inh, e["Overlays"]["Disabled"], "Index", di, false);

                    ReadAttributeColor(ref sl.Overlays.Enabled.Color, inh, e["Overlays"]["Enabled"], "Color", Color.White, false);
                    Color dc = sl.Overlays.Enabled.Color;
                    ReadAttributeColor(ref sl.Overlays.Hovered.Color, inh, e["Overlays"]["Hovered"], "Color", dc, false);
                    ReadAttributeColor(ref sl.Overlays.Pressed.Color, inh, e["Overlays"]["Pressed"], "Color", dc, false);
                    ReadAttributeColor(ref sl.Overlays.Focused.Color, inh, e["Overlays"]["Focused"], "Color", dc, false);
                    ReadAttributeColor(ref sl.Overlays.Disabled.Color, inh, e["Overlays"]["Disabled"], "Color", dc, false);
                }

                if (e["Text"] != null)
                {
                    ReadAttribute(ref sl.Text.Name, inh, e["Text"], "Font", null, true);
                    ReadAttributeInt(ref sl.Text.OffsetX, inh, e["Text"], "OffsetX", 0, false);
                    ReadAttributeInt(ref sl.Text.OffsetY, inh, e["Text"], "OffsetY", 0, false);

                    temp = sl.Text.Alignment.ToString();
                    this.ReadAttribute(ref temp, inh, e["Text"], "Alignment", "MiddleCenter", false);
                    sl.Text.Alignment = (Alignment)Enum.Parse(typeof(Alignment), temp, true);

                    LoadColors(inh, e["Text"], ref sl.Text.Colors);
                }
                if (e["Attributes"] != null)
                {
                    XmlNodeList l2 = e["Attributes"].GetElementsByTagName("Attribute");
                    if (l2 != null && l2.Count > 0)
                    {
                        this.LoadLayerAttributes(sl, l2);
                    }
                }
                if (!inh) sc.Layers.Add(sl);
            }
        }

        private void LoadColors(bool inherited, XmlElement e, ref SkinStates<Color> colors)
        {
            if (e != null)
            {
                ReadAttributeColor(ref colors.Enabled, inherited, e["Colors"]["Enabled"], "Color", Color.White, false);
                ReadAttributeColor(ref colors.Hovered, inherited, e["Colors"]["Hovered"], "Color", colors.Enabled, false);
                ReadAttributeColor(ref colors.Pressed, inherited, e["Colors"]["Pressed"], "Color", colors.Enabled, false);
                ReadAttributeColor(ref colors.Focused, inherited, e["Colors"]["Focused"], "Color", colors.Enabled, false);
                ReadAttributeColor(ref colors.Disabled, inherited, e["Colors"]["Disabled"], "Color", colors.Enabled, false);
            }
        }

        private void LoadControlAttributes(SkinControl sc, XmlNodeList l)
        {
            foreach (XmlElement e in l)
            {
                string name = this.ReadAttribute(e, "Name", null, true);
                SkinAttribute sa = sc.Attributes[name];
                bool inh = true;

                if (sa == null)
                {
                    sa = new SkinAttribute();
                    inh = false;
                }

                sa.Name = name;
                ReadAttribute(ref sa.Value, inh, e, "Value", null, true);

                if (!inh) sc.Attributes.Add(sa);
            }
        }

        private void LoadLayerAttributes(SkinLayer sl, XmlNodeList l)
        {
            foreach (XmlElement e in l)
            {
                string name = this.ReadAttribute(e, "Name", null, true);
                SkinAttribute sa = sl.Attributes[name];
                bool inh = true;

                if (sa == null)
                {
                    sa = new SkinAttribute();
                    inh = false;
                }

                sa.Name = name;
                ReadAttribute(ref sa.Value, inh, e, "Value", null, true);

                if (!inh) sl.Attributes.Add(sa);
            }
        }

        #endregion
    }
}
