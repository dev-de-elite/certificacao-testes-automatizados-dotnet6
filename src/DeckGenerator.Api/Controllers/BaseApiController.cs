using DeckGenerator.Api.HttpResponses;
using Microsoft.AspNetCore.Mvc;

namespace DeckGenerator.Api.Controllers;

[ApiController]
public class BaseDeckApiController : ControllerBase
{
    protected DeckNotFoundResult DeckNotFound() => new();
    protected DeckIsEmptyResult DeckIsEmpty() => new();
    protected InternalErrorResult InternalError(string message = "Error when try to process the request.") => new(message);
    public override BadRequestObjectResult BadRequest(object? error = null) => base.BadRequest(error ?? "Wrong format for GUID");
}