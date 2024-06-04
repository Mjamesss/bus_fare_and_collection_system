using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mysqlx.Crud;

namespace Bus_Fare_and_Collection_System
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }



        private void History_Load(object sender, EventArgs e)

        {



            string connectionString = "server=localhost;database=bus_fare_and_collection;port=3306;user=root;password=;Allow Zero Datetime=True;Convert Zero Datetime=True";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
        SELECT id, 
               Route, 
               SeatNo, 
               Fare, 
               BusId,
               CASE 
                   WHEN DateTime = '0000-00-00 00:00:00' THEN NULL 
                   ELSE DateTime 
               END as DateTime 
        FROM records";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        guna2DataGridView1.DataSource = dataTable;
                    }
                }
            }

        }

        MySqlConnection con = new MySqlConnection("server=localhost;database=bus_fare_and_collection;port=3306;user=root;password=;Allow Zero Datetime=True;Convert Zero Datetime=True");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
        public void searchData(string valueToSearch)
        {
           
            string query = @"
            SELECT id,
                   Route,
                   SeatNo,
                   Fare,
                   BusId,
                   CASE
                       WHEN DateTime = '0000-00-00 00:00:00' THEN NULL
                       ELSE DateTime
                   END as DateTime
            FROM records
            WHERE 
                id LIKE @valueToSearch OR
                Route LIKE @valueToSearch OR
                SeatNo LIKE @valueToSearch OR
                Fare LIKE @valueToSearch OR
                BusId LIKE @valueToSearch OR
                DateTime LIKE @valueToSearch";

            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                // Using parameterized query to prevent SQL injection
                command.Parameters.AddWithValue("@valueToSearch", "%" + valueToSearch + "%");

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView1.DataSource = dataTable;
                }
            }
        }
    






        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string valueToSearch = textBoxValueToSearch.Text.ToString();
            searchData(valueToSearch);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=bus_fare_and_collection;port=3306;user=root;password=;Allow Zero Datetime=True;Convert Zero Datetime=True";
            string usernameToDelete = textBoxValueToSearch.Text; // Assuming you have a TextBox named usernameTextBox

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM records WHERE Route = @Route";
                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Route", usernameToDelete);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Account deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No account found with the given username.");
                    }
                }
            }
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
    }
}

