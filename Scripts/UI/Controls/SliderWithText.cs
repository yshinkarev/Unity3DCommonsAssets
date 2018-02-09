using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI
{
    public class SliderWithText : MonoBehaviour
    {
        public string FormatText = "{0.00}";

        private Slider _slider;
        private Text _text;

        protected void Awake()
        {
            _text = GetComponentInChildren<Text>();
            _slider = GetComponentInChildren<Slider>();
            _slider.onValueChanged.AddListener(OnChangeValue);
        }

        public float Value
        {
            get { return _slider.value; }
            set
            {
                _slider.value = value;
                OnChangeValue(value);
            }
        }

        public Slider Slider
        {
            get { return _slider; }
        }

        public Text Text
        {
            get { return _text; }
        }

        private void OnChangeValue(float value)
        {
            _text.text = string.Format(FormatText, value);
        }
    }
}