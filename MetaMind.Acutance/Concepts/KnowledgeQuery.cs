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
        public KnowledgeQuery(RawKnowledgeFileBuffer buffer)
        {
            this.Buffer = buffer;

            this.Data = new List<Knowledge>();
        }

        public RawKnowledgeFileBuffer Buffer { get; private set; }

        public List<Knowledge> Data { get; private set; }

        public void Add(Knowledge entry)
        {
            this.Data.Add(entry);
        }
    }
}