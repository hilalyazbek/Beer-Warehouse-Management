using AutoMapper;
using Iterates.Bwm.Api.Controllers;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Iterates.Bwm.Tests.Controllers;

[TestFixture]
public class BrewersControllerUnitTests
{
    private Mock<IBrewerService> _mockBrewerService;
    private Mock<IWholesalerService> _mockWholesalerService;
    private Mock<IMapper> _mockMapper;
    private Mock<ILoggerManager> _mockLogger;

    private BrewersController _controller;

    [SetUp]
    public void Setup()
    {
        _mockBrewerService = new Mock<IBrewerService>();
        _mockWholesalerService = new Mock<IWholesalerService>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILoggerManager>();

        _controller = new BrewersController(
            _mockBrewerService.Object,
            _mockWholesalerService.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Test]
    public async Task GetBrewers_ReturnsOkObjectResult_WhenModelStateIsValid()
    {
        // Arrange
        _mockBrewerService
            .Setup(service => service.GetAllBrewersAsync())
            .ReturnsAsync(new List<Brewer>());

        _mockMapper
            .Setup(mapper => mapper.Map<IEnumerable<BrewerDTO>>(It.IsAny<IEnumerable<Brewer>>()))
            .Returns(new List<BrewerDTO>());

        // Act
        var result = await _controller.GetBrewers();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetBrewers_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("key", "error message");

        // Act
        var result = await _controller.GetBrewers();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task GetBrewers_ReturnsOkResultWithBrewers_WhenBrewersExist()
    {
        // Arrange
        var brewers = new List<Brewer>
        {
            new Brewer { Id = Guid.NewGuid(), Name = "Brewer 1" },
            new Brewer { Id = Guid.NewGuid(), Name = "Brewer 2" },
            new Brewer { Id = Guid.NewGuid(), Name = "Brewer 3" },
        };
        _mockBrewerService
            .Setup(s => s.GetAllBrewersAsync())
            .ReturnsAsync(brewers);

        var brewerDTOs = brewers.Select(b => new BrewerDTO { Id = b.Id, Name = b.Name });

        _mockMapper
            .Setup(m => m.Map<IEnumerable<BrewerDTO>>(brewers))
            .Returns(brewerDTOs);

        // Act
        var result = await _controller.GetBrewers();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = (OkObjectResult)result.Result;
        Assert.That(okResult.Value, Is.EqualTo(brewerDTOs));
    }

    [Test]
    public async Task DeleteBeer_ValidBrewerIdAndBeerId_ReturnsOkResult()
    {
        // Arrange
        var brewerId = Guid.NewGuid();
        var beerId = Guid.NewGuid();
        var brewer = new Brewer { Id = brewerId, Name = "TestBrewer" };
        var beerToDelete = new Beer { Id = beerId };

        _mockBrewerService
            .Setup(x => x.GetBrewerAsync(brewerId))
            .ReturnsAsync(brewer);

        _mockBrewerService
            .Setup(x => x.GetBeerAsync(brewerId, beerId))
            .ReturnsAsync(beerToDelete);

        _mockBrewerService
            .Setup(x => x.DeleteBeerAsync(beerToDelete))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBeer(brewerId, beerId);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo($"Beer with ID {beerId} deleted"));
    }

    [Test]
    public async Task DeleteBeer_InvalidBrewerId_ReturnsNotFoundResult()
    {
        // Arrange
        var brewerId = Guid.NewGuid();
        var beerId = Guid.NewGuid();

        _mockBrewerService
            .Setup(x => x.GetBrewerAsync(brewerId))
            .ReturnsAsync((Brewer)null);

        // Act
        var result = await _controller.DeleteBeer(brewerId, beerId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        var notFoundResult = (NotFoundObjectResult)result;
        Assert.That(notFoundResult.Value, Is.EqualTo($"Brewer with id {brewerId} not found"));
    }

    [Test]
    public async Task DeleteBeer_InvalidBeerId_ReturnsNotFoundResult()
    {
        // Arrange
        var brewerId = Guid.NewGuid();
        var beerId = Guid.NewGuid();
        var brewer = new Brewer { Id = brewerId, Name = "TestBrewer" };

        _mockBrewerService
            .Setup(x => x.GetBrewerAsync(brewerId))
            .ReturnsAsync(brewer);

        _mockBrewerService
            .Setup(x => x.GetBeerAsync(brewerId, beerId))
            .ReturnsAsync((Beer)null);

        // Act
        var result = await _controller.DeleteBeer(brewerId, beerId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        var notFoundResult = (NotFoundObjectResult)result;
        Assert.That(notFoundResult.Value, Is.EqualTo($"Beer with ID {beerId} not found"));
    }
}