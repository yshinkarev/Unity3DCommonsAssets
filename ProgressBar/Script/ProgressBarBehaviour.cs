using ProgressBar.Entity;
using UnityEngine;

namespace ProgressBar
{
    public class ProgressBarBehaviour : AbstractProgressBehaviour
    {
        [SerializeField]
        public RectTransform FillRect = null;

        private float _xOffset;
        private FillerProperty _fillerInfo;

        void Awake()
        {
            _fillerInfo = new FillerProperty(0, FillRect.rect.width);
            _xOffset = (transform.GetComponent<RectTransform>().rect.width - FillRect.rect.width) / 2;
        }

        protected override void UpdateFillerSizeInternal(int percent, float value)
        {
            FillRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _xOffset, value * _fillerInfo.MaxWidth);
        }
    }
}