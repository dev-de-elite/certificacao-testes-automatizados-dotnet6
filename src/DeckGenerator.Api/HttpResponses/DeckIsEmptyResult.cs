using Microsoft.AspNetCore.Mvc;

namespace DeckGenerator.Api.HttpResponses;

public class DeckIsEmptyResult : NotFoundObjectResult
{
    public DeckIsEmptyResult() : base("Deck is empty")
    {
    }
}