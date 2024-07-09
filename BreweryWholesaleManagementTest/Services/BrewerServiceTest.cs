using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Services;
using Moq;

namespace BreweryWholesaleManagementTest.Services
{
    [TestFixture]
    public class BrewerServiceTests
    {
        private Mock<IBrewerService> _brewerServiceMock;
        private List<Beer> _beers;

        [SetUp]
        public void Setup()
        {
            _brewerServiceMock = new Mock<IBrewerService>();

            _brewerServiceMock.Setup(x => x.AddBeerAsync(It.IsAny<int>(), It.IsAny<Beer>()))
                              .Returns(Task.CompletedTask);

            _brewerServiceMock.Setup(x => x.DeleteBeerAsync(It.IsAny<int>(), It.IsAny<int>()))
                              .Returns(Task.CompletedTask);

            _beers = new List<Beer>
            {
                new Beer { Id = 1, Name = "Beer 1", AlcoholContent = 5.0, Price = 10.0m },
                new Beer { Id = 2, Name = "Beer 2", AlcoholContent = 4.5, Price = 9.0m },
                new Beer { Id = 3, Name = "Beer 3", AlcoholContent = 4.8, Price = 8.5m }
            };

            _brewerServiceMock.Setup(x => x.GetBeerByBrewerAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                              .Returns((int brewerId, int pageIndex, int pageSize) =>
                              {
                                  var beerModels = _beers.Select(b => new BeerModelView
                                  {
                                      Id = b.Id,
                                      Name = b.Name,
                                      AlcoholContent = b.AlcoholContent,
                                      Price = b.Price
                                  }).ToList();
                                  return Task.FromResult(new PaginatedList<BeerModelView>(beerModels, _beers.Count, pageIndex, pageSize));
                              });
        }

        [Test]
        public async Task AddBeerAsync_ShouldSucceed()
        {
            // Arrange
            int brewerId = 1;
            var beer = new Beer { Id = 4, Name = "New Beer", AlcoholContent = 4.2, Price = 9.5m };

            // Act
            await _brewerServiceMock.Object.AddBeerAsync(brewerId, beer);

            // Assert
            _brewerServiceMock.Verify(x => x.AddBeerAsync(brewerId, beer), Times.Once);
        }

        [Test]
        public async Task DeleteBeerAsync_ShouldSucceed()
        {
            // Arrange
            int breweryId = 1;
            int beerId = 1;

            // Act
            await _brewerServiceMock.Object.DeleteBeerAsync(breweryId, beerId);

            // Assert
            _brewerServiceMock.Verify(x => x.DeleteBeerAsync(breweryId, beerId), Times.Once);
        }

        [Test]
        public async Task GetBeerByBrewerAsync_ShouldReturnPaginatedList()
        {
            // Arrange
            int brewerId = 1;
            int pageIndex = 0;
            int pageSize = 2;

            // Act
            var result = await _brewerServiceMock.Object.GetBeerByBrewerAsync(brewerId, pageIndex, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.TotalPages, Is.EqualTo(2));
        }
    }
}
