using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI
{
    public abstract class BaseControl<TComp, TSelf> : BaseControl
        where TSelf : BaseControl<TComp, TSelf>
        where TComp : Component
    {
        private Text _text;

        protected TComp Component;

        protected BaseControl(GameObject go, GameObject parent = null, bool isThisPrefab = false)
        {
            if (isThisPrefab)
                go = UnityEngine.Object.Instantiate(go);

            Initialize(go);

            if (parent != null)
                SetParent(parent);
        }

        protected BaseControl(TComp comp)
        {
            Initialize(comp);
        }

        // Align.

        public new TSelf Align(int align)
        {
            base.Align(align);
            return Self();
        }

        public new TSelf ToLeftOf(BaseControl anchor)
        {
            base.ToLeftOf(anchor);
            return Self();
        }

        public new TSelf ToBelowOf(BaseControl anchor)
        {
            base.ToBelowOf(anchor);
            return Self();
        }

        public new TSelf ToVerticalBaselineOf(BaseControl anchor)
        {
            base.ToVerticalBaselineOf(anchor);
            return Self();
        }

        // Set.

        public new TSelf SetName(string name)
        {
            base.SetName(name);
            return Self();
        }

        public new TSelf SetText(string text)
        {
            _text.text = text;
            return Self();
        }

        public new TSelf SetParent(GameObject parent)
        {
            base.SetParent(parent);
            return Self();
        }

        public new TSelf SetPosition(float x, float y)
        {
            base.SetPosition(x, y);
            return Self();
        }

        // Padding.

        public new TSelf PaddingLeft(float padding)
        {
            Padding(padding, 0f);
            return Self();
        }

        public new TSelf PaddingRight(float padding)
        {
            Padding(-padding, 0f);
            return Self();
        }

        public new TSelf PaddingtTop(float padding)
        {
            Padding(0f, -padding);
            return Self();
        }

        public new TSelf PaddingBottom(float padding)
        {
            Padding(0f, padding);
            return Self();
        }

        // Get.

        public override string GetText()
        {
            return _text.text;
        }

        public new TComp GetComponent()
        {
            return Component;
        }

        public Text GetTextComponent()
        {
            return _text;
        }

        // Protected.

        protected virtual TSelf Self()
        {
            return (TSelf)this;
        }

        protected virtual void Initialize(TComp component)
        {
            Component = component;
            GameObject = component.gameObject;
            _text = GameObject.GetComponentInChildren<Text>();
            Transform = GameObject.GetComponent<RectTransform>();
        }

        // Private.

        protected void Initialize(GameObject go)
        {
            Initialize(go.GetComponent<TComp>());
        }
    }
}