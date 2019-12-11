using Microsoft.EntityFrameworkCore;
using scoala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scoala.Repositories
{
    public class ElevRepository : IElevRepository
    {
        private readonly ScoalaContext _context;
        public StudentRepository(ScoalaContext studentDbContext)
        {
            _context = studentDbContext;
        }
        public async Task Create(Elev elev)
        {
            _context.Add(elev);
            await _context.SaveChangesAsync();
        }

       
    }
}