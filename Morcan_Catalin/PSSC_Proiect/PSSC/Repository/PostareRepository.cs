using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PSSC.Models.Postare;

namespace PSSC.Repository
{
    public class PostareRepository : IPostareRepository
    {
        private readonly List<Models.Postare.Postare> _postari;

        public PostareRepository()
        {


            _postari = new List<Models.Postare.Postare>();

            PostareFactory postareFactory = new PostareFactory();
            _postari.Add(postareFactory.CreeazaPostare("a", "a", "a", "a", (GradImportantaPostare)1,
                (TipPostare)1, DateTime.Now));

            _postari.Add(postareFactory.CreeazaPostare("b", "b", "b", "b", (GradImportantaPostare)1,
                (TipPostare)1, DateTime.Now));
        }
        public void AdaugarePostare(Postare post)
        {
            if (post != null)
            {
                _postari.Add(post);
            }
        }
        public void StergerePostare(Postare post)
        {
            _postari.Remove(post);

        }
        public List<Postare> GetAllPosts()
        {
            return _postari;
        }

        public Postare GetPostareById(Guid id)
        {
            return _postari.FirstOrDefault(_ => _.id == id);
        }


    }
}
