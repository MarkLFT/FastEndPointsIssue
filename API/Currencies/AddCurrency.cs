using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RM.Data.Svc.Finance;
using WebApplication1.Dto.Currencies;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;
using IMapper = AutoMapper.IMapper;

namespace WebApplication1.API.Currencies;

public class AddCurrency(IMediator mediator, IMapper mapper) : Endpoint<AddCurrencyDto, Result<CurrencyDto>>
{
    public override void Configure()
    {
        Policies("finance-user");
        Post(ApiConstants.Routes.CurrenciesRoute);
        Summary(s =>
        {
            s.Summary = "Add Currency.";
        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(AddCurrencyDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddCurrencyCommand(request), ct);

        if (result.IsSuccess)
        {
            var dto = mapper.Map<CurrencyCode, CurrencyDto>(result.Value);
            await SendOkAsync(Result.Success(dto), ct);
        }
        else
            await this.SendResponseAsync(result, r => result.Value);

    }
}

