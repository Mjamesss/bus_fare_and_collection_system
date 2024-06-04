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
    public partial class terminals : Form
    {

        public terminals()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void terminals_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of the next form
            TerminalChoice nextForm = new TerminalChoice();

            // Show the next form
            nextForm.Show();

            // Optionally, hide or close the current form
            this.Hide(); // or this.Close() if you want to close the current form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cubao_baler nextForm = new Cubao_baler();
            nextForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cubao_bolinao nextForm = new Cubao_bolinao();
            nextForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This terminal is not available.", "Terminal Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This terminal is not available.", "Terminal Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void guna2Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This terminal is not available.", "Terminal Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            // Create an instance of the next form
            TerminalChoice nextForm = new TerminalChoice();

            // Show the next form
            nextForm.Show();

            // Optionally, hide or close the current form
            this.Hide(); // or this.Close() if you want to close the current form
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Cubao_mariveles nextForm = new Cubao_mariveles();
            nextForm.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This terminal is not available.", "Terminal Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This terminal is not available.", "Terminal Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
