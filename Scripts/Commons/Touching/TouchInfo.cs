namespace Commons.Touching
{
    public class TouchInfo
    {
        public int CurTouchNumber;
        public int[] FingerId;
        public bool SwallowsTouches;
        public int TouchPriority;

        public TouchInfo()
        {
            TouchPriority = 0;
            CurTouchNumber = 0;

            //max 10 touches
            FingerId = new int[10];
            SwallowsTouches = false;
        }
    }
}