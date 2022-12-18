using FluentValidation;


namespace Application.Features.Movies.Commands.CreatePageData;

public class CreatePageDataCommandValidator : AbstractValidator<CreatePageDataCommand>
{
    public CreatePageDataCommandValidator()
    {

        //RuleFor(p => p.PageData.Total_pages)
        //   .NotEmpty().WithMessage("{Id} is required.")
        //   .GreaterThan(0).WithMessage("{Id} should be greater than zero.");
    }
}

