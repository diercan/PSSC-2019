using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Repository;
using System.Web.Mvc;

namespace Sistem_gestiune_vanzari.Controllers
{
    public class ClientController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Client()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tabel_Client>().GetValoare());
        }
        public ActionResult EditareClient(int id_client)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tabel_Client>().GetFirstorDefault(id_client));
        }
        [HttpPost]
        public ActionResult EditareClient(Tabel_Client _valoare_in_tabel)
        {
            _unitOfWork.GetRepositoryInstance<Tabel_Client>().Update(_valoare_in_tabel);
            return RedirectToAction("Client");
        }
        public ActionResult AdaugareClient(string nume_client)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdaugareClient(Tabel_Client _valoare_in_tabel)
        {
            _unitOfWork.GetRepositoryInstance<Tabel_Client>().Add(_valoare_in_tabel);
            return RedirectToAction("Client");
        }


    }
}