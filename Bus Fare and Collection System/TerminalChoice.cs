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
    public partial class TerminalChoice : Form
    {
        public string BusId { get; set; }

        public TerminalChoice()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void UpdateLabel(string busId)
        {
            // Update the label with the bus ID
            label1.Text = "Hi, " + busId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Navigate to the terminals form
            terminals terminalsForm = new terminals();
            terminalsForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Navigate to the main page form
            MainPage mainPageForm = new MainPage();
            mainPageForm.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
          
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            terminals mainPageForm = new terminals();
            mainPageForm.Show();
            this.Hide();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            MainPage nextForm = new MainPage();
            nextForm.Show();
            this.Hide();
        }

        private void TerminalChoice_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void guna2Button2_Click_2(object sender, EventArgs e)
        {
            History history = new History();
            history.Show();
            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Perform logout action here if the user confirms
                MessageBox.Show("You have logged out successfully.");

                // Show the next form and hide the current one
                MainPage nextForm = new MainPage();
                nextForm.Show();
                this.Hide();

                // Optionally, close the application or redirect to a login form
                // For example:
                // this.Close(); // if you want to close the application
            }
        }
    }
}

