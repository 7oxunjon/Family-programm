using My_Family.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Family.Forms
{
    public partial class Acount : Form
    {
        string connection = "Server = localhost;Port = 5432;User id = postgres; Password = dotnet;Database = family";

        public Acount()
        {
            InitializeComponent();         
        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                //error when empty
                if (TextBox_id.Text == "" || comboBox_cos_inc.Text == "" || comboBox_cotegory.Text == "" || TextBox_sum.Text == "" || richTextBox_comment.Text == "")
                {
                    MessageBox.Show("information is not complete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NpgsqlConnection con = new NpgsqlConnection(connection);
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE cost SET costincome = '" + comboBox_cos_inc.Text + "'" +
                          ", category = '" + comboBox_cotegory.Text + "',sum = " + TextBox_sum.Text + "" +
                          ",coment = '" + richTextBox_comment.Text + "' WHERE id = '" + TextBox_id.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GetAllIncome(); //returns all income
                    GetAllCost();  //returns all costs
                    SumIncome();  //returns the amount of income
                    SumCost();    //returns the amount of cost
                    Balance();    //total remaining money
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void label_Exid_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Background_Load(object sender, EventArgs e)
        {
            label_date.Text = DateTime.Today.ToShortDateString();
            label_name.Text = login.username;
            GetAllCost();
            GetAllIncome();
            SumIncome();
            SumCost();
            Balance();
        }

        private void Button_cleare_Click(object sender, EventArgs e)
        {
            TextBox_id.Clear();
            comboBox_cos_inc.SelectedItem = "";
            comboBox_cotegory.SelectedItem = "";
            TextBox_sum.Clear();
            richTextBox_comment.Clear();
            comboBox_Income.SelectedItem = "";
            comboBox_cost.SelectedItem = "";
        }
        private void GetAllIncome()
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT *FROM cost WHERE name = '" + label_name.Text + "' AND costincome = 'INCOME'", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_Income.DataSource = dt;

        }
        private void GetAllCost()
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT *FROM cost WHERE name = '" + label_name.Text + "' AND costincome = 'COST'", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_Cost.DataSource = dt;
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "" || comboBox_cos_inc.Text == "" || comboBox_cotegory.Text == "" || TextBox_sum.Text == "" || richTextBox_comment.Text == "")
                {
                    MessageBox.Show("information is not complete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NpgsqlConnection con = new NpgsqlConnection(connection);
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO cost VALUES (@id,@name,@costincome,@category,@sum,@data,@coment)", con);
                    cmd.Parameters.AddWithValue("id", int.Parse(TextBox_id.Text));
                    cmd.Parameters.AddWithValue("name", label_name.Text);
                    cmd.Parameters.AddWithValue("costincome", comboBox_cos_inc.Text);
                    cmd.Parameters.AddWithValue("category", comboBox_cotegory.Text);
                    cmd.Parameters.AddWithValue("sum", double.Parse(TextBox_sum.Text));
                    cmd.Parameters.AddWithValue("data", label_date.Text);
                    cmd.Parameters.AddWithValue("coment", richTextBox_comment.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GetAllIncome();
                    GetAllCost();
                    SumIncome();
                    SumCost();
                    Balance();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView_Income_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox_id.Text = DataGridView_Income.SelectedRows[0].Cells[0].Value.ToString();
            comboBox_cos_inc.Text = DataGridView_Income.SelectedRows[0].Cells[2].Value.ToString();
            comboBox_cotegory.Text = DataGridView_Income.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_sum.Text = DataGridView_Income.SelectedRows[0].Cells[4].Value.ToString();
            richTextBox_comment.Text = DataGridView_Income.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void DataGridView_Cost_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox_id.Text = DataGridView_Cost.SelectedRows[0].Cells[0].Value.ToString();
            comboBox_cos_inc.Text = DataGridView_Cost.SelectedRows[0].Cells[2].Value.ToString();
            comboBox_cotegory.Text = DataGridView_Cost.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_sum.Text = DataGridView_Cost.SelectedRows[0].Cells[4].Value.ToString();
            richTextBox_comment.Text = DataGridView_Cost.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void Button_SearchIncome_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM cost WHERE category = '" + comboBox_Income.Text + "' AND name = '" + label_name.Text + "'", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_Income.DataSource = dt;
        }

        private void Button_Search_Cost_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM cost WHERE category = '" + comboBox_cost.Text + "' AND name = '" + label_name.Text + "' ", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_Cost.DataSource = dt;
        }
        private void SumIncome()
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT SUM(sum) FROM cost where costincome = 'INCOME' AND name = '" + label_name.Text + "'", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_incomeValues.DataSource = dt;
        }
        private void SumCost()
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT SUM(sum) FROM cost where costincome = 'COST' AND name = '" + label_name.Text + "'", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_Cost_values.DataSource = dt;
        }
        private void Balance()
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT SUM(sum) - (SELECT SUM(sum) FROM cost where costincome = 'COST' AND name = '" + label_name.Text + "') " +
                "FROM cost where costincome = 'INCOME' AND name = '" + label_name.Text + "'", con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView_balans.DataSource = dt;
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "" || comboBox_cos_inc.Text == "" || comboBox_cotegory.Text == "" || TextBox_sum.Text == "" || richTextBox_comment.Text == "")
                {
                    MessageBox.Show("information is not complete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NpgsqlConnection con = new NpgsqlConnection(connection);
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE  FROM cost WHERE  id = '" + TextBox_id.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GetAllIncome();
                    GetAllCost();
                    SumIncome();
                    SumCost();
                    Balance();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Button_logout_Click(object sender, EventArgs e)
        {
            login form = new login();
            form.Show();
            this.Hide();
        }

    }
}
