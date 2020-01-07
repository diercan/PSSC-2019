using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSSC.Services;
using PSSC.Models.Postare;


namespace PSSC.Controllers
{
    public class PostareController : Controller
    {
        //string username;
        private IPostareService postareService;

        public PostareController(IPostareService service)
        {
            this.postareService = service;

        }


        public IActionResult Index()
        {         
            return View(postareService.GetAllPosts());
        }

        public IActionResult Stergere(Guid id)
        {

            Postare p = postareService.GetPostareById(id);
            postareService.RemovePostareFromRepository(p);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreeazaPostare()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreeazaPostare(PostareViewModel model)
        {
            //model.Autor = username;
            postareService.AddPostToRepository(model);
            return RedirectToAction(nameof(Index));
        }
    }
}