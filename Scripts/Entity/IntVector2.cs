using System;
using UnityEngine;

namespace Assets.Commons_Assets.Scripts.Entity
{
    public class IntVector2
    {
        public int X;
        public int Y;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException("Invalid IntVector2 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid IntVector2 index!");
                }
            }
        }


        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public float Magnitude
        {
            get { return Mathf.Sqrt((float)(X * (double)X + Y * (double)Y)); }
        }

        public bool IsNulled
        {
            get { return X == 0 && Y == 0; }
        }

        public float SqrMagnitude
        {
            get { return (float)(X * (double)X + Y * (double)Y); }
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X + b.X, a.Y + b.Y);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X - b.X, a.Y - b.Y);
        }

        public static IntVector2 operator -(IntVector2 a)
        {
            return new IntVector2(-a.X, -a.Y);
        }

        public static IntVector2 operator *(IntVector2 a, int d)
        {
            return new IntVector2(a.X * d, a.Y * d);
        }

        public static IntVector2 operator *(int d, IntVector2 a)
        {
            return new IntVector2(a.X * d, a.Y * d);
        }

        public static IntVector2 operator /(IntVector2 a, int d)
        {
            return new IntVector2(a.X / d, a.Y / d);
        }

        public static bool operator ==(IntVector2 lhs, IntVector2 rhs)
        {
            if (lhs == null || rhs == null)
                return lhs == rhs;

            return (lhs.X == rhs.X && lhs.Y == rhs.Y);
        }

        public static bool operator !=(IntVector2 lhs, IntVector2 rhs)
        {
            if (lhs == null || rhs == null)
                return lhs != rhs;

            return (lhs.X != rhs.X || lhs.Y != rhs.Y);
        }

        public void Set(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public static IntVector2 Scale(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X * b.X, a.Y * b.Y);
        }

        public void Scale(IntVector2 scale)
        {
            X *= scale.X;
            Y *= scale.Y;
        }


        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is IntVector2))
                return false;

            IntVector2 intVector2 = (IntVector2)other;

            if (X.Equals(intVector2.X))
                return Y.Equals(intVector2.Y);

            return false;
        }

        public static int Dot(IntVector2 lhs, IntVector2 rhs)
        {
            return (int)((double)lhs.X * rhs.X + (double)lhs.Y * rhs.Y);
        }


        public static int Distance(IntVector2 a, IntVector2 b)
        {
            return (int)(a - b).Magnitude;
        }

        public static IntVector2 Min(IntVector2 lhs, IntVector2 rhs)
        {
            return new IntVector2(Mathf.Min(lhs.X, rhs.X), Mathf.Min(lhs.Y, rhs.Y));
        }

        public static IntVector2 Max(IntVector2 lhs, IntVector2 rhs)
        {
            return new IntVector2(Mathf.Max(lhs.X, rhs.X), Mathf.Max(lhs.Y, rhs.Y));
        }
    }
}