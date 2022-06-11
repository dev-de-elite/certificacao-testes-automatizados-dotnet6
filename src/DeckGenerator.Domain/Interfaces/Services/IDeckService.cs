using DeckGenerator.Domain.Models;

namespace DeckGenerator.Domain.Interfaces.Services;

public interface IDeckService
{
    Task<bool> AddNewDeckAsync(DeckDto deck);
    Task<DeckDto?> GetByGuidAsync(Guid guid);
    Task<bool> UpdateDeckAsync(DeckDto deck);
    Task<IEnumerable<DeckDto>> GetAllAsync();
}