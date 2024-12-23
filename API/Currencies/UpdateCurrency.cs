using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RM.Data.Svc.Finance;
using WebApplication1.Dto.Currencies;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.API.Currencies;

public class UpdateCurrency(IMediator mediator, IMapper mapper) : Endpoint<UpdateCurrencyDto, Result<CurrencyDto>>
{
    public override void Configure()
    {
        Policies("finance-user");
        Put(ApiConstants.Routes.CurrenciesRoute);
        Summary(s =>
        {
            s.Summary = "Update Currency.";
        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(UpdateCurrencyDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCurrencyCommand(request), ct);

        if (result.IsSuccess)
        {
            var currencyDto = mapper.Map<CurrencyCode, CurrencyDto>(result.Value);

            await SendOkAsync(currencyDto, ct);
        }
        else
            await this.SendResponseAsync(result, r => result.Value);
    }
}



