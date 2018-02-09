using UnityEngine;

namespace Commons.Utils
{
    public static class MathUtils
    {
        public static bool IsOneSign(float a, float b)
        {
            return (a > 0f && b > 0f) || (a < 0f && b < 0f);
        }

        public static Vector2 GetSize(Rect rect)
        {
            return new Vector2(rect.width, rect.height);
        }

        public static Vector2 PairMultiplication(Vector2 v1, Vector2 v2)
        {
            return new Vector2 { x = v1.x * v2.x, y = v1.y * v2.y };
        }

        public static Vector3 AddX(Vector3 v, float dx)
        {
            v.x += dx;
            return v;
        }

        public static bool IsInRange(float value, Vector2 range)
        {
            return range.x <= value && value <= range.y;
        }

        public static void Copy(Vector2 from, ref Vector3 to)
        {
            to.x = from.x;
            to.y = from.y;
        }
    }
}