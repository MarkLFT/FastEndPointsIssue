using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;
using WebApplication1.Data;

namespace WebApplication1.Handlers.Currencies;

internal class AddExchangeRateCommandHandler(IRepository repository) : ICommandHandler<AddExchangeRateCommand, Result>
{
    public async Task<Result> Handle(AddExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var currencyCode = await repository.GetCurrencyByCodeAsync(request.Code, cancellationToken);

        if (currencyCode is null)
            return Result.NotFound();

        currencyCode.AddExchangeRate(request.DateValidFrom, request.ExchangeRate);

        await repository.InsertUpdateEntityAsync<CurrencyCode>(currencyCode, cancellationToken);

        var result = await repository.SaveChangesAsync(cancellationToken);

        if (result)
        {
            return Result.Success();
        }
        else
            return Result.Error();
    }
}

