using GestiuneElevi.Reositories;
using System;
using System.Windows.Forms;

namespace GestiuneElevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MasterRepository.InstantiateEleviRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdaugaElevForm form = new AdaugaElevForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditareForm form = new EditareForm();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VeziTotiEleviiForm form = new VeziTotiEleviiForm();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VeziNoteEleviForm form = new VeziNoteEleviForm();
            form.Show();
        }
    }
}
