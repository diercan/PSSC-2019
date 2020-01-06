using System.Windows.Forms;

namespace BankAccountApp
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();

            label1.Text = GlobalInformation.Account.Surname + " " + GlobalInformation.Account.Name;
            label2.Text = "Suma de bani in cont: " + GlobalInformation.Account.Balance + " RON";
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            AddRetrieveMoneyForm form = new AddRetrieveMoneyForm(0);
            var result = form.ShowDialog();
            if(result == DialogResult.OK)
            {
                label2.Text = "Suma de bani in cont: " + GlobalInformation.Account.Balance + " RON";
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            AddRetrieveMoneyForm form = new AddRetrieveMoneyForm(1);
            var result = form.ShowDialog();
            if(result == DialogResult.OK)
            {
                label2.Text = "Suma de bani in cont: " + GlobalInformation.Account.Balance + " RON";
            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            TransferMoneyForm form = new TransferMoneyForm();
            var result = form.ShowDialog();
            if(result == DialogResult.OK)
            {
                label2.Text = "Suma de bani in cont: " + GlobalInformation.Account.Balance + " RON";
            }
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
