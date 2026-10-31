using DeckGenerator.Application.Services;
using DeckGenerator.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using FluentAssertions;

namespace DeckGenerator.Application.Tests.Services;

public class DeckGeneratorServiceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SuffleDeck_Deve_Embaralhar()
    {
        //Arrange
        var mock = new Mock<IMemoryCache>(MockBehavior.Strict);
        var service = new DeckGeneratorService(mock.Object);

        var deck = new DeckDto { Cards = new Stack<string>() };
        deck.Cards.Push("Carta1");
        deck.Cards.Push("Carta2");
        deck.Cards.Push("Carta3");

        //Act
        service.SuffleDeck(deck);

        Action act = () => throw new InvalidCastException(
            "Mensagem de erro",
            new ArgumentException("Mensagem de erro interno", nameof(DeckDto.Cards)));

        act.Should().Throw<InvalidCastException>()
            .WithMessage("Mensagem de erro")
            .WithInnerException<ArgumentException>()
                .WithMessage($"Mensagem de erro interno (Parameter '{nameof(DeckDto.Cards)}')")
                .WithParameterName(nameof(DeckDto.Cards));

        Action argumentException = () => throw new ArgumentNullException(nameof(DeckDto.Cards));

        argumentException.Should().Throw<ArgumentNullException>().WithParameterName(nameof(DeckDto.Cards));


        var nulo = (int?)null;
        nulo.Should().HaveValue()
            .And.BeNull()
            .And.NotBeNull()
            .And.BeGreaterThan(0)
            .And.BeInRange(1, 10)
            .And.Match(x => x % 2 == 0)
            .And.BeNegative()
            .And.BePositive();

        //Assert
        deck.Cards.Should()
            .NotBeNull()
            .And.HaveCountLessThan(4)
            .And.HaveCount(3);
        deck.Cards.Peek()
            .Should()
            .NotBeEmpty()
            .And.NotBe("Carta3");
    }

    [Test]
    public void CreateDefaultDeck_Deve_Gerar_Baralho()
    {
        //Arrange
        var mock = new Mock<IMemoryCache>(MockBehavior.Strict);
        var tamanhoBaralho = 52;
        object expectedValue = new string[tamanhoBaralho];
        mock.Setup(m => m.TryGetValue(It.IsAny<object>(), out expectedValue))
            .Returns(true);
        var service = new DeckGeneratorService(mock.Object);

        //Act
        var deck = service.CreateDefaultDeck();

        //Assert
        deck.Cards.Should().NotBeEmpty().And.HaveCount(tamanhoBaralho);
        mock.Verify(x => x.TryGetValue(It.IsAny<object>(), out expectedValue), Times.Once());
    }
}