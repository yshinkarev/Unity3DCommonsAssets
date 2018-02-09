using System;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI.Helper.Position
{
    public class ScreenSizePositionUiSetter : AbstractPositionUiSetter
    {
        private readonly float _scaleFactor;

        public ScreenSizePositionUiSetter(GameObject go, Canvas canvas, CanvasScaler scaler)
            : base(go, canvas, scaler)
        {
            if (scaler.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
            {
                // TODO
                if (scaler.matchWidthOrHeight < 0.01f)
                    _scaleFactor = scaler.referenceResolution.x / Screen.width;
                else if (scaler.matchWidthOrHeight > 0.99f)
                    _scaleFactor = scaler.referenceResolution.y / Screen.height;

                //                _perUnit = Mathf.Pow(2f, Mathf.Lerp(Mathf.Log(Screen.width / scaler.referenceResolution.x, 2f),
                //              Mathf.Log(Screen.height / scaler.referenceResolution.y, 2f), 1f - scaler.matchWidthOrHeight));
            }
            else
                throw new NotImplementedException();
        }

        public override float GetScaleFactor()
        {
            return _scaleFactor;
        }
    }
}