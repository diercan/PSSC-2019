using BankAccountApp.Entities;
using BankAccountApp.Models;
using BankAccountApp.Repositories;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MasterRepository.InstantiateRepository();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            string IBAN = textBox1.Text;
            if(!string.IsNullOrWhiteSpace(IBAN))
            {
                AccountEntity accountEntity = null;
                await Task.Run(async () => { accountEntity = await MasterRepository.AccountRepository.GetAccountTask(IBAN); });

                if(accountEntity != null)
                {
                    AccountModel account = new AccountModel();
                    account.setIBAN(accountEntity.PartitionKey);
                    account.setName(accountEntity.Name);
                    account.setSurname(accountEntity.RowKey);
                    account.setBalance(accountEntity.Balance);
                    account.setExpiryDate(accountEntity.ExpiryDate);
                    GlobalInformation.setAccount(account);

                    HomeForm form = new HomeForm();
                    form.Show();
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Nu exista acest IBAN");
                }
            }
            else
            {
                MessageBox.Show("Completeaza IBAN");
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            SignUpForm form = new SignUpForm();
            form.Show();
        }
    }
}
