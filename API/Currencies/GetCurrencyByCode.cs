using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RM.Data.Svc.Finance;
using WebApplication1.Dto.Currencies;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.API.Currencies;

public class GetCurrencyByCode(IMediator mediator, IMapper mapper) : Endpoint<GetCurrencyByCodeRequest, Result<CurrencyDto>>
{
    public override void Configure()
    {
        Policies("finance-user");
        Get(ApiConstants.Routes.CurrenciesByCodeRoute);
        Summary(s =>
        {
            s.Summary = "Get Currency By Currency Code.";

        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(GetCurrencyByCodeRequest request, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(request.CurrencyCode))
        {
            AddError("CurrencyCode", "Currency Code is required.");
            ThrowIfAnyErrors();
        }

        var currency = await mediator.Send(new GetCurrencyQuery(null, request.CurrencyCode), ct);

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

