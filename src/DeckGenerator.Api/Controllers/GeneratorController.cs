using DeckGenerator.Application.Extensions;
using DeckGenerator.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeckGenerator.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class GeneratorController : BaseDeckApiController
{
    private readonly ILogger<GeneratorController> _logger;

    public GeneratorController(ILogger<GeneratorController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> Get([FromServices] IDeckService deckService)
    {
        var decks = await deckService.GetAllAsync();

        if (decks is null)
        {
            return DeckNotFound();
        }

        return Ok(decks);
    }

    [HttpPost]
    [Route("new/shuffle")]
    public async Task<IActionResult> NewShuffle(
            [FromServices] IDeckGeneratorService deckGeneratorService,
            [FromServices] IDeckService deckService
        )
    {
        var defaultDeck = deckGeneratorService.CreateDefaultDeckShuffled();

        var result = await deckService.AddNewDeckAsync(defaultDeck);

        if (!result)
        {
            return InternalError();
        }

        return Ok(defaultDeck.Guid);
    }

    [HttpPost]
    [Route("custom/new/shuffle/")]
    public async Task<IActionResult> CustomNewShuffle(
            [FromBody] string[] customCards,
            [FromServices] IDeckGeneratorService deckGeneratorService,
            [FromServices] IDeckService deckService
        )
    {
        var defaultDeck = deckGeneratorService.CreateCustomDeckSuffled(customCards);

        var result = await deckService.AddNewDeckAsync(defaultDeck);

        if (!result)
        {
            return InternalError();
        }

        return Ok(defaultDeck.Guid);
    }

    [HttpGet]
    [Route("{deckGuid}")]
    public async Task<IActionResult> Get(
            string deckGuid,
            [FromServices] IDeckService deckService
        )
    {
        if (!Guid.TryParse(deckGuid, out var guid))
        {
            return BadRequest();
        }

        var deck = await deckService.GetByGuidAsync(guid);

        if (deck is null)
        {
            return DeckNotFound();
        }

        return Ok(deck);
    }

    [HttpPut]
    [Route("{deckGuid}/draw")]
    public async Task<IActionResult> Draw(
            string deckGuid,
            [FromServices] IDeckService deckService
        )
    {
        if (!Guid.TryParse(deckGuid, out var guid))
        {
            return BadRequest();
        }

        var deck = await deckService.GetByGuidAsync(guid);

        if (deck is null)
        {
            return DeckNotFound();
        }

        var draw = deck.Draw();
        if (string.IsNullOrEmpty(draw))
        {
            return DeckIsEmpty();
        }

        await deckService.UpdateDeckAsync(deck);

        return Ok(draw);
    }

    [HttpPut]
    [Route("{deckGuid}/set/draw/{amount}")]
    public async Task<IActionResult> SetDraw(
            string deckGuid,
            int amount,
            [FromServices] IDeckService deckService
        )
    {
        if (!Guid.TryParse(deckGuid, out var guid))
        {
            return BadRequest();
        }

        var deck = await deckService.GetByGuidAsync(guid);

        if (deck is null)
        {
            return DeckNotFound();
        }

        var draws = deck.DrawSet(amount);
        if (draws?.Any() == false)
        {
            return DeckIsEmpty();
        }

        await deckService.UpdateDeckAsync(deck);

        return Ok(draws);
    }
}
