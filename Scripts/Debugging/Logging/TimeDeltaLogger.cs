using Commons.Debugging.Entity;
using UnityEngine;

namespace Commons.Debugging.Logging
{
    public class TimeDeltaLogger : MonoBehaviour
    {
        private readonly Average _average = new Average();

        protected void Update()
        {
            if (Time.deltaTime < 0.0001f)
                return;

            _average.Add(Time.deltaTime);
        }

        public Average GetStat()
        {
            return _average;
        }

        public override string ToString()
        {
            return _average.ToStringWithAccuracy("0.000");
        }
    }
}