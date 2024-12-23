using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;
using WebApplication1.Data;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.Handlers.Currencies;

internal class AddCurrencyCommandHandler(
    IRepository repository,
    IMapper mapper
    ) : ICommandHandler<AddCurrencyCommand, Result<CurrencyCode>>
{
    public async Task<Result<CurrencyCode>> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currencyCode = mapper.Map<CurrencyCode>(request.CurrencyDto);

        await repository.InsertUpdateEntityAsync<CurrencyCode>(currencyCode, cancellationToken);

        var result = await repository.SaveChangesAsync(cancellationToken);

        if (result)
        {
            var currency = await repository.GetCurrencyByCodeAsync(currencyCode.Code, cancellationToken);

            if (currency is null)
                return Result<CurrencyCode>.Error("Currency not found after adding.");

            return Result<CurrencyCode>.Success(currency);
        }
        else
            return Result<CurrencyCode>.Error();
    }
}

