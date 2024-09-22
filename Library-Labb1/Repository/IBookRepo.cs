using Library_Labb1.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Labb1.Repository
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetBooksByNameAsync(string bookName);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);

        Task SaveAsync();

        
    }
}
