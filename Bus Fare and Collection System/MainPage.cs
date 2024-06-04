using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Bus_Fare_and_Collection_System
{
    public partial class MainPage : Form
    {
        string connectionString = "server=127.0.0.1;user=root;database=bus_fare_and_collection;port=3306;password=thesis@2023";


        public MainPage()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            txtBusId.KeyPress += txtBusId_KeyPress;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void txtBusId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (txtBusId.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string busId = txtBusId.Text;
            string password = txtPassword.Text;

            string query = "SELECT COUNT(*) FROM Users WHERE busIdGiven = @busId AND password = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@busId", busId);
                    command.Parameters.AddWithValue("@password", password);

                    try
                    {
                        connection.Open();
                        int userCount = Convert.ToInt32(command.ExecuteScalar());

                        if (userCount > 0)
                        {
                            GlobalVariables.BusId = busId;
                            // User exists, proceed to the next form
                            TerminalChoice nextForm = new TerminalChoice();
                            nextForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid bus ID or password. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBusId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Call the login logic directly
                btnLogin_Click(sender, e);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            register_bus nextForm = new register_bus();
            nextForm.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
           
            DialogResult result = MessageBox.Show("Are you sure you want to close this app?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Perform logout action here if the user confirms
                MessageBox.Show("You close app succesfully.");

                // Show the next form and hide the current one
                this.Hide();
            }
        }

        private void btnConfim_Click(object sender, EventArgs e)
        {

            string busId = txtBusId.Text;
            string password = txtPassword.Text;

            string query = "SELECT COUNT(*) FROM Users WHERE busIdGiven = @busId AND password = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@busId", busId);

                    // Hash the password before comparing it with the hashed password in the database
                    string hashedPassword = HashPassword(password);
                    command.Parameters.AddWithValue("@password", hashedPassword);

                    try
                    {
                        connection.Open();
                        int userCount = Convert.ToInt32(command.ExecuteScalar());

                        if (userCount > 0)
                        {
                            // User exists, proceed to the next form
                            TerminalChoice nextForm = new TerminalChoice();
                            nextForm.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("Invalid bus ID or password. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();

                foreach (var b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            
        }

        private void eye_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                txtPassword.PasswordChar = '•'; // or any character you use for masking
                eye.Text = "Show Password"; // Change button text accordingly
            }
            else
            {
                txtPassword.PasswordChar = '\0'; // '\0' means no masking
                eye.Text = "Hide Password"; // Change button text accordingly
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void eye_Click_1(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                txtPassword.PasswordChar = '•'; // or any character you use for masking
                eye.Text = "Show Password"; // Change button text accordingly
            }
            else
            {
                txtPassword.PasswordChar = '\0'; // '\0' means no masking
                eye.Text = "Hide Password"; // Change button text accordingly
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the 'ding' sound when Enter is pressed
                btnConfim.PerformClick();
            }
        }
    }
}
