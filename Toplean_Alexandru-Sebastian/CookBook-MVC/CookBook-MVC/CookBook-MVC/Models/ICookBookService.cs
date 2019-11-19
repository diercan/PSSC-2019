using CookBook_MVC.Models.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook_MVC.Models
{
    public interface ICookBookService
    {
        void AddUser(User user);
        void RemoveUser(User user);
        void AddRecipe(User user, Recipe recipe);
        void RemoveRecipe(User user, Recipe recipe);
        void Deposit(User user,float amount);
        void Transfer(User user, User userToTransfer, float amount);
        void UpgradeToPremium(User user);
        void DowngradeFromPremium(User user);
        void BanUser(User user);
        void InitiateConversation(User user1, User user2);
        void PayRecipe(User user, Recipe recipe);

        
    }
}
