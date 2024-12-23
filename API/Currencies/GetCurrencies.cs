using System.Collections.ObjectModel;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RM.Data.Svc.Finance;
using WebApplication1.Dto.Currencies;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.API.Currencies;

public class GetCurrencies(IMediator mediator, IMapper mapper) : EndpointWithoutRequest<Result<IReadOnlyCollection<CurrencyListDto>>>
{
    public override void Configure()
    {
        Policies("finance-user");
        Get(ApiConstants.Routes.CurrenciesRoute);
        Summary(s =>
        {
            s.Summary = "Get Currencies.";
        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var currencies = await mediator.Send(new GetCurrenciesQuery(), ct);

        if (currencies.IsSuccess)
        {
            var dtoList = mapper.Map<IEnumerable<CurrencyCode>, IEnumerable<CurrencyListDto>>(currencies.Value);

            await SendOkAsync(new ReadOnlyCollection<CurrencyListDto>(dtoList.ToList()), ct);
        }
        else if (currencies.IsNotFound())
            await SendNotFoundAsync(ct);
        else
        {
            var dtoList = new ReadOnlyCollection<CurrencyListDto>([]);
            await this.SendResponseAsync(currencies, r => currencies.Value);
        }

    }
}



