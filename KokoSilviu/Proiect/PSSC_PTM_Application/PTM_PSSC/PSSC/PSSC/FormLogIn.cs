using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using PSSC.Entities;

namespace PSSC
{
    public partial class FormLogIn : Form
    {
        public string uid;
        public bool dev_user;
        public FormLogIn()
        {
            InitializeComponent();
            panel2.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            textBoxUser.Enabled = true;
            labelUser.Visible = false;
            labelUser.Enabled = false;
            this.ActiveControl = textBoxUser;
        }

        private void labelPassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.Enabled = true;
            labelPassword.Visible = false;
            labelPassword.Enabled = false;
            this.ActiveControl = textBoxPassword;
        }

        private void textBoxUser_Leave(object sender, EventArgs e)
        {
            if (textBoxUser.Text == "")
            {
                textBoxUser.Enabled = false;
                labelUser.Visible = true;
                labelUser.Enabled = true;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Enabled = false;
                labelPassword.Visible = true;
                labelPassword.Enabled = true;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (textBoxUser.Text == "")
            {
                textBoxUser.Enabled = false;
                labelUser.Visible = true;
                labelUser.Enabled = true;
            }
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Enabled = false;
                labelPassword.Visible = true;
                labelPassword.Enabled = true;
            }
        }

        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser.Text == "")
            {
                textBoxUser.Enabled = false;
                labelUser.Visible = true;
                labelUser.Enabled = true;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Enabled = false;
                labelPassword.Visible = true;
                labelPassword.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings Feature under development.");
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PTM-Project Time Manager: Developed by Koko Silviu-Alexandru(silviu.koko@yahoo.com)");
        }

        private void buttonPower_MouseEnter(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void panelMenu_MouseLeave(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void kryptonButtonLogIn_Click(object sender, EventArgs e)
        {
            bool login = false;
            bool Dev_user = false;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCLogInKoko");
            table.CreateIfNotExists();

            List<UserLogInEntity> users = new List<UserLogInEntity>();
            TableQuery<UserLogInEntity> query = new TableQuery<UserLogInEntity>();
            users = table.ExecuteQuery(new TableQuery<UserLogInEntity>()).ToList();

            foreach (UserLogInEntity entity in users)
            {
                if (entity.PartitionKey.Equals(textBoxUser.Text) && entity.RowKey.Equals(textBoxPassword.Text))
                {
                    login = true;
                    if (entity.role.Equals("developer"))
                    {
                        Dev_user = true;
                    }
                    break;
                }
            }

            if (login)
            {
                //  MainForm f = new MainForm(textBoxUser.Text, Dev_user);
                //f.Show();
                uid = textBoxUser.Text;
                dev_user = Dev_user;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorect User or Password.");
            }
        }
    }
}
