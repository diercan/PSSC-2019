using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.UserComponents;
using CookBook_MVC.Models.Exceptions;
using CookBook_MVC.Models.CommonComponents;

namespace CookBook_MVC.Models.CommonComponents
{
    public class CookBookService:ICookBookService
    {
        List<User> UserList = new List<User>();
        List<User> Blacklist = new List<User>();
        List<User> PremiumUsers = new List<User>();
        public static CookBookService service= new CookBookService();
        private CookBookService() { }


        public void AddUser(User user) {
            UserList.Add(user);
        }
        public void RemoveUser(User user) {
            UserList.Remove(user);
        }
        public void AddRecipe(User user,Recipe recipe) {
            if (user.GetRecipes.Contains(recipe))
            {
                throw new ExistentRecipeException("Recipe aready exists in your CookBook");
            }
            else
                user.AddRecipe(recipe);
        

        }
        public void RemoveRecipe(User user,Recipe recipe) {
            if (user.GetRecipes.Contains(recipe))
            {
                user.RemoveRecipe(recipe);
            }
            else
                throw new ExistentRecipeException("Recipe doesn't exists in your CookBook");
            
        }
        public void Deposit(User user, float amount) {
            user.DepositInBankAccount(amount);
        }
        public void Transfer(User user,User userToTransfer, float amount){
            user.TransferInBankAccount(userToTransfer.GetBankAccount, amount);
        }
        public void UpgradeToPremium(User user) {
            PremiumUsers.Add(user);
        }
        public void DowngradeFromPremium(User user) {
            PremiumUsers.Remove(user);
        }
        public void BanUser(User user) {
            Blacklist.Add(user);
        }
        public void GenerateRankings() { }
        public void InitiateConversation(User user1, User user2) { }
        public void PayRecipe(User user, Recipe Recipe)
        {
            foreach(Recipe r in user.GetRecipes)
            {
               
            }
        }

    }
}
