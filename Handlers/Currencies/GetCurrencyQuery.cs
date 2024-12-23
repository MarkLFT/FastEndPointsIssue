using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;

namespace WebApplication1.Handlers.Currencies;

public record GetCurrencyQuery(int? CurrencyId, string? CurrencyCode) : IQuery<Result<CurrencyCode>>;
