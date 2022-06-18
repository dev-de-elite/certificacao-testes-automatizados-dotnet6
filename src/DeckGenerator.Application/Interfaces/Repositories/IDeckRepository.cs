using DeckGenerator.Domain.Entities;

namespace DeckGenerator.Application.Interfaces.Repositories;

public interface IDeckRepository
{
    Task<IEnumerable<Deck>> GetAllAsync();
    Task<Deck?> GetByIdAsync(long id);
    Task<Deck?> GetByGuidAsync(Guid guid);
    Task AddAsync(Deck deck);
    void Update(Deck deck);
    Task DeleteAsync(long id);
    Task SaveChangesAsync();
}