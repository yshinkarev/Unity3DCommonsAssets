using System;
using System.Text;
using Assets.Commons_Assets.Scripts.Entity;
using Commons.UI;
using Commons.Utils;
using UnityEngine;

namespace Commons.Debugging.Logging
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class GuiLoggerImpl : IGuiLogger
    {
        private GUIStyle _style;
        private Rect _rect;

        private Color _backgroundColor;
        private Texture2D _background;
        private Vector2 _position;
        private IntVector2 _sizeInText = new IntVector2(0, 0);
        private int _positionAling;
        private string _labelFontName;
        private int _labelFontSize;
        private Color _labelFontColor = MyColor.None;

        private readonly Func<string> _getMessageCallback;
        private readonly Func<Rect> _getRectCallback;

        public GuiLoggerImpl(Func<string> getMessageCallback, Func<Rect> getRectCallback = null)
        {
            _getMessageCallback = getMessageCallback;
            _getRectCallback = getRectCallback;

        }

        public void Log()
        {
            if (_style == null)
            {
                _style = GetGuiStyle();
                _rect = GetRect();

                if (_backgroundColor != MyColor.None)
                {
                    _background = new Texture2D(1, 1);
                    _background.SetPixel(0, 0, _backgroundColor);
                    _background.Apply();
                    _style.normal.background = _background;
                }
            }

            LogInternal();
        }

        #region Properties.

        public Color BackgroundColor
        {
            set { _backgroundColor = value; }
        }

        public IntVector2 SizeInText
        {
            set { _sizeInText = value; }
        }

        public Vector2 Position
        {
            set { _position = value; }
        }

        public int PositionAlign
        {
            set { _positionAling = value; }
        }

        public string LabelFontName
        {
            set { _labelFontName = value; }
        }

        public int LabelFontSize
        {
            set { _labelFontSize = value; }
        }

        public Color LabelFontColor
        {
            set { _labelFontColor = value; }
        }

        #endregion

        #region Override.

        protected virtual Rect GetRect()
        {
            if (_getRectCallback != null)
                return _getRectCallback();

            Vector2 size;

            if (_sizeInText.IsNulled)
                size = new Vector2(Screen.width / 2f, Screen.height / 2f);
            else
                size = _style.CalcSize(new GUIContent(GetTextForCalc()));

            if (_positionAling != 0)
                _position = UpdatePositionByAlign(size);

            return new Rect(_position, size);
        }

        protected virtual GUIStyle GetGuiStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            OnApplyStyle(style);
            return style;
        }

        protected virtual void OnApplyStyle(GUIStyle style)
        {
            if (!string.IsNullOrEmpty(_labelFontName))
                style.font = Resources.Load<Font>(_labelFontName);

            if (_labelFontColor != MyColor.None)
                style.normal.textColor = _labelFontColor;

            if (_labelFontSize > 0)
                style.fontSize = _labelFontSize;
        }

        protected virtual void LogInternal()
        {
            if (_background != null)
                GUI.Box(_rect, GUIContent.none, _style);

            string text = _getMessageCallback();
            GUI.Label(_rect, text, _style);
        }

        #endregion

        #region Private.

        private String GetTextForCalc()
        {
            StringBuilder sb = new StringBuilder();

            for (int col = 0; col < _sizeInText.X; col++)
                sb.Append("W");

            for (int row = 0; row < _sizeInText.Y - 1; row++)
                sb.AppendLine();

            return sb.ToString();
        }

        private Vector2 UpdatePositionByAlign(Vector2 size)
        {
            Vector2 result = Vector2.zero;

            if ((_positionAling & Aligns.LEFT) != 0)
                result.x = 0f;
            else if ((_positionAling & Aligns.RIGHT) != 0)
                throw new NotImplementedException();

            if ((_positionAling & Aligns.TOP) != 0)
                result.y = 0f;
            else if ((_positionAling & Aligns.BOTTOM) != 0)
                result.y = Screen.height - size.y;

            if ((_positionAling & Aligns.CENTERH) != 0)
                throw new NotImplementedException();

            if ((_positionAling & Aligns.CENTERV) != 0)
                throw new NotImplementedException();

            return result;
        }

        #endregion
    }
}