using AutoMapper;
using FluentValidation;
using Library_Labb1.Models;
using Library_Labb1.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using static Azure.Core.HttpHeader;

namespace Library_Labb1.Endpoints
{
    public static class LibraryEndpoints
    {
        public static void ConfigurationLibraryEndpoints(this WebApplication app)
        {

            app.MapGet("/api/books", GetAllBooks)
                .WithName("GetBooks")
                .Produces<APIResponse>();

            app.MapGet("/api/book/{id:int}", GetBookById)
                .WithName("GetBookById")
                .Produces<APIResponse>();

            app.MapGet("/api/book/{name}", GetBooksByName)
                .WithName("GetBookByName")
                .Produces<APIResponse>();

            app.MapPost("/api/book", AddBook)
                .WithName("AddBook")
                .Accepts<Book>("application/json")
                .Produces<Book>(201)
                .Produces(404);

            app.MapPut("/api/book/{id:int}", UpdateBook)
                .WithName("UpdateCoupon")
                .Accepts<Book>("application/json")
                .Produces<Book>(200)
                .Produces(400)
                .Produces(404);

            app.MapDelete("/api/book/{id:int}", DeleteBook).WithName("DeleteBook");

        }

        private async static Task<IResult> GetAllBooks(IBookRepo _book)
        {
            APIResponse resp = new APIResponse
            {
                Result = await _book.GetAllBooksAsync(),
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return Results.Ok(resp);
        }

        private async static Task<IResult> GetBookById(IBookRepo _book, int id)
        {
            APIResponse apiResponse = new APIResponse
            {
                Result = await _book.GetBookByIdAsync(id),
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };
            return Results.Ok(apiResponse);
        }

        private async static Task<IResult> GetBooksByName(IBookRepo _bookrepo, string bookName)
        {
            APIResponse response = new APIResponse
            {
                Result = await _bookrepo.GetBooksByNameAsync(bookName),
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

            return Results.Ok(response);
        }

        private async static Task<IResult> AddBook(IBookRepo _bookrepo, Book _book, IValidator<Book> _validation)
        {
            
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var validatorResult = await _validation.ValidateAsync(_book);
            if (!validatorResult.IsValid)
            {
                response.ErrorMessage.Add(validatorResult.Errors.FirstOrDefault().ToString());
                return Results.BadRequest(response);
            }

            //if (await _bookrepo.GetBooksByNameAsync(_book.Title) != null)
            //{
            //    response.ErrorMessage.Add("Book already exists");
            //    return Results.BadRequest(response);
            //}
           
            await _bookrepo.AddBookAsync(_book);
            await _bookrepo.SaveAsync();

            // Skapa ett positivt svar
            response.Result = _book;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;

            // Returnera Created-svar med korrekt URL
            return Results.Created($"/api/books/{_book.Id}", response);
        }

        private async static Task<IResult> UpdateBook(IBookRepo _bookrepo, int id, Book _book)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            // Hitta boken att uppdatera
            var existingBook = await _bookrepo.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                response.ErrorMessage.Add("Book not found");
                return Results.NotFound(response);
            }

            // Uppdatera bokens egenskaper
            existingBook.Title = _book.Title;
            existingBook.Author = _book.Author;
            existingBook.YearPublished = _book.YearPublished;
            existingBook.Genre = _book.Genre;
            existingBook.Description = _book.Description;
            existingBook.IsAvailable = _book.IsAvailable;

            // Spara uppdateringarna
            await _bookrepo.UpdateBookAsync(existingBook);
            await _bookrepo.SaveAsync();

            // Skapa ett positivt svar
            response.Result = existingBook;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            // Returnera OK-svar med den uppdaterade boken
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteBook(IBookRepo _bookrepo, int id)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var book = await _bookrepo.GetBookByIdAsync(id);
            if (book == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }

            await _bookrepo.DeleteBookAsync(book);
            await _bookrepo.SaveAsync();

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }


    }
}
