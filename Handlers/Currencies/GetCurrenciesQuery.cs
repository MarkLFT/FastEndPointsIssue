using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;

namespace WebApplication1.Handlers.Currencies;

public record GetCurrenciesQuery() : IQuery<Result<IEnumerable<CurrencyCode>>>;
