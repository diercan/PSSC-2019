using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Models;
using Sistem_gestiune_vanzari.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace Sistem_gestiune_vanzari.Controllers
{
    public class AdminController : Controller
    {
        //data context
        dbSistem_gestiune_vanzariEntities _DBEntitate = new dbSistem_gestiune_vanzariEntities();
        // GET: Admin/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Comanda()
        {
            return View();
        }
        public ActionResult DetaliiComanda()
        {
            return View();
        }
        public ActionResult ExportPDFComanda()
        {
 
            var q = new ViewAsPdf("ExportPDFComandaView");
            return q;

        }
        public ActionResult ExportPDFComandaView()
        {
            return View();
        }
        
        public ActionResult AdaugareInComanda(string ProdusId)
        {
            
            if (Session["comanda"] == null)
            {
                List<ProdusInComanda> comanda = new List<ProdusInComanda>();
                var produs = _DBEntitate.Tabel_Produs.Find(ProdusId);
                //var client = _DBEntitate.Tabel_Client.Find(ClientId);
                comanda.Add(new ProdusInComanda()
                {
                    Produs = produs,
                    Cantitate = 1
                    //Client = client//client
                });
                //variable to store items into list
                Session["comanda"] = comanda;
            }
            else
            {
                List<ProdusInComanda> comanda = (List<ProdusInComanda>)Session["comanda"];
                var produs = _DBEntitate.Tabel_Produs.Find(ProdusId);
                
                foreach (var item in comanda.ToList())
                {
                    if(item.Produs.identitate_produs == ProdusId)
                    {
                        int cantitate_anterioara = item.Cantitate;
                        comanda.Remove(item);
                        comanda.Add(new ProdusInComanda()
                        {
                            Produs = produs,
                            Cantitate = cantitate_anterioara + 1,
                            
                        });
                        break;
                    }
                    else
                    {
                        comanda.Add(new ProdusInComanda()
                        {
                            Produs = produs,
                            Cantitate = 1,
                            
                        });
                        break;
                    }
                }
                //variable to store items into list
                Session["comanda"] = comanda;
                
            }
            return RedirectToAction("Produs", "Produs");

         }
       
        public ActionResult MicsorareCantitate(string ProdusId)
        {
            if (Session["comanda"] != null)
            {
                List<ProdusInComanda> comanda = (List<ProdusInComanda>)Session["comanda"];
                var produs = _DBEntitate.Tabel_Produs.Find(ProdusId);
                //var client = _DBEntitate.Tabel_Client.Find(ClientId);
                foreach (var item in comanda)
                {
                    if (item.Produs.identitate_produs == ProdusId)
                    {
                        int cantitate_anterioara = item.Cantitate;
                        if (cantitate_anterioara > 0)
                        {
                            comanda.Remove(item);
                            comanda.Add(new ProdusInComanda()
                            {
                                Produs = produs,
                                Cantitate = cantitate_anterioara - 1,
                                
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = comanda;
            }
            return Redirect("Comanda");
        }
    }
}