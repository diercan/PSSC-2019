using GestiuneElevi.Reositories;
using System;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class Form1 : Form
    {
        private IEleviRepository eleviRepository;

        public Form1()
        {
            InitializeComponent();

            eleviRepository = new EleviRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdaugaElevForm form = new AdaugaElevForm(eleviRepository);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditareForm form = new EditareForm(eleviRepository);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VeziTotiEleviiForm form = new VeziTotiEleviiForm(eleviRepository);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VeziNoteEleviForm form = new VeziNoteEleviForm(eleviRepository);
            form.Show();
        }
    }
}
