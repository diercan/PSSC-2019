using AutoMapper;
using FrBaschet.API.Extensions;
using FrBaschet.Domain.Entities;
using FrBaschet.Domain.Interfaces;
using FrBaschet.Domain.Models;
using FrBaschet.Domain.ViewModels;
using FrBaschet.Infrastructure.Helpers;
using FrBaschet.Services.Interfaces;
using FrBaschet.Services.Templates;
using Microsoft.AspNetCore.Mvc;

namespace FrBaschet.API.Controllers
{
    public class InvitationController : BaseController
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IRepository<InvitationEntityModel> _repository;

        public InvitationController(IEmailSender emailSender, IMapper mapper,
            IRepository<InvitationEntityModel> repository)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _repository = repository;

            var config =
                new MapperConfiguration(cfg => { cfg.CreateMap<InvitationViewModel, InvitationEntityModel>(); });
            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(InvitationViewModel invitationViewModel)
        {
            var inv = _mapper.Map<InvitationViewModel, InvitationEntityModel>(invitationViewModel);
            inv.Code = SecurityHelper.GetRandomString();

            var callbackUrl = $"{Request.GetBaseUrl()}/account/register?code={inv.Code}";

            _emailSender.SendEmailAsync(new InvitationModel(new EmailAddress(invitationViewModel.Email), callbackUrl));

            _repository.Add(inv);
            _repository.SaveChanges();

            return View();
        }
    }
}