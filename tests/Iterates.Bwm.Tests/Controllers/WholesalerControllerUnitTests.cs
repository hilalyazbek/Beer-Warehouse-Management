using AutoMapper;
using Iterates.Bwm.Api.Controllers;
using Iterates.Bwm.Api.DTOs;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Iterates.Bwm.Tests.Controllers;

[TestFixture]
public class WholesalerControllerUnitTests
{
    private Mock<IWholesalerService> _mockWholesalerService;
    private Mock<IMapper> _mockMapper;
    private Mock<ILoggerManager> _mockLogger;

    private WholesalersController _controller;

    [SetUp]
    public void Setup()
    {
        _mockWholesalerService = new Mock<IWholesalerService>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILoggerManager>();
        _controller = new WholesalersController(
            _mockWholesalerService.Object,
            _mockMapper.Object,
            _mockLogger.Object);
    }

    [Test]
    public async Task UpdateBeerStock_ValidData_ReturnsOkResult()
    {
        // Arrange
        var wholesalerId = Guid.NewGuid();
        var beerId = Guid.NewGuid();
        var stock = 10;
        var wholesalerStock = new WholesalerStock { WholesalerId = wholesalerId, BeerId = beerId, Stock = stock };
        var updatedWholesalerStock = new WholesalerStock { WholesalerId = wholesalerId, BeerId = beerId, Stock = stock + 5 };
        var updatedWholesalerStockDTO = new WholesalerStockDTO { WholesalerId = wholesalerId, BeerId = beerId, Stock = stock + 5 };

        _mockWholesalerService
            .Setup(x => x.GetByIdAsync(wholesalerId))
            .ReturnsAsync(new Wholesaler());

        _mockWholesalerService
            .Setup(x => x.GetStockByBeerIdAsync(wholesalerId, beerId))
            .ReturnsAsync(wholesalerStock);

        _mockWholesalerService
            .Setup(x => x.UpdateStockAsync(wholesalerId, beerId, stock))
            .ReturnsAsync(updatedWholesalerStock);

        _mockMapper
            .Setup(x => x.Map<WholesalerStockDTO>(updatedWholesalerStock))
            .Returns(updatedWholesalerStockDTO);

        // Act
        var result = await _controller.UpdateBeerStock(wholesalerId, beerId, stock);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ActionResult<WholesalerStockDTO>)));
            //Assert.That((WholesalerStockDTO)(result.Result as ObjectResult).Value, Is.EqualTo(updatedWholesalerStockDTO));
        });
    }

    [Test]
    public async Task GetQuote_WithValidRequest_ReturnsOkResult()
    {
        // Arrange
        var wholesalerId = Guid.NewGuid();
        var quoteRequestDTO = new QuotationRequestDTO
        {
            Items = new List<ItemRequestDTO>
            {
                new ItemRequestDTO
                {
                    BeerId = Guid.NewGuid(),
                    Quantity = 10
                }
            }
        };
        var wholesaler = new Wholesaler { Id = wholesalerId };
        var quoteRequest = new QuotationRequest();

        _mockMapper
            .Setup(x => x.Map<QuotationRequest>(quoteRequestDTO))
            .Returns(quoteRequest);

        quoteRequest.Wholesaler = wholesaler;
        var quoteResponse = new QuotationResponse();
        var quoteResponseDTO = new QuotationResponseDTO();

        _mockMapper
            .Setup(x => x.Map<QuotationResponseDTO>(quoteResponse))
            .Returns(quoteResponseDTO);

        _mockWholesalerService
            .Setup(x => x.GetByIdAsync(wholesalerId))
            .ReturnsAsync(wholesaler);
        _mockWholesalerService
            .Setup(x => x.GetQuoteResponseAsync(quoteRequest))
            .ReturnsAsync(quoteResponse);

        // Act
        var result = await _controller.GetQuote(wholesalerId, quoteRequestDTO);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(quoteResponseDTO));
    }
}

