// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FileSearcher;

    using MetaMind.Engine.Components;

    public static class SearchSettings
    {
        public static SearcherParams SearchParams(List<string> fileNames)
        {
            return new SearcherParams(
                searchDir:             FileManager.DataFolderPath,
                includeSubDirsChecked: true,
                fileNames:             fileNames,
                newerThanChecked:      false,
                newerThanDateTime:     DateTime.MinValue,
                olderThanChecked:      false,
                olderThanDateTime:     DateTime.MinValue,
                containingChecked:     false,
                containingText:        string.Empty,
                encoding:              Encoding.Unicode);
        }

        public static List<string> SearchName(string fileName, bool fuzzy)
        {
            // won't be able to search directories now with .md subfix
            var subfix = ".md";
            if (fuzzy)
            {
                return new List<string>(1) { "*" + fileName + "*" + subfix };
            }
            else
            {
                return new List<string>(1) { fileName + subfix };
            }
        }
    }
}