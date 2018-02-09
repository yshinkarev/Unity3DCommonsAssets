using UnityEngine;

namespace Commons.Utils
{
    public static class ColorUtils
    {
        public static Color Clone(Color source)
        {
            return new Color(source.r, source.g, source.b, source.a);
        }

        public static Color ChangeAlpha(Color source, float alpha)
        {
            Color result = Clone(source);
            result.a = alpha;
            return result;
        }

        public static Color HexToColor(int hex)
        {
            byte a = (byte)((hex >> 24) & 0xFF);
            byte r = (byte)((hex >> 16) & 0xFF);
            byte g = (byte)((hex >> 8) & 0xFF);
            byte b = (byte)((hex) & 0xFF);
            return new Color(r, g, b, a);
        }
    }
}