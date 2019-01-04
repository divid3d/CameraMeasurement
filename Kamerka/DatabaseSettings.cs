using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kamerka
{
    public partial class DatabaseSettings : Form
    {
        string log;



        public DatabaseSettings()
        {
            InitializeComponent();
            textBox_password.PasswordChar = '*';
            textBox_username.Enabled = false;
            label2.Enabled = false;
            textBox_host.Enabled = false;
            label1.Enabled = false;
            textBox_password.Enabled = false;
            label3.Enabled = false;
            textBox_database.Enabled = false;
            label4.Enabled = false;
            button_connect.Enabled = false;

            
        }

        private void DatabaseSettings_Load(object sender, EventArgs e)
        {
            
        }

       

        public string getHost()
        {
            return textBox_host.Text;
        }

        public string getLog()
        {
                return this.log;
        }

        public string getUsername()
        {
            return textBox_username.Text;
        }

        public string getPassword()
        {
            return textBox_password.Text;
        }

        public string getDatabase()
        {
            return textBox_database.Text;
        }


        private void button_connect_Click(object sender, EventArgs e)
        {
            string host = textBox_host.Text;
            string username = textBox_username.Text;
            string password = textBox_password.Text;
            string database = textBox_database.Text;

            if((host.Trim().Length != 0 && username.Trim().Length != 0 && database.Trim().Length != 0)){

                String connectionString = "Host=" + host + ";Username=" + username + ";Password=" + password + ";Database=" + database;
                Npgsql.NpgsqlConnection npgsqlConnection = new Npgsql.NpgsqlConnection(connectionString);

                try
                {
                    npgsqlConnection.Open();
                    this.log = "Successfully connected to database (database: " + database + " Host: " + host + " Username: " + username+")";
                    this.DialogResult = DialogResult.OK;
                   

                }
                catch(Exception ex)
                {
                    this.log = ex.Message;
                    this.DialogResult = DialogResult.Abort;
                }
                finally
                {
                    npgsqlConnection.Close();
                }
               
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void checkBox_databaseEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_databaseEnable.Checked)
            {
                textBox_username.Enabled = true;
                label2.Enabled = true;
                textBox_host.Enabled = true;
                label1.Enabled = true;
                textBox_password.Enabled = true;
                label3.Enabled = true;
                textBox_database.Enabled = true;
                label4.Enabled = true;
                button_connect.Enabled = true;

                textBox_host.Focus();
            }
            else
            {
                textBox_host.Clear();
                textBox_username.Clear();
                textBox_password.Clear();
                textBox_database.Clear();


                textBox_username.Enabled = false;
                label2.Enabled = false;
                textBox_host.Enabled = false;
                label1.Enabled = false;
                textBox_password.Enabled = false;
                label3.Enabled = false;
                textBox_database.Enabled = false;
                label4.Enabled = false;
                button_connect.Enabled = false;

                button_cancel.Focus();
            }
        }
    }
}
