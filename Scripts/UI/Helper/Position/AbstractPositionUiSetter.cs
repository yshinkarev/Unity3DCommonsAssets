using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI.Helper.Position
{
    public abstract class AbstractPositionUiSetter
    {
        private readonly RectTransform _transform;
        private Vector2 _position;

        protected AbstractPositionUiSetter(GameObject go, Canvas canvas, CanvasScaler scaler)
        {
            _transform = go.transform as RectTransform;
        }

        public abstract float GetScaleFactor();

        public void SetPosition(float x, float y)
        {
            GetPosition(x, y, out _position);
            _transform.anchoredPosition = _position;
        }

        public void SetPosition(Vector2 position)
        {
            SetPosition(position.x, position.y);
        }

        protected void GetPosition(float x, float y, out Vector2 pos)
        {
            pos.x = x * GetScaleFactor();
            pos.y = y * GetScaleFactor();
        }
    }
}