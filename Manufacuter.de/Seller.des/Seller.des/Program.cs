using System;
using System.Windows.Forms;

namespace Seller.des
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // FIX: Changed Form1() to MyStock() to run the correct main form.
            Application.Run(new MyStock());
        }
    }
}
