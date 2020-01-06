using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookReview.Data;
namespace BookReview.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcBookContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcBookContext>>()))
            {
                //Look for any books
                if (context.Book.Any())
                {
                    return; //DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "The Chronicles of Narnia:The Lion, the Witch and the Wardrobe",
                        Author = "C. S. Lewis",
                        Genre = "Fantasy",
                        ReleaseDate = DateTime.Parse("1951-10-15"),
                        Review = 8
                    },

                    new Book
                    {
                        Title = "Harry Potter and the Chamber of Secrets",
                        Author = "J.K.Rowling",
                        Genre = "Fantasy",
                        ReleaseDate = DateTime.Parse("1998-07-02"),
                        Review = 9
                    },

                    new Book
                    {
                        Title = "City of Bones",
                        Author = "Cassahndra Clare",
                        Genre = "Adventure",
                        ReleaseDate=DateTime.Parse("2007-03-27"),
                        Review = 6
                    }
                 );
                context.SaveChanges();
            }
        }
    }
}
