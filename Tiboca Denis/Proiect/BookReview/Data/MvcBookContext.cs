using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookReview.Models;

namespace BookReview.Data
{
    public class MvcBookContext : DbContext
    {
        public MvcBookContext(DbContextOptions<MvcBookContext> options) : base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
    }
}
