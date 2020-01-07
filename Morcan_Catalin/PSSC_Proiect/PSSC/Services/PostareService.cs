using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using PSSC.Models.Postare;
using PSSC.Repository;
using SendEmail;

namespace PSSC.Services
{
    public interface IPostareService
    {
        List<PostareViewModel> GetAllPosts();
        void AddPostToRepository(PostareViewModel pvm);

        public void RemovePostareFromRepository(Postare post);

        public Postare GetPostareById(Guid id);
    }


    public class PostareService : IPostareService
    {

        private readonly IPostareRepository repository;

        private readonly IBusControl bus;


        public PostareService(IBusControl bus,IPostareRepository repository)
        {
            this.repository = repository;
            this.bus = bus;
        }

        public List<PostareViewModel> GetAllPosts()
        {
            List<PostareViewModel> listaPostari = new List<PostareViewModel>();
            foreach (Postare item in repository.GetAllPosts())
            {
                PostareViewModel pvm = new PostareViewModel();
                pvm.Autor = item.Autor.Text;
                pvm.Titlu = item.Titlu.Text;
                pvm.Topic = item.Topic.Text;
                pvm.Tip = item.Tip;
                pvm.Body = item.Body.Text;
                pvm.NivelImportanta = item.NivelImportanta;
                pvm.DataPostarii = item.DataPostarii;
                listaPostari.Add(pvm);
            }
            return listaPostari;
        }

        public void AddPostToRepository(PostareViewModel pvm)
        {

            PostareFactory postareFactory = new PostareFactory();
            Postare postare = postareFactory.CreeazaPostare(pvm.Titlu, pvm.Autor, pvm.Body, pvm.Topic,
                pvm.NivelImportanta, pvm.Tip, DateTime.Now);
            repository.AdaugarePostare(postare);
            bus.Publish(new PostareImportantaCreata()
            {
                Titlu = postare.Titlu.Text,
                Body = postare.Body.Text
            }) ;
        }

        public void RemovePostareFromRepository(Postare post)
        {
            repository.StergerePostare(post);
        }

        public Postare GetPostareById(Guid id)
        {
            return repository.GetPostareById(id);

        }
    }
}
