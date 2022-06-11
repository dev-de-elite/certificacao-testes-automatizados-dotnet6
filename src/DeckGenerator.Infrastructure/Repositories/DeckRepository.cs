using System.Diagnostics.CodeAnalysis;
using DeckGenerator.Domain.Entities;
using DeckGenerator.Domain.Interfaces.Repositories;
using DeckGenerator.Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DeckGenerator.Infastructure.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly DataContext _context;

    public DeckRepository([NotNull] DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Deck deck) => await _context.Decks.AddAsync(deck);

    public async Task DeleteAsync(long id)
    {
        var deck = await GetByIdAsync(id);
        if (deck is not null)
            _context.Decks.Remove(deck);
    }

    public async Task<IEnumerable<Deck>> GetAllAsync() =>
        await _context.Decks
            .Include(x => x.Cards)
            .ToListAsync();

    public async Task<Deck?> GetByIdAsync(long id) =>
        await _context.Decks
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Deck?> GetByGuidAsync(Guid guid) =>
        await _context.Decks
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Guid == guid);


    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public void Update(Deck deck) => _context.Update(deck);
}