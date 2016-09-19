using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiCamCV.Common.Interfaces;
using Web.Client;

namespace PiCamCV.WinForms
{
    public class RemoteTextboxScreen : RemoteScreen
    {
        private TextboxScreen _textBox;

        public RemoteTextboxScreen(ICameraToServerBus cameraHubProxy, TextBox textbox) : base(cameraHubProxy)
        {
            _textBox = new TextboxScreen(textbox);
        }

        public override void WriteLine(string format, params object[] args)
        {
            base.WriteLine(format, args);
            _textBox.WriteLine(format, args);
        }
    }
}
