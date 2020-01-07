using APPicola.Models;
using System;
using System.Web.Mvc;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace APPicola.Controllers
{
    public class AdminController : Controller
    {
        ConnImplement ci = new ConnImplement();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult InfoArticole()
        {
            ModelState.Clear();
            return View(ci.GetSqlRowsArticole());
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Articole articol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Text = ci.InsertSqlRowsArticole(articol);
                }

                var factory = new ConnectionFactory()
                {
                    Uri = new Uri("amqp://eoqhublt:bDVoI6WQ-EEsxMPV__5isF4E3KEu9LDb@shark.rmq.cloudamqp.com/eoqhublt"),
                    UserName = "eoqhublt",
                    Password = "bDVoI6WQ-EEsxMPV__5isF4E3KEu9LDb",
                };
                SendMessage(factory, articol);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private static void SendMessage(ConnectionFactory factory, Articole model)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test", durable: false, exclusive: false, autoDelete: false, arguments: null);


                var json = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "test", basicProperties: null, body: body);
            }
        }
    }
}
