namespace MetaMind.Session.Model.Tagging
{
    using System;

    public struct Tag : IEquatable<Tag>
    {
        public Tag(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        public string Name { get; }

        #region IEquable

        public bool Equals(Tag other)
        {
            return this.Name == other.Name;
        }

        #endregion
    }
}
