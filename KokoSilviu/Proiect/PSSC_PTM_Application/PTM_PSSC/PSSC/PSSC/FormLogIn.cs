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
            MessageBox.Show("Info Feature under development.");
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
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitRepo\\PSSC-2019\\KokoSilviu\\Proiect\\PSSC_PTM_Application\\PTM_PSSC\\PSSC\\PSSC\\Psscdb.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
            string cmd = "SELECT * FROM UserLogIn";
            SqlDataAdapter da = new SqlDataAdapter(cmd,conn);
            DataSet dbdst = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);

            

            foreach (DataRow dr in dt.Rows)
            {
               if(dr["uid"].Equals(textBoxUser.Text) && dr["password"].Equals(textBoxPassword.Text))
               {
                 login = true;
                 if(dr["type"].Equals("developer"))
                 {
                        Dev_user = true;
                 }
                    break;
               }
            }
            conn.Close();
            //check user and password in database and call main form with parameter for developer or project manager.
            if(login)
            //if(user and pass match db)
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
