namespace DeckGenerator.Domain.Entities;

public class Deck : BaseEntity
{
    public Guid Guid { get; set; } = default!;
    public virtual IEnumerable<Card> Cards { get; set; } = default!;
}