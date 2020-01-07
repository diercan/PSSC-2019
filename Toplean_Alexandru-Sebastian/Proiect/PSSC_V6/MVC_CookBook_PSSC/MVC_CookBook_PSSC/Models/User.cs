using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;
using MVC_CookBook_PSSC.Models.UserComponents;
using MVC_CookBook_PSSC.Models.Exceptions;
using System.ComponentModel.DataAnnotations;
using MVC_CookBook_PSSC.Models.EmailComponents;
using System.ComponentModel.DataAnnotations.Schema;
using MVC_CookBook_PSSC.Models.ValidationAttributes;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace MVC_CookBook_PSSC.Models
{
    [Table("users")]
    public class User
    {


        private Integer _id;
        private Text _name, _surename,_username,_password;
        private CNP _cnp;
        private IBAN _iban;
        private Number _money;
        //https://dzone.com/articles/creating-your-own-validation-attribute-in-mvc-and
        //https://riptutorial.com/csharp/example/18486/creating-a-custom-validation-attribute
        private EmailAddress _emailAddress;
        private List<Recipe> _recipes = new List<Recipe>();
        private BankAccount _bankAccount;
        private bool _premium;
      

        public User() { }
       

        #region User Table
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Username")]
        [Required(ErrorMessage ="The Username field cannot be empty")]
        public String strUsername { get; set; }

        [Column("Password")]
        [PasswordAttr(ErrorMessage = "The Password must be 8 characters long, one upper letter, one number")]
        public String strPassword { get; set; }


        [Column("IpAddress")]
        public String strIpAddress { get; set; }

        [Column("Name")]
        [StringLength(255,ErrorMessage ="The Name field cannot be empty")]
        public String strName { get; set; }

        [Column("Surename")]
        [StringLength(255,ErrorMessage = "The Surename field cannot be empty")]
        public String strSurename { get; set; }
        
        [Column("CNP")]
        [CnpAttr(ErrorMessage ="Invalid CNP(13 digits). If foreign CNP, add \"*\" at the beginning.")]
        public String strCNP { get; set; }

        [Column("Email")]
        [EmailAttr(ErrorMessage = "Email invalid or empty field")]
        public String strEmail { get; set; }

        [Column("IBAN")]
        public String strIBAN { get; set; }

        [Column("Money")]
        public float strMoney { get; set; }

        [Column("Premium")]
        public bool strPremium { get; set; }
        [Column("isLogged")]
        public bool strIsLogged { get; set; }
        [Column("cookie")]
        public String strCookie { get; set; }
        #endregion
        #region User Functions

        
        public async Task CreateValueObjectUser(IFormCollection form)
        {
            try { this._name = new Text(form["strName"]); } catch { }
             try{this._surename = new Text(form["strSurename"]); } catch { }
            try{this._username = new Text(form["strUsername"]); }catch { }
            try{this._password = new Text(CryptoManager.ComputeSha256Hash(form["strPassword"])); }catch { }
            try{this._cnp = new CNP(new Text(form["strCNP"])); }catch { }
            try{this._iban = new IBAN(new Text(form["strIBAN"])); }catch { }
            try{this._emailAddress = new EmailAddress(new Text(form["strEmail"])); }catch { }
            try{if(this._iban != null) this._bankAccount = new BankAccount(this._iban, 0); }catch { }


        }
        public async Task UpdateValueObjectUser(IFormCollection form)
        {
            try { this._name = new Text(form["strName"]); } catch { }
            try { this._surename = new Text(form["strSurename"]); } catch { }
 

            try { this._cnp = new CNP(new Text(form["strCNP"])); } catch { }
            try { this._iban = new IBAN(new Text(form["strIBAN"])); } catch { }

            try { if (this._iban != null) this._bankAccount = new BankAccount(this._iban, 0); } catch { }


        }
        public void CreateDatabaseObject()
        {
            try{this.strName = _name.GetText;}catch { }
            try{this.strSurename = _surename.GetText;}catch { }
            try{this.strUsername = _username.GetText;}catch { }
            try{this.strPassword = _password.GetText;}catch { }
            try{this.strCNP = _cnp.GetCNP.GetText;}catch { }
            try{this.strIBAN = _iban.GetIBAN.GetText;}catch { }
            try{this.strEmail = _emailAddress.GetEmailAddress.GetText;}catch { }
           
         }

        public void AddBankAccount(BankAccount bankAccount)
        {
            this._bankAccount = bankAccount;

        }
        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }
        public void RemoveRecipe(Recipe recipe)
        {
            _recipes.Remove(recipe);
        }
        public void DepositInBankAccount(float amount)
        {
            if (_bankAccount != null)
            {
                _bankAccount.Deposit(amount);
            }
            else
                throw new InexistentBankAccountException("There is no Bank Account linked to this account!");
        }
        public void TransferInBankAccount(BankAccount account, float amount)
        {
            if (_bankAccount != null && account != null)
            {
                _bankAccount.Transfer(account, amount);
            }
            else
                throw new InexistentBankAccountException("There is no Bank Account linked to this account!");
        }
        #endregion
        #region User Get Data

        public int GetUserID { get { return ID; } }
        public Text GetName { get { return this._name; } }
        public Text GetUsername { get { return this._username; } }
        public Text GetSurename { get { return this._surename; } }
        public Text GetPassword { get { return this._password; } }
        public CNP GetCNP { get { return this._cnp; } }
        public EmailAddress GetEmailAddress { get { return this._emailAddress; } }
        public bool GetPremium { get { return this._premium; } }
        public IBAN GetIBAN { get { return this._iban; } }
        public BankAccount GetBankAccount { get { return this._bankAccount; } }
        public List<Recipe> GetRecipes { get { return _recipes; } }
        #endregion

    }
}
