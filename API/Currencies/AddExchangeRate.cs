using Ardalis.Result;
using FastEndpoints;
using MediatR;
using WebApplication1.Dto.Currencies;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;

namespace WebApplication1.API.Currencies;

public class AddExchangeRate(IMediator mediator) : Endpoint<AddExchangeRateDto, Result>
{
    public override void Configure()
    {
        Policies("finance-user");
        Post(ApiConstants.Routes.CurrenciesAddExchangeRateRoute);
        Summary(s =>
        {
            s.Summary = "Add Currency Exchange Rate.";
        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(AddExchangeRateDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddExchangeRateCommand(request.Code, request.DateValidFrom, request.ExchangeRate), ct);

        if (result.IsSuccess)
        {
            await SendOkAsync(Result.Success(), ct);
        }
        else
            await this.SendResponseAsync(result, r => result.Value);

    }
}

