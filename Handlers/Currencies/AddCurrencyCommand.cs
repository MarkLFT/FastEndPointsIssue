using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;
using WebApplication1.Dto.Currencies;

namespace WebApplication1.Handlers.Currencies;

public record AddCurrencyCommand(AddCurrencyDto CurrencyDto) : ICommand<Result<CurrencyCode>>;
