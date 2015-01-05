// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sentence.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class Sentence
    {
        private readonly List<string> words;

        public Sentence(List<string> words)
        {
            this.words = words;
        }

        public List<string> Words
        {
            get
            {
                return this.words;
            }
        }

        public override string ToString()
        {
            return string.Join(" ", this.words);
        }

        public override int GetHashCode()
        {
            return this.words != null ? this.words.GetHashCode() : 0;
        }

        protected bool Equals(Sentence other)
        {
            return Equals(this.words, other.words);
        }
    }
}