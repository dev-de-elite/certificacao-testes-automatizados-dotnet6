using AutoMapper;
using DeckGenerator.Application.Interfaces.Repositories;
using DeckGenerator.Application.Services;
using DeckGenerator.Domain.Entities;
using DeckGenerator.Domain.Models;
using Moq;
using FluentAssertions;

namespace DeckGenerator.Application.Tests.Services;

public class DeckServiceTest
{
    [Test]
    public async Task AddNewDeckAsync_Deve_Inserir_Sucesso()
    {
        //Arrange
        var mapper = new Mock<IMapper>(MockBehavior.Strict);
        mapper.Setup(x => x.Map<Deck>(It.IsAny<DeckDto>()))
            .Returns(new Deck());

        var repository = new Mock<IDeckRepository>(MockBehavior.Strict);
        repository.Setup(x => x.AddAsync(It.IsAny<Deck>()))
            .Callback<Deck>(d => d.Guid = Guid.NewGuid())
            .Returns(() => Task.FromResult(0));
        repository.Setup(x => x.SaveChangesAsync())
            .Returns(() => Task.FromResult(0));

        var deckService = new DeckService(
            mapper.Object,
            repository.Object,
            default!);
        var deck = new DeckDto();

        //Act
        var sucesso = await deckService.AddNewDeckAsync(deck);

        //Assert
        sucesso.Should().BeTrue();
        deck.Guid.Should().NotBeEmpty();

        mapper.Verify(x => x.Map<Deck>(It.IsAny<DeckDto>()), Times.Once());
        repository.Verify(x => x.AddAsync(It.IsAny<Deck>()), Times.Once());
        repository.Verify(x => x.SaveChangesAsync(), Times.Once());
    }

    [Test]
    public async Task GetByGuidAsync_Deve_Retornar_Sucesso()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var mapper = new Mock<IMapper>(MockBehavior.Strict);
        mapper.Setup(x => x.Map<DeckDto>(It.IsAny<Deck>()))
            .Returns((Deck deck) => new DeckDto { Guid = deck.Guid });

        var repository = new Mock<IDeckRepository>(MockBehavior.Strict);
        repository.Setup(x => x.GetByGuidAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid guid) => new Deck { Guid = guid });

        var deckService = new DeckService(
            mapper.Object,
            repository.Object,
            default!);

        //Act
        var dto = await deckService.GetByGuidAsync(guid);

        //Assert
        dto.Should().NotBeNull();
        dto?.Guid.Should().Be(guid);
    }

    [Test]
    public async Task GetByGuidAsync_Deve_Retornar_Falha()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var mapper = new Mock<IMapper>(MockBehavior.Strict);

        var repository = new Mock<IDeckRepository>(MockBehavior.Strict);
        repository.Setup(x => x.GetByGuidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Deck);

        var deckService = new DeckService(
            mapper.Object,
            repository.Object,
            default!);

        //Act
        var dto = await deckService.GetByGuidAsync(guid);

        //Assert
        dto.Should().BeNull();
    }
}
