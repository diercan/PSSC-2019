using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FrBaschet.Domain.Entities;
using FrBaschet.Domain.Interfaces;
using FrBaschet.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace FrBaschet.API.Controllers
{
    public class DashboardController : Controller
    {
        private FrBaschetContext _context;

        public DashboardController(IRepository<GameEntityModel> gameRepository, FrBaschetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Games = _context.GameEntityModels
                .Include(a => a.AwayTeamEntityModel)
                .Include(a => a.HomeTeamEntityModel)
                .Select(a => new
                {
                    title = a.HomeTeamEntityModel.Name + "  -  " + a.AwayTeamEntityModel.Name,
                    start = a.Date,
                    end = a.Date.AddHours(1),
                    url = "game/"+a.Id
                });
            return View();
        }

        // [Route("[controller]/calendar")]
        // public IActionResult Calendar()
        // {
        //     return View();
        // }
        // [Route("[controller]/calendar2")]
        // public IActionResult Calendar2()
        // {
        //     return View();
        // }
    }
}