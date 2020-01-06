using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminSideAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminSideAPP.Data
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options)
            : base(options)
        {

        }

        public DbSet<ReservationModel> Reservations { get; set; }
    }
}
