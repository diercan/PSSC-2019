using BankAccountApp.Entities;
using BankAccountApp.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountApp
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nume = textBox1.Text;
            string prenume = textBox2.Text;
            string cnp = textBox3.Text;

            if(!string.IsNullOrWhiteSpace(nume) && !string.IsNullOrWhiteSpace(prenume) &&
                !string.IsNullOrWhiteSpace(cnp))
            {
                string IBAN = generateIBAN();
                AccountEntity accountEntity = new AccountEntity(IBAN, nume);
                accountEntity.Name = prenume;
                accountEntity.Balance = 0;
                accountEntity.ExpiryDate = "07/25";

                Task.Run(() => { MasterRepository.AccountRepository.AddAccountTask(accountEntity); });

                Close();
            }
            else
            {
                MessageBox.Show("Completati toate datele!");
            }
        }

        private string generateIBAN()
        {
            Random rnd = new Random();
            string IBAN = "RO" + rnd.Next(1, 10).ToString() + rnd.Next(1, 10).ToString();

            int banca = rnd.Next(1, 4);
            if(banca == 1)
            {
                IBAN += "INGB00009999";
            }
            if(banca == 2)
            {
                IBAN += "BTRL00009999";
            }
            if(banca == 3)
            {
                IBAN += "RFSB00009999";
            }

            for(int i = 1; i <= 8; i++)
            {
                IBAN += rnd.Next(1, 10).ToString();
            }

            return IBAN;
        }
    }
}
