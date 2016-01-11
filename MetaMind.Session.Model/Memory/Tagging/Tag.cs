namespace MetaMind.Session.Model.Memory.Tagging
{
    using System;

    public class Tag : IEquatable<Tag>
    {
        public Tag(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
            this.Name = name;
        }

        public string Name { get; set; }

        #region IEquable

        public bool Equals(Tag other)
        {
            return this.Name == other.Name;
        }

        #endregion

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return this.Name != null ? this.Name.GetHashCode() : 0;
        }
    }
}
