using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.Application.Dtos;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using RealState.Controllers;
using Xunit;

public class ListingControllerTests
{
    private ListingController GetControllerWithUser(Mock<IListingService> svcMock)
    {
        var ctrl = new ListingController(svcMock.Object);
        // simulate authenticated user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "BrokerX")
        }, "mock"));
        ctrl.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
        return ctrl;
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithListings()
    {
        // Arrange
        var dtoList = new List<ListingDto> {
            new ListingDto { Id=Guid.NewGuid(), Location="CityA" }
        };
        var svcMock = new Mock<IListingService>();
        svcMock.Setup(x => x.GetAllAsync(null, null, null, null))
               .ReturnsAsync(dtoList);

        var ctrl = GetControllerWithUser(svcMock);

        // Act
        var actionResult = await ctrl.GetAll(null, null, null, null) as OkObjectResult;

        // Assert
        actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        var resp = actionResult.Value as ApiResponse;
        resp.IsSuccess.Should().BeTrue();
        (resp.Result as List<ListingDto>).Should().BeEquivalentTo(dtoList);
    }

    [Fact]
    public async Task GetById_NotFound_WhenMissing()
    {
        // Arrange
        var svcMock = new Mock<IListingService>();
        svcMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync((ListingDto?)null);

        var ctrl = GetControllerWithUser(svcMock);

        // Act
        var result = await ctrl.GetById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Create_ReturnsCreated()
    {
        // Arrange
        var dto = new ListingDto { Id = Guid.NewGuid(), Location = "CityD" };
        var svcMock = new Mock<IListingService>();
        svcMock.Setup(x => x.CreateAsync(It.IsAny<CreateListingDto>(), "BrokerX"))
               .ReturnsAsync(dto);

        var ctrl = GetControllerWithUser(svcMock);

        // Act
        var result = await ctrl.Create(new CreateListingDto
        {
            PropertyType = "Home",
            Location = "CityD",
            Price = 100
        }) as CreatedAtActionResult;

        // Assert
        result.StatusCode.Should().Be((int)HttpStatusCode.Created);
        var resp = result.Value as ApiResponse;
        resp.IsSuccess.Should().BeTrue();
        (resp.Result as ListingDto)!.Id.Should().Be(dto.Id);
    }
}
