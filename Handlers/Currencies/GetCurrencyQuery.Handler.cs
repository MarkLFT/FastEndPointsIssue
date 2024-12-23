using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;
using WebApplication1.Data;

namespace WebApplication1.Handlers.Currencies;

internal class GetCurrencyQueryHandler(
    IRepository repository
    ) : IQueryHandler<GetCurrencyQuery, Result<CurrencyCode>>
{
    public async Task<Result<CurrencyCode>> Handle(GetCurrencyQuery query, CancellationToken ct)
    {
        CurrencyCode? currency = null;

        if (query.CurrencyId.HasValue)
            currency = await repository.GetCurrencyByIdAsync(query.CurrencyId.Value, ct);
        else if (!string.IsNullOrEmpty(query.CurrencyCode))
            currency = await repository.GetCurrencyByCodeAsync(query.CurrencyCode, ct);

        if (currency is null)
        {
            return Result<CurrencyCode>.NotFound();
        }

        return Result<CurrencyCode>.Success(currency);

    }
}
