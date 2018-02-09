using System;
using Commons.Utils;
using UnityEngine;

namespace Commons.UI
{
    public abstract class BaseControl
    {
        public RectTransform Transform;
        protected GameObject GameObject;

        private RectTransform _parentTransform;

        // Align.

        public BaseControl Align(int align)
        {
            RectTransform ptrans = ParentTransform();
            Vector2 psize = ParentSize();
            Vector2 pSizeDelta = ptrans.sizeDelta;

            Vector2 padLeftBottom = ptrans.offsetMin;
            Vector2 padRightTop = ptrans.offsetMax;

            if (pSizeDelta.x < 0f)
                padLeftBottom.y = padLeftBottom.x;

            if (pSizeDelta.y < 0f)
                padRightTop.x = padRightTop.y;

            Vector2 size = Transform.sizeDelta;
            //            Vector2 anchor = Transform.anchorMin;
            Vector2 anchor = new Vector2(0f, 1f);
            anchor.y = 1f - anchor.y;

            Vector2 start = MathUtils.PairMultiplication(psize, anchor);
            //            Vector2 start = Vector2.zero;

            float x = 0f, y = 0f;
            bool setX = false, setY = false;

            if ((align & Aligns.LEFT) != 0)
            {
                x = -start.x + size.x / 2f + padLeftBottom.x;
                setX = true;
            }
            else if ((align & Aligns.RIGHT) != 0)
            {
                x = psize.x - start.x - size.x - padRightTop.x + padLeftBottom.x;
                setX = true;
            }

            if ((align & Aligns.TOP) != 0)
            {
                y = -start.y - size.y / 2f + padRightTop.y;
                setY = true;
            }
            else if ((align & Aligns.BOTTOM) != 0)
            {
                y = -psize.y + start.y + size.y / 2f + padLeftBottom.y;
                setY = true;
            }

            if ((align & Aligns.CENTERH) != 0)
            {
                x = psize.x / 2f - start.x /*+ size.x / 2f - padLeftBottom.x*/;
                setX = true;
            }

            if ((align & Aligns.CENTERV) != 0)
            {
                y = -psize.y / 2f + start.y + size.y / 2f + padRightTop.y;
                setY = true;
            }

            //            if (!_logged)
            //            {
            //                _logged = true;
            //
            //                Debug.LogFormat("PSize: {0}. Size: {1}. Anchor: {2} ({5}). xy: {3}x{4}. Padding: {6} / {7}",
            //                psize, size, Transform.anchorMin, x, y, start, padLeftBottom, padRightTop);
            //            }

            Vector2 target = Transform.anchoredPosition;

            if (setX)
                target.x = x;

            if (setY)
                target.y = y;

            Transform.anchoredPosition = target;

            return this;
        }

        public BaseControl ToLeftOf(BaseControl anchor)
        {
            RectTransform anchorTransform = anchor.Transform;
            Vector2 pos = anchorTransform.anchoredPosition;
            pos.x = pos.x - anchorTransform.sizeDelta.x / 2f - Transform.sizeDelta.x / 2f;
            Transform.anchoredPosition = pos;
            return this;
        }

        public BaseControl ToBelowOf(BaseControl anchor)
        {
            RectTransform anchorTransform = anchor.Transform;
            Vector2 src = anchorTransform.anchoredPosition;

            Vector2 target = Transform.anchoredPosition;
            target.y = src.y - anchorTransform.sizeDelta.y / 2f - Transform.sizeDelta.y / 2f;

            Transform.anchoredPosition = target;
            return this;
        }

        public BaseControl ToVerticalBaselineOf(BaseControl anchor)
        {
            Vector2 target = Transform.anchoredPosition;
            target.x = anchor.Transform.anchoredPosition.x;
            Transform.anchoredPosition = target;
            return this;
        }

        // Set.

        public BaseControl SetName(string name)
        {
            GameObject.name = name.Replace(" ", "");
            return this;
        }

        public BaseControl SetText(string text)
        {
            throw new NotImplementedException();
        }

        public BaseControl SetParent(GameObject parent)
        {
            Transform.SetParent(parent.transform, false);
            return this;
        }

        public BaseControl SetPosition(float x, float y)
        {
            Transform.anchoredPosition = new Vector2(x, y);
            return this;
        }


        // Padding.

        public BaseControl PaddingLeft(float padding)
        {
            return Padding(padding, 0f);
        }

        public BaseControl PaddingRight(float padding)
        {
            return Padding(-padding, 0f);
        }

        public BaseControl PaddingtTop(float padding)
        {
            return Padding(0f, -padding);
        }

        public BaseControl PaddingBottom(float padding)
        {
            return Padding(0f, padding);
        }

        // Get.

        public abstract string GetText();

        public UnityEngine.Object GetComponent()
        {
            return null;
        }

        // Protected.

        protected Vector2 ParentSize()
        {
            return MathUtils.GetSize(ParentTransform().rect);
        }

        protected RectTransform ParentTransform()
        {
            if (_parentTransform == null)
                _parentTransform = Transform.parent.GetComponent<RectTransform>();

            return _parentTransform;
        }

        protected BaseControl Padding(float x, float y)
        {
            Vector2 pos = Transform.anchoredPosition;
            pos.x += x;
            pos.y += y;
            Transform.anchoredPosition = pos;
            return this;
        }
    }
}