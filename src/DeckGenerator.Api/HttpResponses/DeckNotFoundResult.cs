using Microsoft.AspNetCore.Mvc;

namespace DeckGenerator.Api.HttpResponses;

public class DeckNotFoundResult : NotFoundObjectResult
{
    public DeckNotFoundResult() : base("Deck not found")
    {
    }
}