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
    public partial class Users : Form
    {
        string connection = "Server = localhost;Port = 5432;User id = postgres; Password = dotnet;Database = family";

        public Users()
        {
            InitializeComponent();
            GetTable();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            login form = new login();
            form.Show();
            this.Hide();
        }
        private void GetTable()   //returns username and id number
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id, name FROM login", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_users.DataSource = dt;
        }

        private void label_Exid_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
