namespace FileSearcher
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SearcherParams
    {
        // ----- Variables -----

        private String       m_searchDir;
        private Boolean      m_includeSubDirsChecked;
        private List<String> m_fileNames;
        private Boolean      m_newerThanChecked;
        private DateTime     m_newerThanDateTime;
        private Boolean      m_olderThanChecked;
        private DateTime     m_olderThanDateTime;
        private Boolean      m_containingChecked;
        private String       m_containingText;
        private Encoding     m_encoding;

        // ----- Constructor -----

        public SearcherParams(
            String       searchDir,
            Boolean      includeSubDirsChecked,
            List<String> fileNames,
            Boolean      newerThanChecked,
            DateTime     newerThanDateTime,
            Boolean      olderThanChecked,
            DateTime     olderThanDateTime,
            Boolean      containingChecked,
            String       containingText,
            Encoding     encoding)
        {
            this.m_searchDir             = searchDir;
            this.m_includeSubDirsChecked = includeSubDirsChecked;
            this.m_fileNames             = fileNames;
            this.m_newerThanChecked      = newerThanChecked;
            this.m_newerThanDateTime     = newerThanDateTime;
            this.m_olderThanChecked      = olderThanChecked;
            this.m_olderThanDateTime     = olderThanDateTime;
            this.m_containingChecked     = containingChecked;
            this.m_containingText        = containingText;
            this.m_encoding              = encoding;
        }

        // ----- Public Properties -----

        public String SearchDir
        {
            get { return this.m_searchDir; }
        }

        public Boolean IncludeSubDirsChecked
        {
            get { return this.m_includeSubDirsChecked; }
        }

        public List<String> FileNames
        {
            get { return this.m_fileNames; }
        }

        public Boolean NewerThanChecked
        {
            get { return this.m_newerThanChecked; }
        }

        public DateTime NewerThanDateTime
        {
            get { return this.m_newerThanDateTime; }
        }

        public Boolean OlderThanChecked
        {
            get { return this.m_olderThanChecked; }
        }

        public DateTime OlderThanDateTime
        {
            get { return this.m_olderThanDateTime; }
        }

        public Boolean ContainingChecked
        {
            get { return this.m_containingChecked; }
        }

        public String ContainingText
        {
            get { return this.m_containingText; }
        }

        public Encoding Encoding
        {
            get { return this.m_encoding; }
        }
    }
}