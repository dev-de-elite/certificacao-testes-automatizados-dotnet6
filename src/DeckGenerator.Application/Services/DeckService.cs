using AutoMapper;
using DeckGenerator.Domain.Entities;
using DeckGenerator.Application.Interfaces.Repositories;
using DeckGenerator.Application.Interfaces.Services;
using DeckGenerator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DeckGenerator.Application.Services;

internal class DeckService : IDeckService
{
    private readonly IMapper _mapper;
    private readonly IDeckRepository _repository;
    private readonly ILogger<DeckService> _logger;

    public DeckService(
        IMapper mapper,
        IDeckRepository repository,
        ILogger<DeckService> logger
        )
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> AddNewDeckAsync(DeckDto deck)
    {
        try
        {
            var entity = _mapper.Map<Deck>(deck);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            deck.Guid = entity.Guid;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Não foi possível adicionar o novo deck.");
            return false;
        }

    }

    public async Task<DeckDto?> GetByGuidAsync(Guid guid)
    {
        try
        {
            var entity = await _repository.GetByGuidAsync(guid);

            if (entity is null) return default;

            return _mapper.Map<DeckDto>(entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Não foi possível recuperar o deck usando GUID ({guid}) fornecido.");
            return default;
        }
    }

    public async Task<bool> UpdateDeckAsync(DeckDto deck)
    {
        try
        {
            var entity = await _repository.GetByGuidAsync(deck.Guid);

            if (entity is null) return false;

            var cards = new List<Card>();
            foreach (var cardValue in deck.Cards)
            {
                var card = entity.Cards.FirstOrDefault(x => x.Value == cardValue);
                if (card is null)
                {
                    card = new Card { Value = cardValue };
                }
                cards.Add(card);
            }

            entity.Cards = cards;
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Não foi possível atualizar o deck usando GUID ({deck.Guid}) fornecido.");
            return false;
        }
    }

    public async Task<IEnumerable<DeckDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<DeckDto>>(entities);
        return dtos;
    }
}