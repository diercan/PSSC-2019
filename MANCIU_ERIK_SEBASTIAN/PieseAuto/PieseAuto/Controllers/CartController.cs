using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PieseAuto.Helpers;
using PieseAuto.Models;
using PieseAuto.Services;

namespace PieseAuto.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private DataContext db = new DataContext();


        [Route("index")]
        public IActionResult Index(string buy)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(i => i.Product.Price * i.Quantity);
            if(!string.IsNullOrEmpty(buy))
            {
                Send send = new Send("Comanda a fost efectuata!");
            }
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            if(SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart")==null)
            {
                var cart = new List<Item>();
                cart.Add(new Item() { Product = db.Piese2.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart, id);
                if(index == -1)
                {
                    cart.Add(new Item() { Product = db.Piese2.Find(id), Quantity = 1 });
                }
                else
                {
                    cart[index].Quantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(cart, id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }



        private int Exists(List<Item> cart, int id)
        {
            for(int i=0; i<cart.Count;i++)
            {
                if(cart[i].Product.Id==id)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}