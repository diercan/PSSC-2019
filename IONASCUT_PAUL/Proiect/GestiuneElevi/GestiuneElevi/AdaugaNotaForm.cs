using GestiuneElevi.Models;
using GestiuneElevi.Reositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class AdaugaNotaForm : Form
    {
        private IEleviRepository eleviRepository;
        private ElevEntity elev;

        public AdaugaNotaForm(IEleviRepository eleviRepository, ElevEntity elev)
        {
            InitializeComponent();
            this.eleviRepository = eleviRepository;
            this.elev = elev;

            label1.Text = elev.Nume + elev.Prenume;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string materie = textBox2.Text;
            int nota;
            try { nota = int.Parse(textBox1.Text); } catch { nota = -1; }

            if(nota >= 1 && nota <= 10 && !string.IsNullOrEmpty(materie))
            {
                NotaEntity notaEntity = new NotaEntity(Guid.NewGuid().ToString(), elev.RowKey);
                notaEntity.Materie = materie;
                notaEntity.Nota = nota;
                Task.Run(() => { eleviRepository.AdaugaNotaAsyncTask(notaEntity); });
                Close();
            }
            else
            {
                MessageBox.Show("Nota trebuie sa fie intre 1 si 10");
            }
        }
    }
}
