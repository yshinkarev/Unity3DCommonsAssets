using System;
using System.Text;
using UnityEngine;

namespace Commons.Debugging.Logging
{
    public class GuiLogger : IGuiLogger
    {
        private readonly StringBuilder _sb;
        private readonly Rect _rect;
        private bool _enable;
        private string _fileName;

        public GuiLogger()
        {
            _sb = new StringBuilder();
            _rect = new Rect(5f, 5f, Screen.width - 5f, Screen.height - 5f);
            SetEnable(true);
        }

        public bool IsEnabled
        {
            get { return _enable; }
        }

        public GuiLogger SetEnable(bool enable)
        {
            //            Debug.LogFormat("Logger enable = {0}", enable);
            _enable = enable;
            return this;
        }

        public GuiLogger SetLogFileName(string fileName)
        {
            //            Debug.LogFormat("Logger fileName = {0}", fileName);
            _fileName = fileName;
            return this;
        }

        public GuiLogger Clear()
        {
            _sb.Length = 0;
            return this;
        }

        public GuiLogger ClearLogFile()
        {
            FileAppendLogger.Clear(_fileName);
            return this;
        }

        public GuiLogger AppendFormatLine(String format, params System.Object[] args)
        {
            AppendFormat(format, args);
            return AppendLine();
        }

        public GuiLogger AppendFormat(String format, params System.Object[] args)
        {
            if (!_enable)
                return this;

            _sb.AppendFormat(format, args);
            return this;
        }

        public GuiLogger AppendLine()
        {
            if (!_enable)
                return this;

            _sb.AppendLine();
            return this;
        }

        public GuiLogger Append(object obj)
        {
            if (!_enable)
                return this;

            _sb.Append(obj);
            return this;
        }

        public void Log()
        {
            if (!_enable)
                return;

            GUI.contentColor = Color.red;
            string text = _sb.ToString();

            if (string.IsNullOrEmpty(text))
                return;

            GUI.Label(_rect, text);

            FileAppendLogger.LogF(_fileName, text);

            Debug.Log(text);
        }
    }
}