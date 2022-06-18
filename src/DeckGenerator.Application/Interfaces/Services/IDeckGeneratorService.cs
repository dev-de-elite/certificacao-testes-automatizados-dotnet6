using DeckGenerator.Domain.Models;

namespace DeckGenerator.Application.Interfaces.Services;

public interface IDeckGeneratorService
{
    DeckDto CreateDefaultDeck();
    DeckDto CreateDefaultDeckShuffled();
    DeckDto CreateCustomDeck(string[] cards);
    DeckDto CreateCustomDeckSuffled(string[] cards);
    void SuffleDeck(DeckDto deck);
}