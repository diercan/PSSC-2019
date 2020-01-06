using BankAccountApp.Entities;
using BankAccountApp.Models;
using BankAccountApp.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountApp
{
    public partial class AddRetrieveMoneyForm : Form
    {
        private int type;

        public AddRetrieveMoneyForm(int type)
        {
            InitializeComponent();
            
            this.type = type;
            if(type == 0)
            {
                label1.Text = "Suma pe care vrei sa o adaugi";
                button1.Text = "Adauga bani";
            }
            else
            {
                label1.Text = "Suma pe care vrei sa o retragi";
                button1.Text = "Retrage bani";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sum;
            try
            {
                sum = double.Parse(textBox1.Text);
            }
            catch(Exception)
            {
                sum = -1;
            }
            
            if(sum > 0)
            {
                if(type == 0)
                {
                    AddMoney(sum);
                }
                else
                {
                    RetrieveMoney(sum);
                }
            }
            else
            {
                MessageBox.Show("Suma trebuie sa fie un numare real si mai mare decat 0");
            }
        }

        private void AddMoney(double sum)
        {
            AccountEntity accountEntity = castToEntity();
            Task.Run(async () => { await MasterRepository.AccountRepository.AddMoneyTask(accountEntity, sum); }).Wait();
            GlobalInformation.Account.addMoney(sum);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RetrieveMoney(double sum)
        {
            if(sum <= GlobalInformation.Account.Balance)
            {
                AccountEntity accountEntity = castToEntity();
                Task.Run(async () => { await MasterRepository.AccountRepository.RetrieveMoneyTask(accountEntity, sum); }).Wait();
                GlobalInformation.Account.retrieveMoney(sum);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Suma este prea mare fata de cati bani ai in cont.");
            }
        }

        private AccountEntity castToEntity()
        {
            AccountEntity entity = new AccountEntity(GlobalInformation.Account.IBAN, GlobalInformation.Account.Surname);
            entity.Name = GlobalInformation.Account.Name;
            entity.Balance = GlobalInformation.Account.Balance;
            entity.ExpiryDate = GlobalInformation.Account.ExpiryDate;
            return entity;
        }
    }
}
