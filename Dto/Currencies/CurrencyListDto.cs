namespace WebApplication1.Dto.Currencies;
public record CurrencyListDto(int Id, string Code, string CurrencyName, string CurrencySymbol, decimal ActiveExchangeRate);
