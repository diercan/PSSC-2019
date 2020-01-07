using System.Data.Entity;
using Data.Model;

namespace Data
{
    public interface IDatcDbContext
    {
        DbSet<Candidate> Candidates { get; set; }
        DbSet<Voter> Voters { get; set; }
    }
}