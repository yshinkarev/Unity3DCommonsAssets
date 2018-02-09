using System;

namespace Commons.Utils
{
    public static class RandomUtils
    {
        static RandomUtils()
        {
            Reset();
        }

        private static Random _random;

        public static float Float()
        {
            return (float)_random.NextDouble();
            //            float res = (float)_random.NextDouble();
            //            Tracks.StackTrace("Random = {0}", res);
            //            return res;
        }

        public static bool Bool()
        {
            return Float() < 0.5f;
        }

        public static void Reset(int seed = 1982)
        {
            _random = new Random(seed);
        }
    }
}