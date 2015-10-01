namespace MetaMind.Engine.Geometry
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;

#if !WINDOWS_PHONE && !NETFX_CORE
    [Serializable, StructLayout(LayoutKind.Sequential)]
#endif
    public struct MMPoint
    {
        #region Static Constants

        public static readonly MMPoint Zero = new MMPoint(0, 0);

        public static readonly MMPoint NegativeInfinity = new MMPoint(float.NegativeInfinity, float.NegativeInfinity);

        #endregion

        #region Static Anchor

        public static readonly MMPoint AnchorMiddle = new MMPoint(0.5f, 0.5f);

        public static readonly MMPoint AnchorLowerLeft = new MMPoint(0f, 0f);

        public static readonly MMPoint AnchorUpperLeft = new MMPoint(0f, 1f);

        public static readonly MMPoint AnchorLowerRight = new MMPoint(1f, 0f);

        public static readonly MMPoint AnchorUpperRight = new MMPoint(1f, 1f);

        public static readonly MMPoint AnchorMiddleRight = new MMPoint(1f, .5f);

        public static readonly MMPoint AnchorMiddleLeft = new MMPoint(0f, .5f);

        public static readonly MMPoint AnchorMiddleTop = new MMPoint(.5f, 1f);

        public static readonly MMPoint AnchorMiddleBottom = new MMPoint(.5f, 0f);

        #endregion

        public float X;

        public float Y;

        #region Properties

        public float LengthSquared => this.X * this.X + this.Y * this.Y;

        // Computes the length of this point as if it were a vector with XY components relative to the
        // origin. This is computed each time this property is accessed, so cache the value that is
        // returned.
        public float Length => (float)Math.Sqrt(this.X * this.X + this.Y * this.Y);

        public MMPoint InvertY
        {
            get
            {
                MMPoint pt;
                pt.X = this.X;
                pt.Y = -this.Y;
                return pt;
            }
        }

        public MMPoint InvertX
        {
            get
            {
                MMPoint pt;
                pt.X = -this.X;
                pt.Y = this.Y;
                return pt;
            }
        }

        internal Vector3 XnaVector => new Vector3(this.X, this.Y, 0.0f);

        #endregion Properties

        #region Constructors

        public MMPoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public MMPoint(MMPoint pt)
        {
            this.X = pt.X;
            this.Y = pt.Y;
        }

        public MMPoint(MMVector2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        internal MMPoint(Vector2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        #endregion Constructors

        #region Equality

        public static bool Equal(ref MMPoint point1, ref MMPoint point2)
        {
            return ((point1.X == point2.X) && (point1.Y == point2.Y));
        }

        public override bool Equals(object obj)
        {
            return (this.Equals((MMPoint)obj));
        }

        public bool Equals(MMPoint p)
        {
            return this.X == p.X && this.Y == p.Y;
        }

        #endregion Equality

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"MMPoint : (x={this.X}, y={this.Y})";
        }

        public float DistanceSquared(ref MMPoint v2)
        {
            return this.Sub(ref v2).LengthSquared;
        }

        public float Angle => (float)Math.Atan2(this.Y, this.X);

        /// <summary>
        ///     Normalizes the components of this point (convert to mag 1), and returns the original
        ///     magnitude of the vector defined by the XY components of this point.
        /// </summary>
        /// <returns></returns>
        public float Normalize()
        {
            var mag = (float)Math.Sqrt(this.X * this.X + this.Y * this.Y);
            if (mag < float.Epsilon)
            {
                return (0f);
            }
            var l = 1f / mag;
            this.X *= l;
            this.Y *= l;
            return (mag);
        }

        public MMPoint Sub(ref MMPoint v2)
        {
            MMPoint pt;
            pt.X = this.X - v2.X;
            pt.Y = this.Y - v2.Y;
            return pt;
        }

        public MMPoint Offset(float dx, float dy)
        {
            MMPoint pt;
            pt.X = this.X + dx;
            pt.Y = this.Y + dy;
            return pt;
        }

        public MMPoint RoundToInteger()
        {
            MMPoint pt;
            pt.X = (float)Math.Round(this.X);
            pt.Y = (float)Math.Round(this.Y);

            return pt;
        }

        #region Static Methods

        /** Run a math operation function on each point component
             * absf, fllorf, ceilf, roundf
             * any function that has the signature: float func(float);
             * For example: let's try to take the floor of x,y
             * ccpCompOp(p,floorf);
             @since v0.99.1
             */

        public delegate float ComputationOperationDelegate(float a);

        public static MMPoint ComputationOperation(
            MMPoint p,
            ComputationOperationDelegate del)
        {
            MMPoint pt;
            pt.X = del(p.X);
            pt.Y = del(p.Y);
            return pt;
        }

        /** Linear Interpolation between two points a and b
                @returns
                  alpha == 0 ? a
                  alpha == 1 ? b
                  otherwise a value between a..B
                @since v0.99.1
           */

        public static MMPoint Lerp(MMPoint a, MMPoint b, float alpha)
        {
            return (a * (1f - alpha) + b * alpha);
        }


        /** @returns if points have fuzzy equality which means equal with some degree of variance.
                @since v0.99.1
            */

        public static bool FuzzyEqual(MMPoint a, MMPoint b, float variance)
        {
            if (a.X - variance <= b.X
                && b.X <= a.X + variance)
            {
                if (a.Y - variance <= b.Y
                    && b.Y <= a.Y + variance)
                {
                    return true;
                }
            }

            return false;
        }


        /** Multiplies a nd b components, a.X*b.X, a.y*b.y
                @returns a component-wise multiplication
                @since v0.99.1
            */

        public static MMPoint MultiplyComponents(MMPoint a, MMPoint b)
        {
            MMPoint pt;
            pt.X = a.X * b.X;
            pt.Y = a.Y * b.Y;
            return pt;
        }

        /** @returns the signed angle in radians between two vector directions
                @since v0.99.1
            */

        public static float AngleSigned(MMPoint a, MMPoint b)
        {
            var a2 = Normalize(a);
            var b2 = Normalize(b);
            var angle =
                (float)Math.Atan2(a2.X * b2.Y - a2.Y * b2.X, Dot(a2, b2));

            if (Math.Abs(angle) < float.Epsilon)
            {
                return 0.0f;
            }

            return angle;
        }

        /** Rotates a point counter clockwise by the angle around a pivot
                @param v is the point to rotate
                @param pivot is the pivot, naturally
                @param angle is the angle of rotation ccw in radians
                @returns the rotated point
                @since v0.99.1
            */

        public static MMPoint RotateByAngle(
            MMPoint v,
            MMPoint pivot,
            float angle)
        {
            var r = v - pivot;
            float cosa = (float)Math.Cos(angle), sina = (float)Math.Sin(angle);
            var t = r.X;

            r.X = t * cosa - r.Y * sina + pivot.X;
            r.Y = t * sina + r.Y * cosa + pivot.Y;

            return r;
        }

        /** A general line-line intersection test
             @param p1 
                is the startpoint for the first line P1 = (p1 - p2)
             @param p2 
                is the endpoint for the first line P1 = (p1 - p2)
             @param p3 
                is the startpoint for the second line P2 = (p3 - p4)
             @param p4 
                is the endpoint for the second line P2 = (p3 - p4)
             @param s 
                is the range for a hitpoint in P1 (pa = p1 + s*(p2 - p1))
             @param t
                is the range for a hitpoint in P3 (pa = p2 + t*(p4 - p3))
             @return bool 
                indicating successful intersection of a line
                note that to truly test intersection for segments we have to make 
                sure that s & t lie within [0..1] and for rays, make sure s & t > 0
                the hit point is		p3 + t * (p4 - p3);
                the hit point also is	p1 + s * (p2 - p1);
             @since v0.99.1
             */

        public static bool LineIntersect(
            MMPoint A,
            MMPoint B,
            MMPoint C,
            MMPoint D,
            ref float S,
            ref float T)
        {
            // FAIL: Line undefined
            if ((A.X == B.X && A.Y == B.Y)
                || (C.X == D.X && C.Y == D.Y))
            {
                return false;
            }

            var BAx = B.X - A.X;
            var BAy = B.Y - A.Y;
            var DCx = D.X - C.X;
            var DCy = D.Y - C.Y;
            var ACx = A.X - C.X;
            var ACy = A.Y - C.Y;

            var denom = DCy * BAx - DCx * BAy;

            S = DCx * ACy - DCy * ACx;
            T = BAx * ACy - BAy * ACx;

            if (denom == 0)
            {
                if (S == 0
                    || T == 0)
                {
                    // Lines incident
                    return true;
                }
                // Lines parallel and not incident
                return false;
            }

            S = S / denom;
            T = T / denom;

            // Point of intersection
            // CGPoint P;
            // P.X = A.X + *S * (B.X - A.X);
            // P.y = A.y + *S * (B.y - A.y);

            return true;
        }

        /*
            ccpSegmentIntersect returns YES if Segment A-B intersects with segment C-D
            @since v1.0.0
            */

        public static bool SegmentIntersect(
            MMPoint A,
            MMPoint B,
            MMPoint C,
            MMPoint D)
        {
            float S = 0, T = 0;

            if (LineIntersect(A, B, C, D, ref S, ref T)
                && (S >= 0.0f && S <= 1.0f && T >= 0.0f && T <= 1.0f))
            {
                return true;
            }

            return false;
        }

        /*
            ccpIntersectPoint returns the intersection point of line A-B, C-D
            @since v1.0.0
            */

        public static MMPoint IntersectPoint(
            MMPoint A,
            MMPoint B,
            MMPoint C,
            MMPoint D)
        {
            float S = 0, T = 0;

            if (LineIntersect(A, B, C, D, ref S, ref T))
            {
                // Point of intersection
                MMPoint P;
                P.X = A.X + S * (B.X - A.X);
                P.Y = A.Y + S * (B.Y - A.Y);
                return P;
            }

            return Zero;
        }

        /** Converts radians to a normalized vector.
                @return MMPoint
                @since v0.7.2
            */

        public static MMPoint ForAngle(float a)
        {
            MMPoint pt;
            pt.X = (float)Math.Cos(a);
            pt.Y = (float)Math.Sin(a);
            return pt;
        }

        /** Converts a vector to radians.
                @return CGFloat
                @since v0.7.2
            */

        public static float ToAngle(MMPoint v)
        {
            return (float)Math.Atan2(v.Y, v.X);
        }


        /** Clamp a value between from and to.
                @since v0.99.1
            */

        public static float Clamp(
            float value,
            float min_inclusive,
            float max_inclusive)
        {
            if (min_inclusive > max_inclusive)
            {
                var ftmp = min_inclusive;
                min_inclusive = max_inclusive;
                max_inclusive = ftmp;
            }

            return value < min_inclusive
                       ? min_inclusive
                       : value < max_inclusive ? value : max_inclusive;
        }

        /** Clamp a point between from and to.
                @since v0.99.1
            */

        public static MMPoint Clamp(MMPoint p, MMPoint from, MMPoint to)
        {
            MMPoint pt;
            pt.X = Clamp(p.X, from.X, to.X);
            pt.Y = Clamp(p.Y, from.Y, to.Y);
            return pt;
        }

        /// Clamp MMPoint p to length len.
        public static MMPoint Clamp(MMPoint p, float len)
        {
            return (Dot(p, p) > len * len) ? Normalize(p) * len : p;
        }

        /// Clamp point object to the specified len.
        public MMPoint Clamp(float len)
        {
            return Clamp(this, len);
        }

        /// Returns true if the distance between p1 and p2 is less than dist.
        public static bool IsNear(MMPoint p1, MMPoint p2, float dist)
        {
            return p1.DistanceSquared(ref p2) < dist * dist;
        }

        /// Returns true if the distance between MMPoint object and p2 is less than dist.
        public bool IsNear(MMPoint p2, float dist)
        {
            return this.DistanceSquared(ref p2) < dist * dist;
        }

        /**
             * Allow Cast MMSize to MMPoint
             */

        public static explicit operator MMPoint(MMSize size)
        {
            MMPoint pt;
            pt.X = size.Width;
            pt.Y = size.Height;
            return pt;
        }

        public static float Dot(MMPoint p1, MMPoint p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        public static float Distance(MMPoint v1, MMPoint v2)
        {
            return (v1 - v2).Length;
        }

        public static MMPoint Normalize(MMPoint p)
        {
            var x = p.X;
            var y = p.Y;
            var l = 1f / (float)Math.Sqrt(x * x + y * y);
            MMPoint pt;
            pt.X = x * l;
            pt.Y = y * l;
            return pt;
        }

        public static MMPoint Midpoint(MMPoint p1, MMPoint p2)
        {
            MMPoint pt;
            pt.X = (p1.X + p2.X) / 2f;
            pt.Y = (p1.Y + p2.Y) / 2f;
            return pt;
        }

        /** Calculates cross product of two points.
                @return CGFloat
                @since v0.7.2
            */

        public static float CrossProduct(MMPoint v1, MMPoint v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        /// <summary>
        ///     Calculates perpendicular of v, rotated 90 degrees counter-clockwise -- cross(v, PerpendicularMMW(v)) >= 0
        /// </summary>
        /// <returns>A perpendicular point to source point</returns>
        /// <param name="p">Source point.</param>
        public static MMPoint PerpendicularMMW(MMPoint p)
        {
            MMPoint pt;
            pt.X = -p.Y;
            pt.Y = p.X;
            return pt;
        }

        /// <summary>
        ///     Calculates perpendicular of v, rotated 90 degrees clockwise -- cross(v, PerpendicularCW(v)) <= 0
        /// </summary>
        /// <returns>A perpendicular point to source point</returns>
        /// <param name="p">Source point.</param>
        public static MMPoint PerpendicularCW(MMPoint p)
        {
            MMPoint pt;
            pt.X = p.Y;
            pt.Y = -p.X;
            return pt;
        }

        /** Calculates the projection of v1 over v2.
                @return MMPoint
                @since v0.7.2
            */

        public static MMPoint Project(MMPoint v1, MMPoint v2)
        {
            var dp1 = v1.X * v2.X + v1.Y * v2.Y;
            var dp2 = v2.LengthSquared;
            var f = dp1 / dp2;
            MMPoint pt;
            pt.X = v2.X * f;
            pt.Y = v2.Y * f;
            return pt;
            // return Multiply(v2, DotProduct(v1, v2) / DotProduct(v2, v2));
        }

        /** Rotates two points.
                @return MMPoint
                @since v0.7.2
            */

        public static MMPoint Rotate(MMPoint v1, MMPoint v2)
        {
            MMPoint pt;
            pt.X = v1.X * v2.X - v1.Y * v2.Y;
            pt.Y = v1.X * v2.Y + v1.Y * v2.X;
            return pt;
        }

        /** Unrotates two points.
                @return MMPoint
                @since v0.7.2
            */

        public static MMPoint Unrotate(MMPoint v1, MMPoint v2)
        {
            MMPoint pt;
            pt.X = v1.X * v2.X + v1.Y * v2.Y;
            pt.Y = v1.Y * v2.X - v1.X * v2.Y;
            return pt;
        }

        #endregion

        #region Operator Overloads

        public static bool operator ==(MMPoint p1, MMPoint p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(MMPoint p1, MMPoint p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }

        public static MMPoint operator -(MMPoint p1, MMPoint p2)
        {
            MMPoint pt;
            pt.X = p1.X - p2.X;
            pt.Y = p1.Y - p2.Y;
            return pt;
        }

        public static MMPoint operator -(MMPoint p1)
        {
            MMPoint pt;
            pt.X = -p1.X;
            pt.Y = -p1.Y;
            return pt;
        }

        public static MMPoint operator +(MMPoint p1, MMPoint p2)
        {
            MMPoint pt;
            pt.X = p1.X + p2.X;
            pt.Y = p1.Y + p2.Y;
            return pt;
        }

        public static MMPoint operator +(MMPoint p1)
        {
            MMPoint pt;
            pt.X = +p1.X;
            pt.Y = +p1.Y;
            return pt;
        }

        public static MMPoint operator *(MMPoint p, float value)
        {
            MMPoint pt;
            pt.X = p.X * value;
            pt.Y = p.Y * value;
            return pt;
        }

        public static MMPoint operator *(MMPoint p1, MMPoint p2)
        {
            return new MMPoint(p1.X * p2.X, p1.Y * p2.Y);
        }

        public static MMPoint operator /(MMPoint p, float value)
        {
            MMPoint pt;
            pt.X = p.X / value;
            pt.Y = p.Y / value;
            return pt;
        }

        public static MMPoint operator /(MMPoint p, MMSize size)
        {
            return new MMPoint(p.X / size.Width, p.Y / size.Height);
        }

        public static MMPoint operator *(MMPoint point, MMSize size)
        {
            return new MMPoint(point.X * size.Width, point.Y * size.Height);
        }

        #endregion

        public static MMPoint Parse(string s)
        {
            return (MMPointConverter.MMPointFromString(s));
        }

        public static implicit operator MMPoint(MMVector2 point)
        {
            return new MMPoint(point.X, point.Y);
        }

        public static implicit operator MMVector2(MMPoint point)
        {
            return new MMVector2(point.X, point.Y);
        }
    }
}
