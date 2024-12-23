using FastEndpoints;
using FluentValidation;
using WebApplication1.Dto.Currencies;

namespace WebApplication1.API.Currencies;

public class AddCurrencyValidator : Validator<AddCurrencyDto>
{
    public AddCurrencyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required")
            .MinimumLength(3)
            .WithMessage("Code must be at least 3 characters")
            .MaximumLength(5)
            .WithMessage("Code must be at most 5 characters");

        RuleFor(x => x.IsoCode)
            .NotEmpty()
            .WithMessage("IsoCode is required")
            .Length(3)
            .WithMessage("IsoCode must be 3 characters");

        RuleFor(x => x.CurrencyName)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters")
            .MaximumLength(30)
            .WithMessage("Name must be at most 30 characters");

        RuleFor(x => x.CultureCode)
            .NotEmpty()
            .WithMessage("Culture Code is required")
            .MaximumLength(10)
            .WithMessage("Culture Code must be at most 10 characters");

        RuleFor(x => x.CurrencySymbol)
            .MaximumLength(3)
                .When(x => !string.IsNullOrEmpty(x.CurrencySymbol))
            .WithMessage("Symbol must be empty or at most 3 characters");

        RuleFor(x => x.DecimalPlaces)
            .InclusiveBetween(0, 2)
            .WithMessage("DecimalPlaces must be between 0 and 2");


    }
}

