using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI
{
    public class MyText : BaseControl<Text, MyText>
    {
        public MyText(GameObject go, GameObject parent = null, bool isThisPrefab = false)
            : base(go, parent, isThisPrefab)
        {
        }

        protected override MyText Self()
        {
            return this;
        }
    }
}