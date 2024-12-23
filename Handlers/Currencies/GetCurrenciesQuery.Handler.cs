using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;
using WebApplication1.Data;

namespace WebApplication1.Handlers.Currencies;

internal class GetCurrenciesQueryHandler(
    IRepository repository
    ) : IQueryHandler<GetCurrenciesQuery, Result<IEnumerable<CurrencyCode>>>
{
    public async Task<Result<IEnumerable<CurrencyCode>>> Handle(GetCurrenciesQuery query, CancellationToken cancellationToken)
    {
        var currencies = await repository.GetCurrenciesAsync(cancellationToken);

        if (currencies is null || !currencies.Any())
        {
            return Result<IEnumerable<CurrencyCode>>.NotFound();
        }

        return Result<IEnumerable<CurrencyCode>>.Success(currencies);

    }
}
