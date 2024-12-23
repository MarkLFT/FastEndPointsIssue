using FastEndpoints;
using FluentValidation;
using WebApplication1.Dto.Currencies;

namespace WebApplication1.API.Currencies;

public class UpdateCurrencyValidator : Validator<UpdateCurrencyDto>
{
    public UpdateCurrencyValidator()
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
            .Must(value => BeEmptyOrMaxLength(value, 3))
            .WithMessage("Symbol must be empty or at most 3 characters");

        RuleFor(x => x.DecimalPlaces)
            .Must(value => value >= 0 && value <= 4)
            .WithMessage("DecimalPlaces must be between 0 and 2");


    }

    private static bool BeEmptyOrMaxLength(string value, int length)
    {
        // Return true if the value is null, empty, or has max length characters
        return string.IsNullOrEmpty(value) || value.Length <= length;
    }
}

