using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar
{
    [RequireComponent(typeof(Image))]
    public class ProgressRadialBehaviour : AbstractProgressBehaviour
    {
        private Image _fill;

        void Awake()
        {
            _fill = GetComponent<Image>();
        }

        protected override void UpdateFillerSizeInternal(int percent, float value)
        {
            _fill.fillAmount = value;
        }
    }
}