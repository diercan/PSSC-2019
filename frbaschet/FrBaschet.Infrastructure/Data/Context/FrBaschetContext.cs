using System;
using FrBaschet.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrBaschet.Infrastructure.Data.Context
{
    public class FrBaschetContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RefereeEntity> RefereeEntities { get; set; }
        public DbSet<CommissionerEntity> CommissionerEnties { get; set; }
        public DbSet<InvitationEntityModel> InvitationEntityModels { get; set; }
        public DbSet<GameEntityModel> GameEntityModels { get; set; }
        public DbSet<TeamEntityModel> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cString =
                Environment.GetEnvironmentVariable("ConnectionString");
            optionsBuilder
                .UseMySql(cString ?? throw new Exception("ConnectionString can not be null"),
                    b => b.MigrationsAssembly("FrBaschet.Infrastructure"));
        }
    }
}