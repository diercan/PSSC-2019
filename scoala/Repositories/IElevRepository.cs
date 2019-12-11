using scoala.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace scoala.Repositories
{
    public interface IElevRepository
    {
        Task Create(Elev elev);
        Task<List<Elev>> GetAllStudents();
    }
}