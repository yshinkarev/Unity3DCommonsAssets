using UnityEngine;

namespace Commons.UI
{
    public class MySliderWithText : BaseControl<SliderWithText, MySliderWithText>
    {
        public MySliderWithText(GameObject go, GameObject parent = null, bool isThisPrefab = false)
            : base(go, parent, isThisPrefab)
        {
        }

        protected override MySliderWithText Self()
        {
            return this;
        }
    }
}