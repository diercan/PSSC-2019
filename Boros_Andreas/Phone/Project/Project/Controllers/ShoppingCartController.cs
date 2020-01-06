using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(IPhoneRepository phoneRepository,ShoppingCart shoppingCart)
        {
            _phoneRepository = phoneRepository;
            _shoppingCart = shoppingCart;
        }
        [AllowAnonymous]
       public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart=_shoppingCart,
            };
            return View(shoppingCartViewModel);
        }
        /*
        public RedirectToActionResult AddToShoppingCart(int phoneId)
        {
            var selectedPhone = _phoneRepository.GetAllPhone.FirstOrDefault(p => p.Id == phoneId);
            if(selectedPhone !=null)
            {
                _shoppingCart.AddToCart(selectedPhone, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int phoneId)
        {
            var selectedPhone = _phoneRepository.GetAllPhone.FirstOrDefault(p => p.Id == phoneId);
            if (selectedPhone != null)
            {
                _shoppingCart.RemoveFromCart(selectedPhone);
            }
            return RedirectToAction("Index");
        }
        */


    }
}