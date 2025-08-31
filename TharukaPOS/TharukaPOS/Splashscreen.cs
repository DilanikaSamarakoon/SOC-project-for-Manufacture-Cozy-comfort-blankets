using System;
using System.Windows.Forms;
using TharukaPOS.Forms; // Namespace for LoginForm
using TharukaPOS.Forms._Repositories; // Namespace for DataAccess


namespace TharukaPOS
{
    public partial class Splashscreen : Form
    {
        private readonly string _connectionString; // To hold the connection string

        public Splashscreen(string connectionString) // Constructor to receive connection string
        {
            InitializeComponent();
            _connectionString = connectionString; // Store it
            timer1.Enabled = true;
        }

        // Keep default constructor for designer if needed
        public Splashscreen() { InitializeComponent(); }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                this.Close(); // <--- MODIFIED: Simply close the splash screen
            }
        }
    }
}