using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.IO;

namespace Bus_Fare_and_Collection_System
{

    public partial class Cubao_mariveles : Form
    {



        private PrintDocument printDocument1 = new PrintDocument();
        private const string connectionString = "server=127.0.0.1;user=root;database=bus_fare_and_collection;password=your_password;";

        private int discountClicks = 0;
        // Dictionary to map selections to fares (pamasahe)
        private Dictionary<string, Dictionary<string, decimal>> pamasahe = new Dictionary<string, Dictionary<string, decimal>>();
        private decimal baseFare;


        public Cubao_mariveles()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            guna2TextBox1.Text = "0"; // Set guna2TextBox1 to display '0'
            guna2TextBox1.TextChanged += textBox1_TextChanged;
            InitializePamasahe();
            printDocument2.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            Seat seatForm = new Seat();
            guna2ComboBox1.SelectedIndexChanged += ValidateInputs;
            guna2ComboBox2.SelectedIndexChanged += ValidateInputs;
            guna2TextBox1.TextChanged += ValidateInputs;
            guna2TextBox2.TextChanged += ValidateInputs;
            guna2Button5.Enabled = false;
            guna2Button6.Enabled = false;


        }
        private void ValidateInputs(object sender, EventArgs e)
        {
            // Check if all required fields have values
            guna2Button5.Enabled = !string.IsNullOrWhiteSpace(guna2ComboBox1.Text) &&
                                   !string.IsNullOrWhiteSpace(guna2ComboBox2.Text) &&
                                   !string.IsNullOrWhiteSpace(guna2TextBox1.Text) &&
                                   !string.IsNullOrWhiteSpace(guna2TextBox2.Text) &&
                                   !string.IsNullOrWhiteSpace(seatNum.Text);

            guna2Button6.Enabled = !string.IsNullOrWhiteSpace(guna2ComboBox1.Text) &&
                                   !string.IsNullOrWhiteSpace(guna2ComboBox2.Text) &&
                                   !string.IsNullOrWhiteSpace(guna2TextBox1.Text) &&
                                   !string.IsNullOrWhiteSpace(guna2TextBox2.Text) &&
                                   !string.IsNullOrWhiteSpace(seatNum.Text);
        }

