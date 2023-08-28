using System;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace Registrationform
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = @"Data Source=LAPTOP-UDFFUK18\SQLEXPRESS;Initial Catalog=RegistrationWeb;Integrated Security=True";
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        string phonepattern = @"^(?:(?:\+|00)\d{1,3})?[ -]*(\d{10})$";
        string passpattern = @"^.(?=.{8,})(?=.\d)(?=.[a-z])(?=.[A-Z])(?=.[!@#$%^&+=]).*$";
        string gender = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(DateTime.Today < dateTimePicker1.Value)
            {
                MessageBox.Show("Please select a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {

        }

        private void textBox7_Leave(object sender, EventArgs e)
        {

        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Kerala")
            {
                comboBox2.Items.Add("Kochi");
                comboBox2.Items.Add("Thiruvananthapuram");
                comboBox2.Items.Add("Kozhikode");
            }
            else if (comboBox1.Text == "Tamil Nadu")
            {
                comboBox2.Items.Add("Chennai");
                comboBox2.Items.Add("Coimbatore");
                comboBox2.Items.Add("Pondicherry");
            }
            else if(comboBox1.Text == "Karnataka")
            {
                comboBox2.Items.Add("Bangalore");
                comboBox2.Items.Add("Mysore");
                comboBox2.Items.Add("Mangalore");
            }
            else
            {
                return;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please fill in the first name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please fill in the last name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DateTime.Today < dateTimePicker1.Value)
            {
                MessageBox.Show("Please select a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (radioButton1.Checked)
            {
                gender = "Male";
            }
            else if (radioButton2.Checked)
            {
                gender = "Female";
            }
            else if (radioButton3.Checked)
            {
                gender = "Transgender";
            }
            else
            {
                MessageBox.Show("Please select a gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please fill in the phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Regex.IsMatch(textBox4.Text, phonepattern))
            {
                MessageBox.Show("Please fill valid phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Valid Phone Number");
               
            }
            if (!Regex.IsMatch(textBox5.Text, pattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please fill in the address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select a state.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select a city.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please fill in the username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Regex.IsMatch(textBox8.Text,passpattern))
            {
                MessageBox.Show("A strong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                
            }
            if (textBox8.Text != textBox9.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO registrationdata (firstname, lastname, dateofbirth, Gender, phone, email, Address, State, city, username, password, cpassword) VALUES (@firstname, @lastname, @dateofBirth, @gender, @Phone, @email, @address, @state, @city, @username, @password, @cpassword)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    cmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@dateofBirth", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@Phone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@email", textBox5.Text);
                    cmd.Parameters.AddWithValue("@address", textBox6.Text);
                    cmd.Parameters.AddWithValue("@state", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@city", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@username", textBox7.Text);
                    cmd.Parameters.AddWithValue("@password", textBox8.Text);
                    cmd.Parameters.AddWithValue("@cpassword", textBox9.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sign up successful");
                        registermenu form2 = new registermenu();
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Sign up details");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}