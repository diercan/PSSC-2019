using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab6.Models;

namespace lab6.Controllers
{
    public class ToDOController : Controller
    {
        private readonly ILogger<ToDOController> _logger;
        private static ToDOList toDOList;
        private List<ToDOEntry> toDOEntry=new List<ToDOEntry>();

        public ToDOController(ILogger<ToDOController> logger)
        {
            _logger = logger;
            toDOEntry.Add(new ToDOEntry("First principle","make money",Convert.ToDateTime("2019/04/30")));
            
            toDOEntry.Add(new ToDOEntry("Seconde principle","spend money",Convert.ToDateTime("2019/04/30")));

            toDOList=new ToDOList(toDOEntry);
        }

        public IActionResult Index()
        {
            return View(toDOList);
        }

    }
}
