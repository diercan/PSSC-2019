using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSSC.Models;
using RabbitMQ.Client;
using System.Text;

namespace PSSC.Controllers
{
    public class CarController : Controller
    {
        private EmpDBContext db = new EmpDBContext();
        // GET: Car/ShowCarList
        
        
        public ActionResult CarList()
        {
            var cars = from e in db.Cars
                            orderby e.Id
                            select e;
            return View(cars);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Car c)
        {
            try
            {
                db.Cars.Add(c);
                db.SaveChanges();
                return RedirectToAction("CarList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var cars = db.Cars.Single(m => m.Id == id);
            return View(cars);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("CarList");

        }

        public ActionResult Details(int id)
        {
            var cars = db.Cars.Single(m => m.Id == id);
            return View(cars);
        }


        public ActionResult Edit(int? id)
        {
            var cars = db.Cars.Single(m => m.Id == id);
            return View(cars);
        }
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            try
            {
                var cars = db.Cars.Single(m => m.Id == id);
                if (TryUpdateModel(cars))
                {
                    //To Do:- database code
                    db.SaveChanges();
                    return RedirectToAction("CarList");
                }
                return View(cars);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BuyRent(int id)
        {
            var cars = db.Cars.Single(m => m.Id == id);
            return View(cars);
        }
        [HttpPost]
        
        public ActionResult Buy(int? id)
        {
            return Content("Congratulation for your choice!");          
        }
        public ActionResult Rent(FormCollection collection)
        {
            RQueue(collection);
            return Content("Rent");
        }
        public void RQueue(FormCollection collenction)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = collenction.ToString();
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: "task_queue",
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}