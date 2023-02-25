using Microsoft.VisualBasic.ApplicationServices;
using My_Family.Context;
using My_Family.Forms;
using My_Family.Model;
using Npgsql;

namespace My_Family
{
    public partial class login : Form
    {
        public static string username;     //to return a username

        public login()
        {
            InitializeComponent();
        }
        string connection = "Server = localhost;Port = 5432;User id = postgres; Password = dotnet;Database = family";

        private void label_Exid_Click(object sender, EventArgs e)
        {
            Application.Exit();                                //exit button
        }

        private void Button_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1_username.Text == "" || textBox2_password.Text == "")
                {
                    MessageBox.Show("ERROR", "Not complete", MessageBoxButtons.OK, MessageBoxIcon.Error);  //error when empty
                }
                else
                {
                    NpgsqlConnection con = new NpgsqlConnection(connection);         //If the information is correct, the backboard will open
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM login WHERE name = '" + textBox1_username.Text + "' AND password = '" + textBox2_password.Text + "'", con);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        username = textBox1_username.Text;
                        Acount background = new Acount();
                        background.Show();
                        this.Hide();
                    }
                    else
                    {
                        //Returns an error if a data error is entered
                        MessageBox.Show("Wrong Username or password", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            

        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox1_username.Clear();
            textBox2_password.Clear();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Users users = new Users();    //opens user list form
            users.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();        //opens the registration window
            registration.Show();
            this.Hide();
        }
    }
}