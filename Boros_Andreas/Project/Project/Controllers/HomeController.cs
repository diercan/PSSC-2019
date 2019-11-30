using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private IPhoneRepository _phoneRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IPhoneRepository phoneRepository,IHostingEnvironment hostingEnvironment)
        {
            _phoneRepository = phoneRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Route("")]
        [Route("Index")]
        [Route("~/")]
        public ViewResult Index()
        {
           var model= _phoneRepository.GetAllPhone();
            return View(model);
        }
        [Route("Details/{id?}")]

        public ViewResult Details(int? id)
        {

            Phone phone = _phoneRepository.GetPhone(id.Value);
            if(phone==null)
            {
                Response.StatusCode = 404;
                return View("PhoneNotFound",id.Value);
            }
               

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Phone = phone,
                PageTitle = "Phone Details"
            };
            return View(homeDetailsViewModel);
        }
        
        [Route("Edit/{id?}")]  
       
        public ViewResult Edit (int id)
        {
            Phone phone = _phoneRepository.GetPhone(id);
            PhoneEditViewModel phoneEditViewModel = new PhoneEditViewModel
            {
                Id = phone.Id,
                Name = phone.Name,
                Type = phone.Type,
                Color = phone.Color,
                Dimension = phone.Dimension,
                ExistingPhotoPath = phone.PhotoPath

            };
            return View(phoneEditViewModel);
        }

     
     
        public IActionResult Update (PhoneEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Phone phone = _phoneRepository.GetPhone(model.Id);
                phone.Name = model.Name;
                phone.Type = model.Type;
                phone.Color = model.Color;
                phone.Dimension = model.Dimension;
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                       string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                   phone.PhotoPath = ProcessUploadedFile(model);
                }
               
                _phoneRepository.Update(phone);
                return RedirectToAction("index");
            }
            return View();
        }
        
        private string ProcessUploadedFile(PhoneCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                {
                    model.Photo.CopyTo(fileStream); 
                }
                
            }

            return uniqueFileName;
        }

       
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PhoneCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Phone newphone = new Phone
                {
                    Name = model.Name,
                    Color = model.Color,
                    Type = model.Type,
                    Dimension = model.Dimension,
                    PhotoPath = uniqueFileName
                };
                _phoneRepository.Add(newphone);
                return RedirectToAction("details", new { id = newphone.Id });
            }
            return View();
        }

    }
}
