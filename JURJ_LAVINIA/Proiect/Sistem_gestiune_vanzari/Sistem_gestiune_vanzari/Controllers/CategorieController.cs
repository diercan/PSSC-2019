using Newtonsoft.Json;
using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Models;
using Sistem_gestiune_vanzari.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistem_gestiune_vanzari.Controllers
{
    public class CategorieController : Controller
    {
        // GET: Categorie
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Categorii()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tabel_Categorie>().GetValoare());
        }
        public ActionResult EditareCategorie(int id_categorie)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tabel_Categorie>().GetFirstorDefaultCategorie(id_categorie));
        }
        [HttpPost]
        public ActionResult EditareCategorie(Tabel_Categorie _valoare_in_tabel)
        {
            _unitOfWork.GetRepositoryInstance<Tabel_Categorie>().Update(_valoare_in_tabel);
            return RedirectToAction("Categorii");
        }
        public ActionResult AdaugareCategorie(string Nume_categorie)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdaugareCategorie(Tabel_Categorie _valoare_in_tabel)
        {
            _unitOfWork.GetRepositoryInstance<Tabel_Categorie>().Add(_valoare_in_tabel);
            return RedirectToAction("Categorii");
        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tabel_Categorie>().GetAllRecords();
            foreach(var item in cat)
            {
                list.Add(new SelectListItem { Value = item.identitate_categorie.ToString(), Text = item.nume_categorie });
            }
            return list;
        }
    }
}