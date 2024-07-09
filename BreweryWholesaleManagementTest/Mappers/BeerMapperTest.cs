using BreweryWholesaleManagement.Mappers;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace BreweryWholesaleManagementTest.Mappers
{
    [TestFixture]
    internal class BeerMapperTest
    {
        private Mock<IBeerMapper> _beerMapperMock;

        [SetUp]
        public void Setup()
        {
            _beerMapperMock = new Mock<IBeerMapper>();
        }

        [Test]
        public void Map_ValidAddBeerBrewerRequest_ReturnsBeer()
        {
            // Arrange
            var addBeerBrewerRequest = new AddBeerBrewerRequest
            {
                Name = "Sample Beer",
                AlcoholContent = 5.0,
                Price = 10.0m
            };

            var expectedBeer = new Beer
            {
                Name = "Sample Beer",
                AlcoholContent = 5.0,
                Price = 10.0m
            };

            _beerMapperMock.Setup(mapper => mapper.Map(It.IsAny<AddBeerBrewerRequest>())).Returns(expectedBeer);

            // Act
            var actualBeer = _beerMapperMock.Object.Map(addBeerBrewerRequest);

            // Assert
            Assert.That(actualBeer.Name, Is.EqualTo(expectedBeer.Name));
            Assert.That(actualBeer.AlcoholContent, Is.EqualTo(expectedBeer.AlcoholContent));
            Assert.That(actualBeer.Price, Is.EqualTo(expectedBeer.Price));
        }

        [Test]
        public void Map_InvalidAddBeerBrewerRequest_ThrowsValidationException()
        {
            // Arrange
            var addBeerBrewerRequest = new AddBeerBrewerRequest
            {
                Name = null, // Invalid Name
                AlcoholContent = 150.0, // Invalid AlcoholContent
                Price = 0.0m // Invalid Price
            };

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => ValidateRequest(addBeerBrewerRequest));
            Assert.That(ex.Message, Is.EqualTo("The Name field is required."));
        }

        private void ValidateRequest(AddBeerBrewerRequest request)
        {
            var validationContext = new ValidationContext(request, serviceProvider: null, items: null);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);
        }
    }
}
