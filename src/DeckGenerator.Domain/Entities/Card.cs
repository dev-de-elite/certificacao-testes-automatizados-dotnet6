namespace DeckGenerator.Domain.Entities;

public class Card : BaseEntity
{
    public string Value { get; set; } = default!;

    public virtual Deck Deck { get; set; } = default!;
}