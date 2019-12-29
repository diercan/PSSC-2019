using GestiuneElevi.Models;
using GestiuneElevi.Reositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class VeziTotiEleviiForm : Form
    {
        private IEleviRepository eleviRepository;
        private List<ElevEntity> elevi = new List<ElevEntity>();

        public VeziTotiEleviiForm(IEleviRepository eleviRepository)
        {
            InitializeComponent();

            this.eleviRepository = eleviRepository;

            Task.Run(async () => { elevi = await eleviRepository.GetAllEleviAsyncTask(); }).Wait();
            listBox1.Items.Add("Nume\tPrenume\tVarsta\tClasa");
            listBox1.Items.Add("");
            foreach(ElevEntity entity in elevi)
            {
                listBox1.Items.Add(entity.getInfo());
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                ElevEntity elev = elevi[listBox1.SelectedIndex - 2];
                AdaugaNotaForm form = new AdaugaNotaForm(eleviRepository, elev);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Alege un elev");
            }
        }
    }
}
