using AutoMapper;
using DeckGenerator.Domain.Entities;
using DeckGenerator.Domain.Models;

namespace DeckGenerator.Application.Automapper;

public class DtoToEntityProfile : Profile
{
    public DtoToEntityProfile()
    {
        CreateMap<DeckDto, Deck>()
            .ForMember(c => c.Cards, opt => opt.MapFrom(x => x.Cards.Select(c => new Card { Value = c })));
    }
}