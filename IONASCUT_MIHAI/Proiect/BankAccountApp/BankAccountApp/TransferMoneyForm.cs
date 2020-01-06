using BankAccountApp.Entities;
using BankAccountApp.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountApp
{
    public partial class TransferMoneyForm : Form
    {
        public TransferMoneyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string IBANdestinatar = textBox1.Text;

            if(!string.IsNullOrWhiteSpace(IBANdestinatar))
            {
                AccountEntity destinatarAccountEntity = null;
                Task.Run(async () => { destinatarAccountEntity = await MasterRepository.AccountRepository.GetAccountTask(IBANdestinatar); }).Wait();

                if(destinatarAccountEntity != null)
                {
                    double sum;
                    try
                    {
                        sum = double.Parse(textBox2.Text);
                    }
                    catch(Exception)
                    {
                        sum = -1;
                    }

                    if(sum > 0)
                    {
                        if(sum <= GlobalInformation.Account.Balance)
                        {
                            AccountEntity sourceAccountEntity = new AccountEntity(GlobalInformation.Account.IBAN, GlobalInformation.Account.Surname);
                            sourceAccountEntity.Name = GlobalInformation.Account.Name;
                            sourceAccountEntity.Balance = GlobalInformation.Account.Balance;
                            sourceAccountEntity.ExpiryDate = GlobalInformation.Account.ExpiryDate;

                            Task.Run(async () => { await MasterRepository.AccountRepository.TransferMoneyTask(sourceAccountEntity, destinatarAccountEntity, sum); }).Wait();
                            GlobalInformation.Account.retrieveMoney(sum);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Suma este prea mare fata de cati bani ai in cont.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Suma trebuie sa fie un numare real si mai mare decat 0");
                    }
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
    }
}
