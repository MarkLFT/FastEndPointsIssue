using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;

namespace WebApplication1.Extensions;
internal static class ResultsExtensions
{
    public static async Task SendResponseAsync<TResult, TResponse>(this IEndpoint ep, TResult result, Func<TResult, TResponse> mapper) where TResult : Ardalis.Result.IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await ep.HttpContext.Response.SendAsync(mapper(result));
                break;

            case ResultStatus.Invalid:
                ep.ValidationFailures.AddRange(result.ValidationErrors.Select(e => new ValidationFailure(e.Identifier, e.ErrorMessage)).ToList());
                await ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures);
                break;
        }
    }
}
