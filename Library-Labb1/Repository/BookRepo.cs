using Library_Labb1.Data;
using Library_Labb1.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Library_Labb1.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly LibraryContext _context;

        public BookRepo(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Book>> GetBooksByNameAsync(string bookName)
        {
            return await _context.Books
                                 .Where(b => b.Title.Contains(bookName))
                                 .ToListAsync();
        }

    }
}
