using System;
using System.Collections.Generic;
using Commons.Debugging.SettingsEditor.Interfaces;
using Commons.UI;
using Commons.Utils;
using UnityEngine;

namespace Commons.Debugging.SettingsEditor.UI
{
    public abstract class AbstractSettingsUi : UiControls, IEnumerateAllParams, IApplyParamControlValues
    {
        protected readonly GameObject Panel;

        private readonly Dictionary<string, BaseControl> _controls = new Dictionary<string, BaseControl>();

        private BaseControl _ctrl;
        private readonly Dictionary<Type, float> _leftPadding = new Dictionary<Type, float>();

        protected AbstractSettingsUi(MonoBehaviourSettings settings, GameObject panel)
        {
            Panel = panel;
            settings.EnumerateAllParams(this);
            settings.ApplyParamControlValuesCallback = this;

            float bottom = UiUtils.GetBottomOfControl(_ctrl.Transform);
            UiUtils.ChangeAnchoredPosition(Panel.transform, 0f, -bottom / 2f);
            UiUtils.ChangeSizeDelta(Panel.transform, 0f, bottom);

            Panel = null;
        }

        public abstract void OnBool(string name, bool value);

        public void OnInt(string name, int value, int min, int max)
        {
            OnFloat(name, value, min, max, true);
        }

        public abstract void OnFloat(string name, float value, float min, float max, bool wholeNumbers);

        public object GetValue(string name)
        {
            _ctrl = U.GetValueOrThrow(_controls, name);

            if (_ctrl is MyToggle)
                return ((MyToggle)_ctrl).IsOn;

            if (_ctrl is MySliderWithText)
                return ((MySliderWithText)_ctrl).GetComponent().Value;

            throw new Exception(name + ": " + TypesUtils.GetSimpleClassName(_ctrl));
        }

        protected float GetLeftPaddingOf(BaseControl ctrl)
        {
            if (ctrl is MyToggle)
                return 30f;

            if (ctrl is MySliderWithText)
                return 20f;

            throw new NotImplementedException();
        }

        protected void OnCreate(BaseControl ctrl, string name)
        {
            float left;

            if (!_leftPadding.TryGetValue(ctrl.GetType(), out left))
            {
                ctrl.Align(Aligns.LEFT);

                left = GetLeftPaddingOf(ctrl) + ctrl.Transform.anchoredPosition.x;
                _leftPadding.Add(ctrl.GetType(), left);
            }

            if (_ctrl == null)
                ctrl.Align(Aligns.TOP).PaddingtTop(20f);
            else
                ctrl.ToBelowOf(_ctrl);

            UiUtils.ChangeAnchoredPositionX(ctrl.Transform, left);

            _ctrl = ctrl;
            _controls.Add(name, ctrl);
        }
    }
}