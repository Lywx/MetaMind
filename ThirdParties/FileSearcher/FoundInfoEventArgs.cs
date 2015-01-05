namespace FileSearcher
{
    using System.IO;

    public class FoundInfoEventArgs
    {
        // ----- Variables -----

        private FileSystemInfo m_info;


        // ----- Constructor -----

        public FoundInfoEventArgs(FileSystemInfo info)
        {
            this.m_info = info;
        }


        // ----- Public Properties -----

        public FileSystemInfo Info
        {
            get { return this.m_info; }
        }
    }
}
