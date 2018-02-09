using ProgressBar.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar
{
    public abstract class AbstractProgressBehaviour : MonoBehaviour
    {
        private float _value;
        private int _valueInPercent, _transitoryValueInPercent;
        private float _transitoryValueFloat01;

        [SerializeField]
        public Text AttachedText = null;

        public int ProgressSpeed = 1;

        public bool TriggerOnComplete = true;

        [SerializeField]
        public OnCompleteEvent OnCompleteMethods = null;

        #region Properties

        public float Value
        {
            get { return _value; }
            set
            {
                _value = Mathf.Clamp01(value);
                _valueInPercent = (int)(_value * 100f);
            }
        }

        public int ValueInPercent
        {
            get { return _valueInPercent; }
            set
            {
                _valueInPercent = Mathf.Clamp(value, 0, 100);
                _value = _valueInPercent / 100f;
            }
        }

        public float TransitoryValue
        {
            get { return _transitoryValueFloat01; }
            set
            {
                _transitoryValueFloat01 = Mathf.Clamp01(value);
                _transitoryValueInPercent = (int)(_transitoryValueFloat01 * 100f);
                UpdateFillerSize();
            }
        }

        public int TransitoryValueInPercent
        {
            get { return _transitoryValueInPercent; }
            set
            {
                _transitoryValueInPercent = value;
                _transitoryValueFloat01 = _transitoryValueInPercent / 100f;
                UpdateFillerSize();
            }
        }

        public bool IsDone
        {
            get { return _valueInPercent == 100; }
        }

        public bool IsPaused
        {
            get { return _valueInPercent == _transitoryValueInPercent; }
        }

        #endregion

        public void IncrementValue(float inc)
        {
            Value = _value + inc;
        }

        public void DecrementValue(float dec)
        {
            Value = _value - dec;
        }

        void Start()
        {
            UpdateFillerSize();
        }

        void Update()
        {
            if (IsPaused)
                return;

            //            Tracks.StackTrace("_valueInPercent: {0}. _transitoryValueInPercent : {1}", _valueInPercent, _transitoryValueInPercent);

            int dvalue = _valueInPercent - _transitoryValueInPercent;

            if (dvalue > 0)
                _transitoryValueFloat01 += ProgressSpeed * Time.deltaTime;
            else
                _transitoryValueFloat01 -= ProgressSpeed * Time.deltaTime;

            _transitoryValueFloat01 = Mathf.Clamp01(_transitoryValueFloat01);
            _transitoryValueInPercent = (int)(_transitoryValueFloat01 * 100f);

            if (_transitoryValueInPercent > _valueInPercent)
            {
                _transitoryValueInPercent = _valueInPercent;
                _transitoryValueFloat01 = _transitoryValueInPercent / 100f;
            }

            UpdateFillerSize();

            if (TriggerOnComplete && IsPaused && IsDone)
                OnCompleteMethods.Invoke();
        }

        protected void UpdateFillerSize()
        {
            if (AttachedText)
                AttachedText.text = _transitoryValueInPercent + " %";

            UpdateFillerSizeInternal(_transitoryValueInPercent, _transitoryValueFloat01);
        }

        protected abstract void UpdateFillerSizeInternal(int percent, float value);
    }
}