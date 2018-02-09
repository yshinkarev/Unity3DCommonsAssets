using UnityEngine;

namespace Commons.Utils
{
    public class ParticleSystemUtils
    {
        public static float GetDuration(GameObject go)
        {
            ParticleSystem[] systems = go.GetComponentsInChildren<ParticleSystem>();
            float duration = 0f;

            foreach (ParticleSystem ps in systems)
            {
                if (duration < ps.duration)
                    duration = ps.duration;
            }

            return duration;
        }
    }
}
