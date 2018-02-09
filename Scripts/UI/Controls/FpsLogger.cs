using Commons.Debugging.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI
{
    public class FpsLogger : MonoBehaviour
    {
        public Text Text;
        public string Display = "{0:0} FPS ({1:0} / {2:0})";
        public float IgnoreFpsFirstTime = 0f;

        private const float FPS_MEASURE_PERIOD = 1f;
        private int _fpsAccumulator;

        private readonly Average _average = new Average();
        private float _fpsNextPeriod, _stopIgnoreTime;

        protected void Start()
        {
            if (Text == null)
            {
                Text = GetComponent<Text>();

                if (Text == null)
                    print("Not set 'Text' Value");
            }

            if (IgnoreFpsFirstTime > 0f)
            {
                float seconds = IgnoreFpsFirstTime / 1000f;
                _stopIgnoreTime = Time.realtimeSinceStartup + seconds;
                Text.text = string.Format("FPS (ignore {0:0.0 s})", seconds);
            }
            else
                _stopIgnoreTime = Time.realtimeSinceStartup;

            _fpsNextPeriod = _stopIgnoreTime + FPS_MEASURE_PERIOD;
        }

        protected void Update()
        {
            if (Time.realtimeSinceStartup <= _stopIgnoreTime)
                return;

            _fpsAccumulator++;

            if (Time.realtimeSinceStartup <= _fpsNextPeriod)
                return;

            float currentFps = _fpsAccumulator / FPS_MEASURE_PERIOD;
            _average.Add(currentFps);
            Text.text = _average.ToString(Display);

            _fpsAccumulator = 0;
            _fpsNextPeriod += FPS_MEASURE_PERIOD;
        }
    }
}