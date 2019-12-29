using GestiuneElevi.Entities;
using GestiuneElevi.Models;
using GestiuneElevi.Reositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class VeziTotiEleviiForm : Form
    {
        private List<ElevModel> elevi = new List<ElevModel>();

        public VeziTotiEleviiForm()
        {
            InitializeComponent();

            Task.Run(async () => {
                List<ElevEntity> entities = await MasterRepository.EleviRepository.GetAllEleviAsyncTask();
                foreach(ElevEntity entity in entities)
                {
                    ElevModel elev = new ElevModel();
                    elev.setId(entity.PartitionKey);
                    elev.setCnp(entity.RowKey);
                    elev.setNume(entity.Nume);
                    elev.setPrenume(entity.Prenume);
                    elev.setVarsta(entity.Varsta);
                    elev.setClasa(entity.Clasa);
                    elevi.Add(elev);
                }
            }).Wait();

            listBox1.Items.Add("Nume\tPrenume\tVarsta\tClasa");
            listBox1.Items.Add("");
            foreach(ElevModel elev in elevi)
            {
                listBox1.Items.Add(elev.getInfo());
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                ElevModel elev = elevi[listBox1.SelectedIndex - 2];
                AdaugaNotaForm form = new AdaugaNotaForm(elev);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Alege un elev");
            }
        }
    }
}
