using FastEndpoints;
using FluentValidation;

namespace WebApplication1.API.Currencies;

public class DeleteCurrencyValidator : Validator<DeleteCurrencyRequest>
{
    public DeleteCurrencyValidator()
    {
        RuleFor(x => x.CurrencyId).GreaterThan(0);
    }
}
