using GestiuneElevi.Entities;
using GestiuneElevi.Models;
using GestiuneElevi.Reositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class VeziNoteEleviForm : Form
    {
        private List<NotaEntity> note = new List<NotaEntity>();

        public VeziNoteEleviForm()
        {
            InitializeComponent();

            Task.Run(async () => { note = await MasterRepository.EleviRepository.GetAllNoteAsyncTask(); }).Wait();

            listBox1.Items.Add("Nume\tPrenume\tClasa\tMaterie\tNota");
            listBox1.Items.Add("");
            for(int i = 0; i < note.Count; i++)
            {
                ElevModel elev = null;
                Task.Run(async () => {
                    ElevEntity entity = await MasterRepository.EleviRepository.GetElevAsyncTask(note[i].RowKey);
                    elev = new ElevModel();
                    elev.setId(entity.PartitionKey);
                    elev.setCnp(entity.RowKey);
                    elev.setNume(entity.Nume);
                    elev.setPrenume(entity.Prenume);
                    elev.setVarsta(entity.Varsta);
                    elev.setClasa(entity.Clasa);
                }).Wait();
                listBox1.Items.Add(elev.getInfo(note[i].Materie, note[i].Nota));
            }
        }
    }
}
