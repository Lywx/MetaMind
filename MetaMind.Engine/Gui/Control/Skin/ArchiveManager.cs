/*
namespace MetaMind.Engine.Gui.Control.Skin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Xna.Framework.Content;

    public class ArchiveManager : ContentManager
    {
        #region //// Fields ////////////
    
        private string archivePath = null;

        private ZipFile archive = null;

        private bool useArchive = false;
    
        #endregion

        #region //// Properties ////////

        public virtual string ArchivePath
        {
            get { return this.archivePath; }
        }
    
        public bool UseArchive
        {
            get { return this.useArchive; }
            set { this.useArchive = value; }
        }

        #endregion

        #region //// Constructors //////

        public ArchiveManager(IServiceProvider serviceProvider) : this(serviceProvider, null) { }
    
        public ArchiveManager(IServiceProvider serviceProvider, string archive) : base(serviceProvider)
        {
            if (archive != null)
            {
                this.archive = ZipFile.Read(archive);
                this.archivePath = archive;
                this.useArchive = true;
            }
        }

        #endregion

        #region //// Methods ///////////
            
        protected override Stream OpenStream(string assetName)
        {
            if (this.useArchive && this.archive != null)
            {
                assetName = assetName.Replace("\\", "/");
                if (assetName.StartsWith("/")) assetName = assetName.Remove(0, 1);

                string fullAssetName = (assetName + ".xnb").ToLower();

                foreach (ZipEntry entry in this.archive)
                {
                    ZipDirEntry ze = new ZipDirEntry(entry);

                    string entryName = entry.FileName.ToLower();

                    if (entryName == fullAssetName)
                    {
                        return entry.GetStream();
                    }
                }
                throw new Exception("Cannot find asset \"" + assetName + "\" in the archive.");
            }
            else
            {
                return base.OpenStream(assetName);
            }
        }
    
        public string[] GetAssetNames()
        {
            if (this.useArchive && this.archive != null)
            {
                List<string> filenames = new List<string>();

                foreach (ZipEntry entry in this.archive)
                {
                    string name = entry.FileName;
                    if (name.EndsWith(".xnb"))
                    {
                        name = name.Remove(name.Length - 4, 4);
                        filenames.Add(name);
                    }
                }
                return filenames.ToArray();
            }
            else
            {
                return null;
            }
        }
    
        /// <include file='Documents/ArchiveManager.xml' path='ArchiveManager/Member[@name="GetAssetNames1"]/*' />        
        public string[] GetAssetNames(string path)
        {
            if (this.useArchive && this.archive != null)
            {
                if (path != null && path != "" && path != "\\" && path != "/")
                {
                    List<string> filenames = new List<string>();

                    foreach (ZipEntry entry in this.archive)
                    {
                        string name = entry.FileName;
                        if (name.EndsWith(".xnb"))
                        {
                            name = name.Remove(name.Length - 4, 4);
                        }

                        string[] parts = name.Split('/');
                        string dir = "";
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            dir += parts[i] + '/';
                        }

                        path = path.Replace("\\", "/");
                        if (path.StartsWith("/")) path = path.Remove(0, 1);
                        if (!path.EndsWith("/")) path += '/';

                        if (dir.ToLower() == path.ToLower() && !name.EndsWith("/"))
                        {
                            filenames.Add(name);
                        }
                    }
                    return filenames.ToArray();
                }
                else
                {
                    return this.GetAssetNames();
                }
            }
            else
            {
                return null;
            }
        }
    
        public Stream GetFileStream(string filename)
        {
            if (this.useArchive && this.archive != null)
            {
                filename = filename.Replace("\\", "/").ToLower();
                if (filename.StartsWith("/")) filename = filename.Remove(0, 1);

                foreach (ZipEntry entry in this.archive)
                {
                    string entryName = entry.FileName.ToLower();

                    if (entryName.Equals(filename))
                        return entry.GetStream();
                }

                throw new Exception("Cannot find file \"" + filename + "\" in the archive.");
            }
            else
            {
                return null;
            }
        }
    
        public string[] GetDirectories(string path)
        {
            if (this.useArchive && this.archive != null)
            {
                if (path != null && path != "" && path != "\\" && path != "/")
                {
                    List<string> dirs = new List<string>();

                    path = path.Replace("\\", "/");
                    if (path.StartsWith("/")) path = path.Remove(0, 1);
                    if (!path.EndsWith("/")) path += '/';

                    foreach (ZipEntry entry in this.archive)
                    {
                        string name = entry.FileName;
                        if (name.ToLower().StartsWith(path.ToLower()))
                        {
                            int i = name.IndexOf("/", path.Length);
                            string item = name.Substring(path.Length, i - path.Length) + "\\";
                            if (!dirs.Contains(item))
                            {
                                dirs.Add(item);
                            }
                        }
                    }
                    return dirs.ToArray();
                }
                else
                {
                    return this.GetAssetNames();
                }
            }
            else if (Directory.Exists(path))
            {
                string[] dirs = Directory.GetDirectories(path);

                for (int i = 0; i < dirs.Length; i++)
                {
                    string[] parts = dirs[i].Split('\\');
                    dirs[i] = parts[parts.Length - 1] + '\\';
                }

                return dirs;
            }
            else return null;
        }

        #endregion
    }
}
*/
