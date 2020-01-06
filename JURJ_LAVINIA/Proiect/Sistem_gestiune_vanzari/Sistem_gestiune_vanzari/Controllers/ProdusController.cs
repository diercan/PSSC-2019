using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistem_gestiune_vanzari.Controllers
{
    
    public class ProdusController : Controller
    {
        // GET: Produs
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Produs()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tabel_Produs>().GetValoare());
        }
        public ActionResult EditareProdus(string id_produs)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tabel_Produs>().GetFirstorDefault(id_produs));
        }
        [HttpPost]
        public ActionResult EditareProdus(Tabel_Produs _valoare_in_tabel, HttpPostedFileBase file)
        {
            string imagine_adaugata = null;
            if (file != null)
            {
                imagine_adaugata = System.IO.Path.GetFileName(file.FileName);
                string cale = System.IO.Path.Combine(Server.MapPath("~/ImaginiProduse"), imagine_adaugata);
                file.SaveAs(cale);
            }
            _valoare_in_tabel.imagine = file != null ? imagine_adaugata : _valoare_in_tabel.imagine;
            _unitOfWork.GetRepositoryInstance<Tabel_Produs>().Update(_valoare_in_tabel);
            return RedirectToAction("Produs");
        }
        public ActionResult AdaugareProdus(string id_produs)
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult AdaugareProdus(Tabel_Produs _valoare_in_tabel, HttpPostedFileBase file)
        {
            string imagine_adaugata = null;
            if(file != null)
            {
                imagine_adaugata = System.IO.Path.GetFileName(file.FileName);
                string cale = System.IO.Path.Combine(Server.MapPath("~/ImaginiProduse"), imagine_adaugata);
                file.SaveAs(cale);
            }
            _valoare_in_tabel.imagine = imagine_adaugata;
            _unitOfWork.GetRepositoryInstance<Tabel_Produs>().Add(_valoare_in_tabel);
            return RedirectToAction("Produs");
        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tabel_Categorie>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.identitate_categorie.ToString(), Text = item.nume_categorie });
            }
            return list;
        }
    }

}
