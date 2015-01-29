// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeQuery.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Parsers.Elements;

    public class KnowledgeQuery
    {
        public KnowledgeQuery(KnowledgeFileBuffer buffer)
        {
            this.Buffer = buffer;

            this.Entries = new List<KnowledgeEntry>();
        }

        public KnowledgeFileBuffer Buffer { get; private set; }

        public List<KnowledgeEntry> Entries { get; private set; }

        public void AddEntry(KnowledgeEntry entry)
        {
            this.Entries.Add(entry);
        }
    }
}