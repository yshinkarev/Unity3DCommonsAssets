using UnityEngine;
using UnityEngine.UI;

namespace Commons.Utils
{
    public static class UiUtils
    {
        private static Vector2 _scaledResolution;

        public static Vector2 GetScaledResolution(Canvas canvas)
        {
            if (_scaledResolution.x < 1f)
                _scaledResolution = canvas.GetComponent<CanvasScaler>().referenceResolution;

            return _scaledResolution;
        }

        public static Vector2 GetSizeOfRectTransform(GameObject go)
        {
            return MathUtils.GetSize((go.transform as RectTransform).rect);
        }

        public static Vector2 ChangeAnchoredPositionX(RectTransform transform, float x)
        {
            Vector2 position = transform.anchoredPosition;
            position.x = x;
            transform.anchoredPosition = position;
            return position;
        }

        public static void ChangeAnchoredPosition(Transform transform, float dx, float dy)
        {
            ChangeAnchoredPosition(transform as RectTransform, dx, dy);
        }

        public static void ChangeAnchoredPosition(RectTransform transform, float dx, float dy)
        {
            Vector2 pos = transform.anchoredPosition;
            pos.x += dx;
            pos.y += dy;
            transform.anchoredPosition = pos;
        }

        public static void SetAnchoredPosition(Transform transform, float x, float y)
        {
            SetAnchoredPosition(transform as RectTransform, x, y);
        }

        public static void SetAnchoredPosition(RectTransform transform, float x, float y)
        {
            Vector2 pos = transform.anchoredPosition;
            pos.x = x;
            pos.y = y;
            transform.anchoredPosition = pos;
        }

        public static void ChangeSizeDelta(Transform transform, float top, float bottom)
        {
            ChangeSizeDelta(transform as RectTransform, top, bottom);
        }

        public static void ChangeSizeDelta(RectTransform transform, float top, float bottom)
        {
            Vector2 size = transform.sizeDelta;
            size.x += top;
            size.y += bottom;
            transform.sizeDelta = size;
        }

        public static float GetBottomOfControl(Transform transform)
        {
            return GetBottomOfControl(transform as RectTransform);
        }

        public static float GetBottomOfControl(RectTransform transform)
        {
            return Mathf.Abs(transform.anchoredPosition.y) + transform.sizeDelta.y;
        }
    }
}