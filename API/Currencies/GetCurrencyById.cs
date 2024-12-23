using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RM.Data.Svc.Finance;
using WebApplication1.Dto.Currencies;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.API.Currencies;

public class GetCurrencyById(IMediator mediator, IMapper mapper) : Endpoint<GetCurrencyByIdRequest, Result<CurrencyDto>>
{
    public override void Configure()
    {
        Policies("finance-user");
        Get(ApiConstants.Routes.CurrenciesByIdRoute);
        Summary(s =>
        {
            s.Summary = "Get Currency by Id.";

        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(GetCurrencyByIdRequest request, CancellationToken ct)
    {
        if (request.currencyId == 0)
        {
            AddError("currencyId is required.");
            ThrowIfAnyErrors();
        }

        var currency = await mediator.Send(new GetCurrencyQuery(request.currencyId, string.Empty), ct);

        if (currency.IsSuccess)
        {
            var currencyDto = mapper.Map<CurrencyCode, CurrencyDto>(currency.Value);

            await SendOkAsync(currencyDto, ct);
        }
        else if (currency.IsNotFound())
            await SendNotFoundAsync(ct);
        else
            await this.SendResponseAsync(currency, r => currency.Value);

    }
}




