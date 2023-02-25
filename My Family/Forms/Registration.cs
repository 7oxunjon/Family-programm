using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Family.Forms
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        string connection = "Server = localhost;Port = 5432;User id = postgres; Password = dotnet;Database = family";

        private void label_Exid_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            login form = new login();
            form.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (TextBox_Id.Text == "" || textBox_name.Text == "" || textBox_password.Text == "")
            {
                MessageBox.Show("ERROR", "Not complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            else
            {
                NpgsqlConnection con = new NpgsqlConnection(connection);
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO login VALUES (@id,@Name,@Password)", con);
                cmd.Parameters.AddWithValue("id", int.Parse(TextBox_Id.Text));
                cmd.Parameters.AddWithValue("Name", textBox_name.Text);
                cmd.Parameters.AddWithValue("Password", textBox_password.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Added", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            TextBox_Id.Clear();
            textBox_name.Clear();
            textBox_password.Clear();
        }
    }
}
