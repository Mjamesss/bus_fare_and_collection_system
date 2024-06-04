using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Bus_Fare_and_Collection_System
{
    public partial class register_bus : Form
    {
        private readonly List<string> validBusIds = new List<string> { "659876", "632456", "632786", "698326", "675146", "693826", "621336" };
        string connectionString = "server=127.0.0.1;user=root;database=bus_fare_and_collection;port=3306;password=thesis@2023";

        public register_bus()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            // Attach the KeyPress event handler to the txtBusId textbox
            txtBusId.KeyPress += TxtBusId_KeyPress;

            // Attach TextChanged event handlers for real-time validation
            txtPassword.TextChanged += TxtPassword_TextChanged;
            txtConfirmPassword.TextChanged += TxtPassword_TextChanged;
        }

        private void TxtBusId_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Ensure textBox is not null
            if (textBox == null)
            {
                return;
            }

            // Allow digits, backspace, and delete keys
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
            {
                e.Handled = true; // Ignore the key press
                return;
            }

            // Limit to 6 characters
            if (textBox.Text.Length >= 6 && !(e.KeyChar == 8 || e.KeyChar == 127))
            {
                e.Handled = true; // Ignore the key press
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            // Enable or disable the sign-up button based on password validity
            btnSignUp.Enabled = IsPasswordValid(txtPassword.Text);

            // Optionally, display a note to the user if the password is invalid
            lblPasswordNote.Visible = !IsPasswordValid(txtPassword.Text);
        }

        private bool IsPasswordValid(string password)
        {
            // Ensure the password is at least 8 characters long
            if (password.Length < 8)
            {
                return false;
            }

            // Ensure the password contains at least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Ensure the password contains at least one special character
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return false;
            }

            return true;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string busIdGiven = txtBusId.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string firstname = txtFirstName.Text;
            string lastname = txtLastName.Text;

            // Check if the bus ID is in the list of valid bus IDs
            if (!validBusIds.Contains(busIdGiven))
            {
                MessageBox.Show("Invalid Bus ID. Please enter a valid Bus ID.");
                return;
            }

            // Perform other validations
            if (string.IsNullOrEmpty(busIdGiven) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Validate the password
            if (!IsPasswordValid(password))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain at least one uppercase letter, and one special character.");
                return;
            }

            // Hash the password before storing it
            string hashedPassword = HashPassword(password);

            // Assuming you have a Users table with columns (busIdGiven, password, firstname, lastname)
            string query = "INSERT INTO Users (busIdGiven, password, firstname, lastname) VALUES (@busIdGiven, @password, @firstname, @lastname)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@busIdGiven", busIdGiven);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("User signed up successfully.");
                        MainPage nextForm = new MainPage();
                        nextForm.Show();
                        this.Hide();
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
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MainPage nextForm = new MainPage();
            nextForm.Show();
            this.Hide();
        }

        private void register_bus_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void register_bus_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtBusId_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a digit or control key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If not a digit, set handled to true to cancel the event
                e.Handled = true;
            }
        }

        private void txtLastName_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a digit or control key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If not a digit, set handled to true to cancel the event
                e.Handled = true;
            }
        }

        private void txtBusId_TextChanged(object sender, EventArgs e)
        {

        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                txtPassword.PasswordChar = '•'; // or any character you use for masking
                guna2Button2.Text = "Show Password"; // Change button text accordingly
            }
            else
            {
                txtPassword.PasswordChar = '\0'; // '\0' means no masking
                guna2Button2.Text = "Hide Password"; // Change button text accordingly
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Toggle the PasswordChar property to show/hide password for txtConfirmPassword
            if (txtConfirmPassword.PasswordChar == '\0')
            {
                txtConfirmPassword.PasswordChar = '•'; // or any character you use for masking
                guna2Button3.Text = "Show Password"; // Change button text accordingly
            }
            else
            {
                txtConfirmPassword.PasswordChar = '\0'; // '\0' means no masking
                guna2Button3.Text = "Hide Password"; // Change button text accordingly
            }
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
