using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DeckGenerator.Api.HttpResponses;

[DefaultStatusCode(500)]
public class InternalErrorResult : ObjectResult
{
    public InternalErrorResult(string message) : base(message)
    {
    }
}