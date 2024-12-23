using Ardalis.Result;
using FastEndpoints;
using MediatR;
using WebApplication1.Extensions;
using WebApplication1.Handlers.Currencies;

namespace WebApplication1.API.Currencies;

public class DeleteCurrency(IMediator mediator) : Endpoint<DeleteCurrencyRequest, Result>
{
    public override void Configure()
    {
        Policies("finance-user");
        Delete(ApiConstants.Routes.CurrenciesDeleteRoute);
        Summary(s =>
        {
            s.Summary = "Delete Currency By Currency Id.";

        });
        Tags("Currencies");
    }

    public override async Task HandleAsync(DeleteCurrencyRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new DeleteCurrencyCommand(request.CurrencyId), ct);

        if (result.IsSuccess)
        {
            await SendOkAsync(Result.Success(), ct);
        }
        else if (result.IsNotFound())
            await SendNotFoundAsync(ct);
        else
            await this.SendResponseAsync(result, r => result.Value);
    }
}

