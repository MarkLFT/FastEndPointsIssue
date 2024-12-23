using Ardalis.Result;
using WebApplication1.Behaviours;

namespace WebApplication1.Handlers.Currencies;

public record DeleteCurrencyCommand(int currencyId) : ICommand<Result>;
