using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSSC.Models.Postare;

namespace PSSC.Repository
{
    public interface IPostareRepository
    {
        void AdaugarePostare(Postare post);
        void StergerePostare(Postare post);
        List<Postare> GetAllPosts();

        Postare GetPostareById(Guid id);
    }
}
