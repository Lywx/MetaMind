// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Geometry
{
    using System;
    using Converters;
    using Microsoft.Xna.Framework;

    public struct MMSize
    {
        public static readonly MMSize Zero = new MMSize(0, 0);

        public static readonly MMSize One = new MMSize(1, 1);

        public float Width;

        public float Height;

        #region Constructors

        public MMSize(float width, float height)
        {
            this.Width  = width;
            this.Height = height;
        }

        #endregion Constructors

        #region Conversion

        /// <summary>
        /// Allow cast MMPoint to MMSize.
        /// </summary> 
        public static explicit operator MMSize(Point point)
        {
            MMSize size;
            size.Width = point.X;
            size.Height = point.Y;
            return size;
        }

        #endregion

        #region Equal

        /// <summary>
        /// Float point comparison.
        /// </summary>
        public static bool Equal(ref MMSize lhs, ref MMSize rhs)
        {
            return (lhs.Width == rhs.Width) && lhs.Height == rhs.Height;
        }

        /// <summary>
        /// Float point comparison.
        /// </summary>
        public bool Equals(MMSize size)
        {
            return Math.Abs(this.Width - size.Width) < float.Epsilon && Math.Abs(this.Height - size.Height) < float.Epsilon;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((MMSize)obj);
        }

        public override int GetHashCode()
        {
            return this.Width.GetHashCode() + this.Height.GetHashCode();
        }

        #endregion Equality

        #region Operators

        public static bool operator ==(MMSize p1, MMSize p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(MMSize p1, MMSize p2)
        {
            return !p1.Equals(p2);
        }

        public static MMSize operator *(MMSize p, float f)
        {
            return new MMSize(p.Width * f, p.Height * f);
        }

        public static MMSize operator *(MMSize p1, MMSize p2)
        {
            return new MMSize(p1.Width * p2.Width, p1.Height * p2.Height);
        }

        public static MMSize operator /(MMSize p, float f)
        {
            return new MMSize(p.Width / f, p.Height / f);
        }

        public static MMSize operator /(MMSize p1, MMSize p2)
        {
            return new MMSize(p1.Width / p2.Width, p1.Height / p2.Height);
        }

        public static MMSize operator +(MMSize p, float f)
        {
            return new MMSize(p.Width + f, p.Height + f);
        }

        public static MMSize operator -(MMSize p, float f)
        {
            return new MMSize(p.Width - f, p.Height - f);
        }

        #endregion Operators

        #region Parse

        public override string ToString()
        {
            return $"MMSize: ({this.Width}, {this.Height})";
        }

        public static MMSize Parse(string s)
        {
            return MMSizeConverter.MMSizeFromString(s);
        }

        #endregion
    }
}
