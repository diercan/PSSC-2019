using GestiuneElevi.Entities;
using GestiuneElevi.Reositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class EditareForm : Form
    {
        private ElevEntity elevGasit = null;

        public EditareForm()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string cnp = textBox1.Text;

            await Task.Run(async () => { elevGasit = await MasterRepository.EleviRepository.GetElevAsyncTask(cnp); });

            if(elevGasit != null)
            {
                textBox2.Text = elevGasit.Nume;
                textBox3.Text = elevGasit.Prenume;
                textBox4.Text = elevGasit.RowKey;
                textBox5.Text = elevGasit.Varsta.ToString();
                textBox6.Text = elevGasit.Clasa.ToString();
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            string nume = textBox2.Text;
            string prenume = textBox3.Text;
            string cnp = textBox4.Text;
            int varsta, clasa;
            try { varsta = int.Parse(textBox4.Text); } catch(Exception) { varsta = -1; }
            try { clasa = int.Parse(textBox5.Text); } catch(Exception) { clasa = -1; }

            if(!string.IsNullOrWhiteSpace(nume) && !string.IsNullOrWhiteSpace(prenume) && !string.IsNullOrWhiteSpace(cnp) && varsta != -1 && clasa != -1)
            {
                ElevEntity elevEntity = new ElevEntity(elevGasit.PartitionKey, cnp);
                elevEntity.Nume = nume;
                elevEntity.Prenume = prenume;
                elevEntity.Varsta = varsta;
                elevEntity.Clasa = clasa;

                Task.Run(() => { MasterRepository.EleviRepository.AdaugaElevAsyncTask(elevEntity); });

                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox6.Text = string.Empty;

                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Completati toate datele!");
            }
        }
    }
}
