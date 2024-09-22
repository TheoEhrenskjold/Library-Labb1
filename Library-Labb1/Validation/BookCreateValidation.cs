using FluentValidation;
using Library_Labb1.Models;

namespace Library_Labb1.Validation
{
    public class BookCreateValidation : AbstractValidator<Book>
    {
        public BookCreateValidation()
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Author).NotEmpty();
        }
    }
}
