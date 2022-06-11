using System.Diagnostics.CodeAnalysis;
using DeckGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeckGenerator.Infastructure.Context;

public class DataContext : DbContext
{
    public DataContext([NotNull] DbContextOptions<DataContext> options) : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; } = default!;
    public virtual DbSet<Deck> Decks { get; set; } = default!;
}