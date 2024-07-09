using BreweryWholesaleManagement.Mappers;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace BreweryWholesaleManagementTest.Mappers
{
    [TestFixture]
    internal class WholesalerStockMapperTest
    {
        private Mock<IWholesalerStockMapper> _wholesalerStockMapperMock;

        [SetUp]
        public void Setup()
        {
            _wholesalerStockMapperMock = new Mock<IWholesalerStockMapper>();
        }

        [Test]
        public void Map_ValidBeerSaleRequest_ReturnsWholesalerStock()
        {
            // Arrange
            int wholesalerId = 1;
            var beerSaleRequest = new BeerSaleRequest
            {
                BeerId = 1,
                Quantity = 10
            };

            var expectedStock = new WholesalerStock
            {
                WholesalerId = wholesalerId,
                BeerId = beerSaleRequest.BeerId,
                Quantity = beerSaleRequest.Quantity
            };

            _wholesalerStockMapperMock.Setup(mapper => mapper.Map(wholesalerId, beerSaleRequest)).Returns(expectedStock);

            // Act
            var actualStock = _wholesalerStockMapperMock.Object.Map(wholesalerId, beerSaleRequest);

            // Assert
            Assert.That(actualStock.WholesalerId, Is.EqualTo(expectedStock.WholesalerId));
            Assert.That(actualStock.BeerId, Is.EqualTo(expectedStock.BeerId));
            Assert.That(actualStock.Quantity, Is.EqualTo(expectedStock.Quantity));
        }

        [Test]
        public void Map_ValidBeerStockRequest_ReturnsWholesalerStock()
        {
            // Arrange
            int wholesalerId = 1;
            int beerId = 1;
            var beerStockRequest = new BeerStockRequest
            {
                Quantity = 20
            };

            var expectedStock = new WholesalerStock
            {
                WholesalerId = wholesalerId,
                BeerId = beerId,
                Quantity = beerStockRequest.Quantity
            };

            _wholesalerStockMapperMock.Setup(mapper => mapper.Map(wholesalerId, beerId, beerStockRequest)).Returns(expectedStock);

            // Act
            var actualStock = _wholesalerStockMapperMock.Object.Map(wholesalerId, beerId, beerStockRequest);

            // Assert
            Assert.That(actualStock.WholesalerId, Is.EqualTo(expectedStock.WholesalerId));
            Assert.That(actualStock.BeerId, Is.EqualTo(expectedStock.BeerId));
            Assert.That(actualStock.Quantity, Is.EqualTo(expectedStock.Quantity));
        }

        [Test]
        public void Map_InvalidBeerSaleRequest_ThrowsValidationException()
        {
            // Arrange
            var beerSaleRequest = new BeerSaleRequest
            {
                BeerId = 1,
                Quantity = -5 // Invalid Quantity
            };

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => ValidateRequest(beerSaleRequest));
            Assert.That(ex.Message, Is.EqualTo("The field Quantity must be between 1 and 2147483647."));
        }

        [Test]
        public void Map_InvalidBeerStockRequest_ThrowsValidationException()
        {
            // Arrange
            var beerStockRequest = new BeerStockRequest
            {
                Quantity = -10 // Invalid Quantity
            };

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => ValidateRequest(beerStockRequest));
            Assert.That(ex.Message, Is.EqualTo("The field Quantity must be between 0 and 2147483647."));
        }

        private void ValidateRequest(object request)
        {
            var validationContext = new ValidationContext(request, serviceProvider: null, items: null);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);
        }
    }
}
