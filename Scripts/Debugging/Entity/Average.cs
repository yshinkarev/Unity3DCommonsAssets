namespace Commons.Debugging.Entity
{
    public class Average
    {
        private float _min = float.MaxValue;
        private float _max = float.MinValue;
        private int _count;
        private float _total;

        public Average()
        {
            Clear();
        }

        public void Add(float value)
        {
            if (value < _min)
                _min = value;

            if (value > _max)
                _max = value;


            _total += value;
            _count++;
        }

        public void Clear()
        {
            _min = float.MaxValue;
            _max = float.MinValue;
            _count = 0;
            _total = 0f;
        }

        public float Min
        {
            get { return _min; }
        }

        public float Max
        {
            get { return _max; }
        }

        public float AverageValue
        {
            get
            {
                return _total / _count;
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public override string ToString()
        {
            return ToString("0.00");
        }

        public string ToString(string format)
        {
            return string.Format(format, AverageValue, _min, _max, _count);
        }

        public string ToStringWithAccuracy(string accuracy)
        {
            return ToString("Value: {0:" + accuracy + "}. Min: {1:" + accuracy + "}. Max: {2:" + accuracy + "}. Count: {3}");
        }
    }
}