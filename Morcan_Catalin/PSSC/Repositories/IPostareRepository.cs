using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSSC.Models.Modele.Postare;

namespace PSSC.Repositories
{
    interface IPostareRepository
    {
        void AdaugarePostare(Postare post);
        void StergerePostare(Postare post);
        List<Postare> GetAllPosts();
    }
}
