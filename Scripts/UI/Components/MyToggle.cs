using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI
{
    public class MyToggle : BaseControl<Toggle, MyToggle>
    {
        public MyToggle(GameObject go, GameObject parent = null, bool isThisPrefab = false)
            : base(go, parent, isThisPrefab)
        {
        }

        public bool IsOn
        {
            get { return Component.isOn; }
            set { Component.isOn = value; }
        }

        public MyToggle SetOn(bool on)
        {
            IsOn = on;
            return Self();
        }

        protected override MyToggle Self()
        {
            return this;
        }
    }
}