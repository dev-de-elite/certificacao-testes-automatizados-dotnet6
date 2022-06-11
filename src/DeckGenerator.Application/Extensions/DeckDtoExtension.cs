using DeckGenerator.Domain.Entities;
using DeckGenerator.Domain.Models;

namespace DeckGenerator.Application.Extensions;

public static class DeckDtoExtension
{
    public static string Draw(this DeckDto deck)
    {
        if (deck.Cards is null || !deck.Cards.Any()) return string.Empty;

        var card = deck.Cards.Pop();
        return card;
    }

    public static IEnumerable<string> DrawSet(this DeckDto deck, int amount)
    {
        if (deck.Cards is null || !deck.Cards.Any()) return Enumerable.Empty<string>();

        var draws = deck.Cards.PopRange(amount);
        return draws;
    }
}