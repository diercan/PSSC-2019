using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaminConfig
{
    public partial class Form1 : Form
    {
        public int Etaje;
        public int Camere;
        public int Locuri;
        public string Parter;
        public string Etaj1;
        public string Etaj2;
        public string Etaj3;
        public string Etaj4;
        public string Etaj5;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Fete");
            comboBox1.Items.Add("Baieti");
            comboBox1.Enabled = false;
            comboBox2.Items.Add("Fete");
            comboBox2.Items.Add("Baieti");
            comboBox2.Enabled = false;
            comboBox3.Items.Add("Fete");
            comboBox3.Items.Add("Baieti");
            comboBox3.Enabled = false;
            comboBox4.Items.Add("Fete");
            comboBox4.Items.Add("Baieti");
            comboBox4.Enabled = false;
            comboBox5.Items.Add("Fete");
            comboBox5.Items.Add("Baieti");
            comboBox5.Enabled = false;
            comboBox6.Items.Add("Fete");
            comboBox6.Items.Add("Baieti");
            comboBox6.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Etaje = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            Camere = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
            Locuri = Convert.ToInt32(Math.Round(numericUpDown3.Value, 0));
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            comboBox1.Enabled = true;
            if(Etaje>=2)
            {
                comboBox2.Enabled = true;
                if(Etaje>=3)
                {
                    comboBox3.Enabled = true;
                    if(Etaje>=4)
                    {
                        comboBox4.Enabled = true;
                        if(Etaje>=5)
                        {
                            comboBox5.Enabled = true;
                            if(Etaje==6)
                            {
                                comboBox6.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ok = 0;

            if (comboBox1.SelectedIndex == -1)
            {
                ok++;
            }
            else
            {
                Parter = comboBox1.SelectedItem.ToString();
            }

            if (comboBox2.Enabled == true)
            {
                if (comboBox2.SelectedIndex == -1)
                {
                    ok++;
                }
                else
                {
                    Etaj1 = comboBox2.SelectedItem.ToString();
                }

            }

            if (comboBox3.Enabled == true)
            {
                if (comboBox3.SelectedIndex == -1)
                {
                    ok++;
                }
                else
                {
                    Etaj2 = comboBox3.SelectedItem.ToString();
                }
            }

            if (comboBox4.Enabled == true)
            {
                if (comboBox4.SelectedIndex == -1)
                {
                    ok++;
                }
                else
                {
                    Etaj3 = comboBox4.SelectedItem.ToString();
                }
            }

            if (comboBox5.Enabled == true)
            {
                if (comboBox5.SelectedIndex == -1)
                {
                    ok++;
                }
                else
                {
                    Etaj4 = comboBox5.SelectedItem.ToString();
                }
            }

            if (comboBox6.Enabled == true)
            {
                if (comboBox6.SelectedIndex == -1)
                {
                    ok++;
                }
                else
                {
                    Etaj5 = comboBox6.SelectedItem.ToString();
                }
            }

            if (ok == 0)
            {
                string fileName = "CaminConfig.txt";

                string workingDirectory = Environment.CurrentDirectory;

                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

                string projectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                string[] partialpath = projectDirectory.Split(new string[] { projectName }, StringSplitOptions.None);

                string filePath = partialpath[0] + fileName;

                using (FileStream fs = File.Create(filePath))
                { }

                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filePath))
                {
                    string Config = Etaje.ToString() + '/' + Camere.ToString() + '/' + Locuri.ToString() + '/' + Parter[0];

                    if(Etaj1 == null)
                    {
                        Config = Config + '/' + '0';
                    }
                    else
                    {
                        Config = Config + '/' + Etaj1[0];
                    }

                    if (Etaj2 == null)
                    {
                        Config = Config + '/' + '0';
                    }
                    else
                    {
                        Config = Config + '/' + Etaj2[0];
                    }

                    if (Etaj3 == null)
                    {
                        Config = Config + '/' + '0';
                    }
                    else
                    {
                        Config = Config + '/' + Etaj3[0];
                    }

                    if (Etaj4 == null)
                    {
                        Config = Config + '/' + '0';
                    }
                    else
                    {
                        Config = Config + '/' + Etaj4[0];
                    }

                    if (Etaj5 == null)
                    {
                        Config = Config + '/' + '0';
                    }
                    else
                    {
                        Config = Config + '/' + Etaj5[0];
                    }

                    file.WriteLine(Config);
                }

                MessageBox.Show("Fisierul a fost creat cu succes !");
                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Nu lasati casute necompletate !");
            }
        }
    }
}
