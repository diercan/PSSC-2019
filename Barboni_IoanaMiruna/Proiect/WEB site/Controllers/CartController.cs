using LegumeDeBelint.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LegumeDeBelint.Controllers
{
    public class CartController : Controller
    {
        
        // GET: Cart
        public ActionResult Index(string id)
        {
            vegetableModel vegetableModel = new vegetableModel();
            ViewBag.vegetables = vegetableModel.findAll(); 
            return View();
        }
        public ActionResult Cumpara(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            var total = cart.Sum(item => item.Vegetable.Price * item.Quantity);
        
        var viewModel = new Item();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                string message = total.ToString();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                
               
                   
                    if (cart.Count > 0)
                    {
                        for (int i = 0; i < cart.Count; i++)
                        {
                            cart.RemoveAt(i);
                            
                        }
                    }
                 }
                Session["cart"] = cart;
                return RedirectToAction("Index");
            }
        

        public ActionResult Buy(string id)
        {

            vegetableModel vegetableModel = new vegetableModel();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Vegetable = vegetableModel.find(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Vegetable = vegetableModel.find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Vegetable.Id.Equals(id))
                    return i;
            return -1;

        }
    }
}