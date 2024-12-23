using Ardalis.Result;
using RM.Data.Svc.Finance;
using WebApplication1.Behaviours;
using WebApplication1.Data;
using WebApplication1.Dto.Currencies;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.Handlers.Currencies;

internal class UpdateCurrencyCommandHandler(
    IRepository repository,
    IMapper mapper
    ) : ICommandHandler<UpdateCurrencyCommand, Result<CurrencyCode>>
{
    public async Task<Result<CurrencyCode>> Handle(UpdateCurrencyCommand request, CancellationToken ct)
    {
        var currency = await repository.GetCurrencyByIdAsync(request.currencyDto.Id, ct);

        if (currency is null)
            return Result<CurrencyCode>.NotFound();

        mapper.Map<UpdateCurrencyDto, CurrencyCode>(request.currencyDto, currency);

        await repository.InsertUpdateEntityAsync<CurrencyCode>(currency, ct);

        var result = await repository.SaveChangesAsync(ct);

        if (result)
        {
            return Result<CurrencyCode>.Success(currency);
        }
        else
            return Result<CurrencyCode>.Error();
    }
}