        private void Cubao_mariveles_Load(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Get the current value of the text box
            int value;
            if (!int.TryParse(guna2TextBox1.Text, out value))
            {
                // If the input is not a valid integer, set the text to "0"
                guna2TextBox1.Text = "0";
            }
            else
            {
                // If the input is within the range 0-44, keep it unchanged
                // Otherwise, limit it to the range
                if (value < 1)
                {
                    guna2TextBox1.Text = "0";
                }
                else if (value > 44)
                {
                    guna2TextBox1.Text = "44";
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            terminals nextForm = new terminals();
            nextForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int currentValue))
            {
                int newValue = currentValue + 1;
                guna2TextBox1.Text = newValue.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int currentValue))
            {
                int newValue = currentValue - 1; // Change to currentValue - 1 for subtraction
                guna2TextBox1.Text = newValue.ToString();
            }
        }

        private void InitializePamasahe()
        {
            // Populate the pamasahe dictionary with fares for each combination
            pamasahe["Cubao"] = new Dictionary<string, decimal>();
            pamasahe["Cubao"]["San Fernando"] = 190;
            pamasahe["Cubao"]["Guagua"] = 222;
            pamasahe["Cubao"]["Lubao"] = 256;
            pamasahe["Cubao"]["Dinalupihan"] = 270;
            pamasahe["Cubao"]["Abucay"] = 293;
            pamasahe["Cubao"]["Balanga"] = 311;
            pamasahe["Cubao"]["Orion"] = 340;
            pamasahe["Cubao"]["Limay"] = 369;
            pamasahe["Cubao"]["Mariveles"] = 390;
            //
            pamasahe["San Fernando"] = new Dictionary<string, decimal>();
            pamasahe["San Fernando"]["Guagua"] = 100;
            pamasahe["San Fernando"]["Lubao"] = 154;
            pamasahe["San Fernando"]["Dinalupihan"] = 169;
            pamasahe["San Fernando"]["Abucay"] = 202;
            pamasahe["San Fernando"]["Balanga"] = 234;
            pamasahe["San Fernando"]["Orion"] = 258;
            pamasahe["San Fernando"]["Limay"] = 270;
            pamasahe["San Fernando"]["Mariveles"] = 290;
            pamasahe["San Fernando"]["Cubao"] = 190;
            //
            pamasahe["Guagua"] = new Dictionary<string, decimal>();
            pamasahe["Guagua"]["Lubao"] = 100;
            pamasahe["Guagua"]["Dinalupihan"] = 130;
            pamasahe["Guagua"]["Abucay"] = 156;
            pamasahe["Guagua"]["Balanga"] = 174;
            pamasahe["Guagua"]["Orion"] = 198;
            pamasahe["Guagua"]["Limay"] = 224;
            pamasahe["Guagua"]["Mariveles"] = 249;
            pamasahe["Guagua"]["San Fernando"] = 100;
            pamasahe["Guagua"]["Cubao"] = 200;
            //
            pamasahe["Lubao"] = new Dictionary<string, decimal>();
            pamasahe["Lubao"]["Dinalupihan"] = 100;
            pamasahe["Lubao"]["Abucay"] = 130;
            pamasahe["Lubao"]["Balanga"] = 156;
            pamasahe["Lubao"]["Orion"] = 174;
            pamasahe["Lubao"]["Limay"] = 198;
            pamasahe["Lubao"]["Mariveles"] = 224;
            pamasahe["Lubao"]["Guagua"] = 100;
            pamasahe["Lubao"]["San Fernando"] = 130;
            pamasahe["Lubao"]["Cubao"] = 230;
            //
            pamasahe["Dinalupihan"] = new Dictionary<string, decimal>();
            pamasahe["Dinalupihan"]["Abucay"] = 100;
            pamasahe["Dinalupihan"]["Balanga"] = 130;
            pamasahe["Dinalupihan"]["Orion"] = 156;
            pamasahe["Dinalupihan"]["Limay"] = 174;
            pamasahe["Dinalupihan"]["Mariveles"] = 198;
            pamasahe["Dinalupihan"]["Lubao"] = 100;
            pamasahe["Dinalupihan"]["Guagua"] = 130;
            pamasahe["Dinalupihan"]["San Fernando"] = 154;
            pamasahe["Dinalupihan"]["Cubao"] = 254;
            //
            pamasahe["Abucay"] = new Dictionary<string, decimal>();
            pamasahe["Abucay"]["Balanga"] = 100;
            pamasahe["Abucay"]["Orion"] = 130;
            pamasahe["Abucay"]["Limay"] = 156;
            pamasahe["Abucay"]["Mariveles"] = 174;
            pamasahe["Abucay"]["Dinalupihan"] = 100;
            pamasahe["Abucay"]["Lubao"] = 130;
            pamasahe["Abucay"]["Guagua"] = 156;
            pamasahe["Abucay"]["San Fernando"] = 174;
            pamasahe["Abucay"]["Cubao"] = 274;
            //
            pamasahe["Balanga"] = new Dictionary<string, decimal>();
            pamasahe["Balanga"]["Orion"] = 100;
            pamasahe["Balanga"]["Limay"] = 130;
            pamasahe["Balanga"]["Mariveles"] = 156;
            pamasahe["Balanga"]["Abucay"] = 100;
            pamasahe["Balanga"]["Dinalupihan"] = 130;
            pamasahe["Balanga"]["Lubao"] = 156;
            pamasahe["Balanga"]["Guagua"] = 174;
            pamasahe["Balanga"]["San Fernando"] = 198;
            pamasahe["Balanga"]["Cubao"] = 298;
            //
            pamasahe["Orion"] = new Dictionary<string, decimal>();
            pamasahe["Orion"]["Limay"] = 100;
            pamasahe["Orion"]["Mariveles"] = 130;
            pamasahe["Orion"]["Balanga"] = 100;
            pamasahe["Orion"]["Abucay"] = 130;
            pamasahe["Orion"]["Dinalupihan"] = 156;
            pamasahe["Orion"]["Lubao"] = 174;
            pamasahe["Orion"]["Guagua"] = 198;
            pamasahe["Orion"]["San Fernando"] = 224;
            pamasahe["Orion"]["Cubao"] = 324;
            //
            pamasahe["Limay"] = new Dictionary<string, decimal>();
            pamasahe["Limay"]["Mariveles"] = 100;
            pamasahe["Limay"]["Orion"] = 130;
            pamasahe["Limay"]["Balanga"] = 156;
            pamasahe["Limay"]["Abucay"] = 174;
            pamasahe["Limay"]["Dinalupihan"] = 198;
            pamasahe["Limay"]["Lubao"] = 224;
            pamasahe["Limay"]["Guagua"] = 249;
            pamasahe["Limay"]["San Fernando"] = 270;
            pamasahe["Limay"]["Cubao"] = 370;
            //
            pamasahe["Mariveles"] = new Dictionary<string, decimal>();
            pamasahe["Mariveles"]["Limay"] = 130;
            pamasahe["Mariveles"]["Orion"] = 156;
            pamasahe["Mariveles"]["Balanga"] = 174;
            pamasahe["Mariveles"]["Abucay"] = 198;
            pamasahe["Mariveles"]["Dinalupihan"] = 224;
            pamasahe["Mariveles"]["Lubao"] = 249;
            pamasahe["Mariveles"]["Guagua"] = 270;
            pamasahe["Mariveles"]["San Fernando"] = 290;
            pamasahe["Mariveles"]["Cubao"] = 390;
        }


        private void UpdateTotalFare()
        {
            if (int.TryParse(guna2TextBox1.Text, out int multiplier))
            {
                decimal totalFare = baseFare * multiplier;
                guna2TextBox2.Text = totalFare.ToString("C");
            }
        }
        //multiplier
        private void UpdateFareBasedOnMultiplier()
        {
            Console.WriteLine("UpdateFareBasedOnMultiplier called");

            if (decimal.TryParse(guna2TextBox2.Text, System.Globalization.NumberStyles.Currency, null, out decimal baseFare) && int.TryParse(guna2TextBox1.Text, out int multiplier))
            {
                Console.WriteLine($"Base Fare: {baseFare}, Multiplier: {multiplier}");

                decimal totalFare = baseFare * multiplier;
                guna2TextBox2.Text = totalFare.ToString("C");

                Console.WriteLine($"Total Fare: {totalFare}");
            }
            else
            {
                Console.WriteLine("Failed to parse base fare or multiplier.");
            }
        }

        private void SaveRecordToDatabase(decimal discountedFare)
        {
            string connectionString = "server=127.0.0.1;user=root;database=bus fare and collection system;port=3306;password=thesis@2023";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO records (fare, discountClicks) VALUES (@fare, @discountClicks)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fare", discountedFare);
                    command.Parameters.AddWithValue("@discountClicks", discountClicks); // Use the class-level variable here
                    command.ExecuteNonQuery();
                }
            }
        }
        private void UploadToDatabase()
        {
            // Get the information needed for database upload
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            string route = $"{guna2ComboBox1.SelectedItem} to {guna2ComboBox2.SelectedItem}";
            string fare = guna2TextBox2.Text;

            // Insert the record into the database
            InsertRecordToDatabase(dateTime, route, fare, route);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Call the method to append information to receipt and upload to database
            AppendToReceipt();
            UploadToDatabase();
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument2;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }

        }
        private List<string> GetReceiptInfo()
        {
            // Example implementation to return receipt info
            return new List<string>
            {
                baseFare.ToString(),
                // Add more details as needed
            };
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayFare();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayFare();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int currentValue))
            {
                int newValue = currentValue - 1;
                if (newValue >= 0)
                {
                    guna2TextBox1.Text = newValue.ToString();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int currentValue))
            {
                int newValue = currentValue + 1;
                if (newValue <= 30)
                {
                    guna2TextBox1.Text = newValue.ToString();
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(guna2TextBox1.Text, out value))
            {
                guna2TextBox1.Text = "0";
            }
            else
            {
                if (value < 0)
                {
                    guna2TextBox1.Text = "0";
                }
                else if (value > 30)
                {
                    guna2TextBox1.Text = "30";
                }
            }

            // Update the fare when the text in guna2TextBox1 changes
            UpdateTotalFare();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            terminals nextForm = new terminals();
            nextForm.Show();
            this.Hide();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            // Update guna2TextBox3 with the multiplied value


        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(guna2TextBox2.Text, System.Globalization.NumberStyles.Currency, null, out decimal originalPrice) &&
        int.TryParse(guna2TextBox1.Text, out int maxClicks))
            {
                if (discountClicks < maxClicks)
                {
                    decimal discountPercentage = GetDiscountPercentage();

                    // Calculate the discount amount based on the discount percentage
                    decimal discountAmount = originalPrice * discountPercentage;

                    // Calculate the discounted price
                    decimal discountedPrice = originalPrice - discountAmount;

                    // Update guna2TextBox2 with the discounted price
                    guna2TextBox2.Text = discountedPrice.ToString("C");

                    // Increment the click counter
                    discountClicks++;
                }
                else
                {
                    // Notify the user that the maximum number of clicks has been reached
                    MessageBox.Show("You've reached the maximum passenger to discount allowed.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private decimal GetDiscountPercentage()
        {
            // Define discount percentages for each click
            decimal[] discountPercentages = { 0.20m, 0.10m, 0.05m };

            // Get the discount percentage based on the current number of clicks
            decimal discountPercentage = discountClicks < discountPercentages.Length ? discountPercentages[discountClicks] : discountPercentages.Last();

            return discountPercentage;
        }
        private void CalculateFare()
        {

        }
        private void DisplayFare()
        {
            string location1 = guna2ComboBox1.SelectedItem?.ToString();
            string location2 = guna2ComboBox2.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(location1) && !string.IsNullOrEmpty(location2))
            {
                if (pamasahe.ContainsKey(location1) && pamasahe[location1].ContainsKey(location2))
                {
                    baseFare = pamasahe[location1][location2];
                    UpdateTotalFare(); // Call the method to update fare
                }
                else
                {
                    guna2TextBox2.Text = "Fare not available";
                }
            }
            else
            {
                guna2TextBox2.Text = string.Empty;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayFare();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayFare();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Clear TextBoxes
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            // Check if the length of the text in guna2TextBox3 is greater than 100
            seatNum.Text = "";


            // Clear ComboBoxes
            guna2ComboBox1.SelectedIndex = -1;
            guna2ComboBox2.SelectedIndex = -1;

            // Clear RichTextBox
            richTextBox1.Text = "";
            

        }



        //receipt void
        private void AppendToReceipt()

        {


            // Get the information from the form controls
            string location1 = guna2ComboBox1.SelectedItem?.ToString();
            string location2 = guna2ComboBox2.SelectedItem?.ToString();
            string passengers = guna2TextBox1.Text;
            string fare = guna2TextBox2.Text;
            string discount = $"{discountClicks}x"; // Update discount information
            string currentDateAndTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string seatNumber = seatNum.Text;


            // Append the content to the RichTextBox
            richTextBox1.Clear();
            richTextBox1.AppendText("LI TOURS CO.,INC. TERMINAL\n");
            richTextBox1.AppendText("***************************\n");
            richTextBox1.AppendText($"Date and Time Purchased: {currentDateAndTime}\n");
            richTextBox1.AppendText("***************************\n");
            richTextBox1.AppendText($"Seat No. : {seatNumber}\n");
            richTextBox1.AppendText("TRAVEL INFORMATION\n");
            richTextBox1.AppendText("Bus Class: Economy\n");
            richTextBox1.AppendText($"Route: {location1} to {location2}\n");
            richTextBox1.AppendText($"No. of Passengers: {passengers}\n");
            richTextBox1.AppendText($"Discounted : {discount}Passenger \n\n");
            richTextBox1.AppendText($"Total Fare Amount: {fare}\n");
            richTextBox1.AppendText($"Date and Time: {currentDateAndTime}\n"); // Add date and time
            richTextBox1.AppendText($"Fare: {fare}\n"); // Add fare

            // Insert the record into the database


        }

        private void InsertRecordToDatabase(string dateTime, string route, string seatNo, string fare)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO records (DateTime, Route, SeatNo, Fare) VALUES (@DateTime, @Route, @SeatNo, @Fare)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DateTime", dateTime);
                        command.Parameters.AddWithValue("@Route", route);
                        command.Parameters.AddWithValue("@SeatNo", seatNo);
                        command.Parameters.AddWithValue("@Fare", fare);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Record inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void records(string dateTime, string route, string seatNo, string fare)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO records (DateTime, Route, SeatNo, Fare) VALUES (@DateTime, @Route, @SeatNo, @Fare)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DateTime", dateTime);
                        command.Parameters.AddWithValue("@Route", route);
                        command.Parameters.AddWithValue("@SeatNo", seatNo);
                        command.Parameters.AddWithValue("@Fare", fare);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Record inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void guna2Button5_Click(object sender, EventArgs e)
        {
            AppendToReceipt();
        }



        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged_1(object sender, EventArgs e)
        {
            UpdateRichTextBox();
        }

        private void UpdateRichTextBox()
        {
        }

        private void guna2TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a digit or control key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If not a digit, set handled to true to cancel the event
                e.Handled = true;
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            Seat nextForm = new Seat();
            nextForm.Show();

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            Font printFont = richTextBox1.Font;
            SolidBrush myBrush = new SolidBrush(Color.Black);

            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            StringReader stringReader = new StringReader(richTextBox1.Text);

            while (count < linesPerPage && ((line = stringReader.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count++;
            }

            e.HasMorePages = (line != null);
            myBrush.Dispose();
        }


        private void guna2TextBox3_TextChanged_2(object sender, EventArgs e)
        {
        }

        private void seatNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void seatNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                // If not a digit, space, or control key, set handled to true to cancel the event
                e.Handled = true;
            }
        }
    }
}

