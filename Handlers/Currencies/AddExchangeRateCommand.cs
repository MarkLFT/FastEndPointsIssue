using Ardalis.Result;
using WebApplication1.Behaviours;

namespace WebApplication1.Handlers.Currencies;

public record AddExchangeRateCommand(string Code, DateTime DateValidFrom, decimal ExchangeRate) : ICommand<Result>;
