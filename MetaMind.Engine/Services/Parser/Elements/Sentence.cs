// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sentence.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services.Parser.Elements
{
    using System.Collections.Generic;

    public class Sentence
    {
        private readonly List<string> words;

        public Sentence(List<string> words)
        {
            this.words = words;
        }

        public List<string> Words => this.words;

        public override string ToString()
        {
            return string.Join(" ", this.words);
        }

        public override int GetHashCode()
        {
            return this.words?.GetHashCode() ?? 0;
        }

        protected bool Equals(Sentence other)
        {
            return Equals(this.words, other.words);
        }
    }
}