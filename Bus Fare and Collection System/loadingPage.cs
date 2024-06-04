using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Bus_Fare_and_Collection_System
{
    public partial class loadingPage : Form
    {
        //round form code starts here
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        private void LoadingPage_HandleCreated(object sender, EventArgs e)
        {
            // Create a rounded region for the form
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); //rounded form
        }

        public loadingPage()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.HandleCreated += LoadingPage_HandleCreated; // Subscribe to HandleCreated event
        }

        private void loadingPage_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 1;
            }
            else
            {
                timer1.Stop(); // Stop the timer when progress reaches 100%
                MainPage form2 = new MainPage(); // Create an instance of Form2
                form2.Show(); // Show Form2
                this.Hide(); // Hide the current form (loadingPage)
            }

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
