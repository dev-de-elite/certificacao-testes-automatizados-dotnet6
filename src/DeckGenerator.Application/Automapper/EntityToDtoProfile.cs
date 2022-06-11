using AutoMapper;
using DeckGenerator.Domain.Entities;
using DeckGenerator.Domain.Models;

namespace DeckGenerator.Application.Automapper;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<Card, DrawDto>();
        CreateMap<Deck, DeckDto>()
            .ForMember(c => c.Cards, opt => opt.MapFrom(x => new Stack<string>(x.Cards.Select(c => c.Value))));
    }
}