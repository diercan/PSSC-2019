using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSSC.Repository;
using PSSC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace PSSC.Controllers
{
    public class RezervareController : Controller
    {
        private readonly IConfiguration configuration;

        private readonly IRezervareRepository rezervareRepository;

        private static void SendMessage(ConnectionFactory factory, Comunicare model)
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
        public RezervareController(IRezervareRepository rezervareRepository, IConfiguration config)
        {
            this.rezervareRepository = rezervareRepository;
            this.configuration = config;
        }

        // GET: Rezervare
        public ActionResult Index()
        {
            
            return View(rezervareRepository.ObtineRezervari());
        }

        // GET: Rezervare/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rezervare/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rezervare/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rezervare rezervare)
        {
            try
            {

                var factory = new ConnectionFactory()
                {
                    Uri = new Uri("amqp://lpoqlqsc:1r-UxN...@golden-kangaroo.rmq.cloudamqp.com/lpoqlqsc"),
                    UserName = "lpoqlqsc",
                    Password = "1r-UxNN_LJ7zNk9sAzubWYGyFHmtLkVW",
                };
                var model = new Comunicare(){ Mesaj="Rezervare noua"};
                SendMessage(factory, model);


                double pret = 0;
                rezervare.IdUnic = Guid.NewGuid();
                if (rezervare.murdarie.Equals(stareMasina.foarte_murdara))
                    pret= pret + 30 ;
                else if (rezervare.murdarie.Equals(stareMasina.murdara))
                    pret = pret + 20;
                else if (rezervare.murdarie.Equals(stareMasina.relativ_curata))
                    pret = pret + 15;
                
                if (rezervare.optiune1.Equals(optiuni.ceara)||rezervare.optiune2.Equals(optiuni.ceara)||
                    rezervare.optiune3.Equals(optiuni.ceara) || rezervare.optiune4.Equals(optiuni.ceara))
                    pret = pret + 5;
                if (rezervare.optiune1.Equals(optiuni.portbagaj) || rezervare.optiune2.Equals(optiuni.portbagaj) ||
                    rezervare.optiune3.Equals(optiuni.portbagaj) || rezervare.optiune4.Equals(optiuni.portbagaj))
                    pret = pret + 10;
                if (rezervare.optiune1.Equals(optiuni.exterior) || rezervare.optiune2.Equals(optiuni.exterior) ||
                    rezervare.optiune3.Equals(optiuni.exterior) || rezervare.optiune4.Equals(optiuni.exterior))
                    pret = pret + 10;
                if (rezervare.optiune1.Equals(optiuni.interior) || rezervare.optiune2.Equals(optiuni.interior) ||
                    rezervare.optiune3.Equals(optiuni.interior) || rezervare.optiune4.Equals(optiuni.interior))
                    pret = pret + 10;
                rezervare.total = pret;
                rezervareRepository.CreareRezervare(rezervare);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezervare/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rezervare/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezervare/Delete/5
        public ActionResult Delete(Guid id)
        {
            var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
            return View(rezervare);
        }

        // POST: Rezervare/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
                rezervareRepository.StergeRezervare(rezervare);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}