using GestiuneElevi.Models;
using GestiuneElevi.Reositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class VeziNoteEleviForm : Form
    {
        private IEleviRepository eleviRepository;
        private List<ElevEntity> elevi = new List<ElevEntity>();
        private List<NotaEntity> note = new List<NotaEntity>();

        public VeziNoteEleviForm(IEleviRepository eleviRepository)
        {
            InitializeComponent();
            this.eleviRepository = eleviRepository;

            Task.Run(async () => { elevi = await eleviRepository.GetAllEleviAsyncTask(); }).Wait();
            Task.Run(async () => { note = await eleviRepository.GetAllNoteAsyncTask(); }).Wait();

            listBox1.Items.Add("Nume\tPrenume\tClasa\tMaterie\tNota");
            listBox1.Items.Add("");
            for(int i = 0; i < note.Count; i++)
            {
                ElevEntity elev = null;
                Task.Run(async () => { elev = await eleviRepository.GetElevAsyncTask(note[i].RowKey); }).Wait();
                listBox1.Items.Add(elev.getInfo(note[i].Materie, note[i].Nota));
            }
        }
    }
}
