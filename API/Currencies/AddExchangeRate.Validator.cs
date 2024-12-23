using FastEndpoints;
using FluentValidation;
using WebApplication1.Dto.Currencies;

namespace WebApplication1.API.Currencies;

public class AddExchangeRateValidator : Validator<AddExchangeRateDto>
{
    public AddExchangeRateValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required")
            .MinimumLength(3)
            .WithMessage("Code must be at least 3 characters")
            .MaximumLength(5)
            .WithMessage("Code must be at most 5 characters");

        RuleFor(x => x.ExchangeRate)
            .GreaterThan(0)
            .WithMessage("Exchange rate must be greater than 0");
    }
}

