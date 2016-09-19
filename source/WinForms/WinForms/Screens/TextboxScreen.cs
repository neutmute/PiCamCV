using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.WinForms
{
    class TextboxScreen : IScreen
    {
        private readonly TextBox _textBox;
        public TextboxScreen(TextBox textbox)
        {
            _textBox = textbox;
        }

        public void Clear()
        {
            SetText(string.Empty);
        }

        public void BeginRepaint()
        {
            Clear();
        }

        public void WriteLine(string format, params object[] args)
        {
            string message;
            if (args.Length == 0)
            {
                message = format;
            }
            else
            {
                message = string.Format(format, args);
            }

            SetText(_textBox.Text + "\r\n" + message);
        }

        private void SetText(string text)
        {
            _textBox.Invoke((MethodInvoker)(() => { _textBox.Text = text; }), new object[0]);
        }
    }
}
