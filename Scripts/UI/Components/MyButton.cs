using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Commons.UI
{
    public class MyButton : BaseControl<Button, MyButton>
    {
        public delegate void OnButtonClickEvent(MyButton btn);

        public MyButton(GameObject go, GameObject parent = null, bool isThisPrefab = false)
            : base(go, parent, isThisPrefab)
        {
        }

        public MyButton(Button comp)
            : base(comp)
        {
        }

        public void AddListener(UnityAction listener)
        {
            Component.onClick.AddListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            Component.onClick.RemoveListener(listener);
        }

        protected override MyButton Self()
        {
            return this;
        }
    }
}