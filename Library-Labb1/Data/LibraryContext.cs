using Library_Labb1.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Labb1.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(new Book()
            {
                Id = 1,
                Title = "Harry Potter 1",
                Author = "JK Rowling",
                YearPublished = 2008,
                Genre = "Fantasy",
                Description = "Harry Potter finds out he is a wizard and needs to fight evil",
                IsAvailable = true,
            },
            new Book()
            {
                Id=2,
                Title = "Harry Potter 2",
                Author = "JK Rowling",
                YearPublished = 2011,
                Genre = "Fantasy",
                Description = "Harry Potter finds out he is a wizard and needs to fight evil",
                IsAvailable = false,
            });
        }
        }
}
