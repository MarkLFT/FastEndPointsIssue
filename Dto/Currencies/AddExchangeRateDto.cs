namespace WebApplication1.Dto.Currencies;

public record AddExchangeRateDto(string Code, DateTime DateValidFrom, decimal ExchangeRate);