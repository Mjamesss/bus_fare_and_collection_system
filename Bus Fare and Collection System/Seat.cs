using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bus_Fare_and_Collection_System
    
{

    public delegate void ConfirmClickedEventHandler(object sender, string value);
    public partial class Seat : Form
    {
        // Event handler for confirmBtn click event
        public Seat()
        {
            InitializeComponent();
            confirmBtn.Click += confirmBtn_Click;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Seat_Load(object sender, EventArgs e)
        {

        }

        private void sit1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sitTextBox.Text))
            {
                sitTextBox.Text += " , ";
            }
            sitTextBox.Text += "1";

            // Disable the button to prevent further clicks
            sit1.Enabled = false;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
                // Clear the text of guna2TextBox1
                sitTextBox.Text = string.Empty;

            // Re-enable the sit1 button
            sit1.Enabled = true; // 1
            sit2.Enabled = true; // 2
            sit3.Enabled = true; // 3
            sit4.Enabled = true; // 4
            sit5.Enabled = true; // 5
            sit6.Enabled = true; // 6
            sit7.Enabled = true; // 7
            sit8.Enabled = true; // 8
            sit9.Enabled = true; // 9
            sit10.Enabled = true; // 10
            sit11.Enabled = true; // 11
            sit12.Enabled = true; // 12
            sit13.Enabled = true; // 13
            sit14.Enabled = true; // 14
            sit15.Enabled = true; // 15
            sit16.Enabled = true; // 16
            sit17.Enabled = true; // 17
            sit18.Enabled = true; // 18
            sit19.Enabled = true; // 19
            sit20.Enabled = true; // 20
            sit21.Enabled = true; // 21
            sit22.Enabled = true; // 22
            sit23.Enabled = true; // 23
            sit24.Enabled = true; // 24
            sit25.Enabled = true; // 25
            sit26.Enabled = true; // 26
            sit27.Enabled = true; // 27
            sit28.Enabled = true; // 28
            sit29.Enabled = true; // 29
            sit30.Enabled = true;
        }

        private void sit2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sitTextBox.Text))
            {
                sitTextBox.Text += " , ";
            }
            sitTextBox.Text += "2";

            // Disable the button to prevent further clicks
            sit2.Enabled = false;
        }
        public event ConfirmClickedEventHandler ConfirmClicked;

        // Invoke the event when the "Confirm" button is clicked
        private void confirmBtn_Click(object sender, EventArgs e)
        { 
            this.Close();
        }
    }
}
