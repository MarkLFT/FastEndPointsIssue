using Ardalis.Result;
using WebApplication1.Behaviours;
using WebApplication1.Data;

namespace WebApplication1.Handlers.Currencies;

internal class DeleteCurrencyCommandHandler(IRepository repository) : ICommandHandler<DeleteCurrencyCommand, Result>
{
    public async Task<Result> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currencyCode = await repository.GetCurrencyByIdAsync(request.currencyId, cancellationToken);

        if (currencyCode is null)
            return Result.NotFound();

        repository.Delete(currencyCode);

        var result = await repository.SaveChangesAsync(cancellationToken);

        if (result)
        {
            return Result.Success();
        }
        else
            return Result.Error();
    }
}
