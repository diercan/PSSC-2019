using System.Data.Entity;

namespace scoala.Models
{
    public class ScoalaContext : DbContext

    {
        public ScoalaContext() : base("asanuContext")
        {
        }
        public virtual DbSet<Elev> Elevi { get; set; }
        public virtual DbSet<Situatie> Situatii { get; set; }

    }
}