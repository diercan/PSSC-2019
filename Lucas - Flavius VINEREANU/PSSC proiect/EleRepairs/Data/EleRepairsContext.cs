using Microsoft.EntityFrameworkCore;
using EleRepairs.Models;

namespace EleRepairs.Data
{
    public class EleRepairsContext : DbContext
    {
        public EleRepairsContext (DbContextOptions<EleRepairsContext> options)
            : base(options)
        {
        }

        public DbSet<Repairs> Repairs { get; set; }
    }
}