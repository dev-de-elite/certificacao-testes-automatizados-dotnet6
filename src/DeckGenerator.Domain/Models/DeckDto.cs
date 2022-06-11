namespace DeckGenerator.Domain.Models;

public class DeckDto
{
    public Guid Guid { get; set; } = default!;
    public Stack<string> Cards { get; set; } = default!;
}