using System;
using System.Windows.Forms;

// Ensure this namespace matches the namespace used in your Form1.cs and other files.
namespace DistributorDashboard
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

            // This line will now work correctly because Program and Form1 are in the same namespace.
            Application.Run(new Form1());
        }
    }
}
