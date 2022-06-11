using DeckGenerator.Domain.Interfaces.Services;
using DeckGenerator.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DeckGenerator.Application.Services;

public class DeckGeneratorService : IDeckGeneratorService
{
    private readonly IMemoryCache _cache;

    public DeckGeneratorService(IMemoryCache cache)
    {
        _cache = cache;
    }

    private string[] GenerateCards()
    {
        var result = _cache.GetOrCreate("DeckGeneratorService", context =>
        {
            // Define o tempo de expiração para o cache
            context.SetAbsoluteExpiration(TimeSpan.FromHours(1));

            var cardNotation = new[] { "A", "K", "Q", "J", "T", "9", "8", "7", "6", "5", "4", "3", "2" };
            var cardSuitedness = new[] { "s", "c", "h", "d" };

            var cards = new string[cardNotation.Length * cardSuitedness.Length];
            var position = 0;
            for (var i = 0; i < cardNotation.Length; i++)
            {
                var notation = cardNotation[i];
                for (var j = 0; j < cardSuitedness.Length; j++)
                {
                    var suitedness = cardSuitedness[j];
                    cards[position++] = $"{notation}{suitedness}";
                }
            }

            return cards;
        });
        return result;
    }

    public DeckDto CreateCustomDeck(string[] cards)
    {
        var deck = new DeckDto { Guid = Guid.NewGuid(), Cards = new Stack<string>(cards) };
        return deck;
    }

    public DeckDto CreateCustomDeckSuffled(string[] cards)
    {
        var deck = CreateCustomDeck(cards);
        SuffleDeck(deck);
        return deck;
    }

    public DeckDto CreateDefaultDeck() => new DeckDto { Guid = Guid.NewGuid(), Cards = new Stack<string>(GenerateCards()) };

    public DeckDto CreateDefaultDeckShuffled()
    {
        var deck = CreateDefaultDeck();
        SuffleDeck(deck);
        return deck;
    }

    public void SuffleDeck(DeckDto deck)
    {
        var random = new Random();
        var cardsArray = deck.Cards.ToArray();
        var maxCount = cardsArray.Length - 1;
        for (var i = 0; i < maxCount; i++)
        {
            var j = random.Next(i, maxCount);
            var swap = cardsArray[j];
            cardsArray[j] = cardsArray[i];
            cardsArray[i] = swap;
        }
        deck.Cards = new Stack<string>(cardsArray);
    }
}