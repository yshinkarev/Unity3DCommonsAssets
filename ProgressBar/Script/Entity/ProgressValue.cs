namespace ProgressBar.Entity
{
    /// <summary>
    /// Used with linear ProgressBars.
    /// Stocks the Current and Max Filler's width
    /// </summary>
    public class ProgressValue
    {
        private float _value;
        private readonly float _maxValue;

        public ProgressValue(float value, float maxValue)
        {
            _value = value;
            _maxValue = maxValue;
        }


        public void Set(float newValue)
        {
            _value = newValue;
        }

        public float AsFloat { get { return _value; } }

        public int AsInt { get { return (int)_value; } }

        public float Normalized { get { return _value / _maxValue; } }

        public float PercentAsFloat { get { return Normalized * 100; } }

        public float PercentAsInt { get { return (int)(PercentAsFloat); } }
    }
}