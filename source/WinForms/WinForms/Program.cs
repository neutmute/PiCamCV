using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Logging;

namespace WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ILog log = LogManager.GetLogger("WinForms");
            try
            {
                Application.Run(new MainForm());
            }
            catch(Exception e)
            {
                log.Fatal(e.Message, e);
            }
        }
    }
}
