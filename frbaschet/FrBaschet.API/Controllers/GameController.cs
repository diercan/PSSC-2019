using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FrBaschet.Domain.Entities;
using FrBaschet.Domain.Interfaces;
using FrBaschet.Domain.ViewModels;
using FrBaschet.Infrastructure.Data;
using FrBaschet.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrBaschet.API.Controllers
{
    public class GameController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<GameEntityModel> _gameRepository;
        private readonly IRepository<TeamEntityModel> _teamRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private FrBaschetContext _context;

        public GameController(IRepository<GameEntityModel> gameRepository, IRepository<TeamEntityModel> teamRepository,
            FrBaschetContext context)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _context = context;

            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GameViewModel, GameEntityModel>();
                    cfg.CreateMap<TeamViewModel, TeamEntityModel>();
                });
            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("game/{id}")]
        public IActionResult Game(Guid id)
        {
            // return Ok(User.);
            var game = _context.GameEntityModels
                .Include(a => a.Commissioner)
                .Include(a => a.Referee2)
                .Include(a => a.Referee1)
                .Include(a => a.HomeTeamEntityModel)
                .Include(a => a.AwayTeamEntityModel)
                .First(a => a.Id == id);

            GameView Game = new GameView();
            Game.title = game.HomeTeamEntityModel.Name + "  -  " + game.AwayTeamEntityModel.Name;
            Game.data = game.Date;

            if (game.Commissioner != null)
            {
                Game.commissioner = game.Commissioner.FirstName + " " + game.Commissioner.LastName.ToUpper();
            }

            ViewBag.Game = Game;
            if (game.Referee1 != null)
            {
                Game.referee1 = game.Referee1.FirstName + " " + game.Referee1.LastName.ToUpper();
            }

            if (game.Referee2 != null)
            {
                Game.referee2 = game.Referee2.FirstName + " " + game.Referee2.LastName.ToUpper();
            }

            var refereeList = _context.RefereeEntities
                .Select(a => new
                {
                    Value = a.Id,
                    Text = a.FirstName + "  -  " + a.LastName.ToUpper(),
                });
            var commissionerList = _context.CommissionerEnties.Select(a => new
            {
                Value = a.Id,
                Text = a.FirstName + "  -  " + a.LastName.ToUpper(),
            });
            ViewBag.refereeList = refereeList;
            ViewBag.commissionerList = commissionerList;
            return View("FGame");
        }

        [HttpPost]
        [Route("game/{id}")]
        public IActionResult Game(Guid id, UpdateGameViewModel updateGameViewModel)
        {
            var game = _context.GameEntityModels.First(g => g.Id == id);
            var c = _context.CommissionerEnties.First(g => g.Id == updateGameViewModel.Commissioner.ToString());
            var r1 = _context.RefereeEntities.First(g => g.Id == updateGameViewModel.Referee1.ToString());
            var r2 = _context.RefereeEntities.First(g => g.Id == updateGameViewModel.Referee2.ToString());
            game.Commissioner = c;
            game.Referee1 = r1;
            game.Referee2 = r2;
            _context.Update(game);
            _context.SaveChanges();
            return LocalRedirect(Url.Action("", "Home"));
        }

        [HttpPost]
        public async Task<IActionResult> Index(GameViewModel gameViewModel)
        {
            var game = _mapper.Map<GameViewModel, GameEntityModel>(gameViewModel);
            var awayT = _teamRepository.GetQueryable().FirstOrDefault(a => a.Name == gameViewModel.AwayTeam.Name);
            var homeT = _teamRepository.GetQueryable().FirstOrDefault(a => a.Name == gameViewModel.HomeTeam.Name);

            if (awayT != null)
            {
                game.AwayTeamEntityModel = awayT;
            }
            else
                game.AwayTeamEntityModel = new TeamEntityModel
                {
                    Name = gameViewModel.AwayTeam.Name
                };

            if (homeT != null)
            {
                game.HomeTeamEntityModel = homeT;
            }
            else
                game.HomeTeamEntityModel = new TeamEntityModel
                {
                    Name = gameViewModel.HomeTeam.Name
                };

            _gameRepository.Add(game);
            _gameRepository.SaveChanges();
            return View();
        }
    }
}