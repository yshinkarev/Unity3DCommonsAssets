using System;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI.Helper.Position
{
    public static class PositionUiSetterFactory
    {
        public static AbstractPositionUiSetter Create(GameObject go)
        {
            Canvas canvas = go.GetComponentInParent<Canvas>();
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

            switch (scaler.uiScaleMode)
            {
                case CanvasScaler.ScaleMode.ScaleWithScreenSize:
                    return new ScreenSizePositionUiSetter(go, canvas, scaler);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}