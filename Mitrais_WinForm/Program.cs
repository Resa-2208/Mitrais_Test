using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mitrais_WinForm
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

            if (string.IsNullOrEmpty(Mitrais_WinForm.Properties.Settings.Default.IPaddress))
            {
                Application.Run(new Config());
            }
            else
            {
                Application.Run(new frmRegistration());
            }
        }
    }
}
