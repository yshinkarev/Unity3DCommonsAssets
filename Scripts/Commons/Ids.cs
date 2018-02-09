namespace Commons
{
    public static class Ids
    {
        private static int _id = 1;

        public static int Allocate()
        {
            return _id++;
        }

        public static void Reset()
        {
            _id = 1;
        }
    }
}