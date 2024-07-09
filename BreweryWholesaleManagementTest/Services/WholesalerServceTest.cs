using BreweryWholesaleManagement.Helpers;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Repositories;
using BreweryWholesaleManagement.Services;
using Moq;

namespace BreweryWholesaleManagementTest.Services
{
    internal class WholesalerServceTest
    {
        private Mock<IWholesalerRepository> _wholesalerRepositoryMock;
        private Mock<IQuoteService> _quoteServiceMock;
        private IWholesalerService _wholesalerService;

        [SetUp]
        public void SetUp()
        {
            _wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            _quoteServiceMock = new Mock<IQuoteService>();
            _wholesalerService = new WholesalerService(_wholesalerRepositoryMock.Object, _quoteServiceMock.Object);
        }

        [Test]
        public async Task AddBeerSaleAsync_Should_Call_Repository()
        {
            var wholesalerStock = new WholesalerStock();

            await _wholesalerService.AddBeerSaleAsync(wholesalerStock);

            _wholesalerRepositoryMock.Verify(r => r.AddBeerSaleAsync(wholesalerStock), Times.Once);
        }

        [Test]
        public async Task GetBeerByWholesalerAsync_Should_Return_Correct_Data()
        {
            int wholesalerId = 1;
            int pageIndex = 1;
            int pageSize = 10;
            var expectedData = new PaginatedList<BeerWholesalerModelView>(new List<BeerWholesalerModelView>(), 0, pageIndex, pageSize);

            _wholesalerRepositoryMock.Setup(r => r.GetBeerByWholesalerAsync(wholesalerId, pageIndex, pageSize))
                .ReturnsAsync(expectedData);

            var result = await _wholesalerService.GetBeerByWholesalerAsync(wholesalerId, pageIndex, pageSize);

            Assert.That(result, Is.EqualTo(expectedData));
        }

        [Test]
        public async Task GetQuoteAsync_Should_Throw_Exception_For_NonExistent_Wholesaler()
        {
            int wholesalerId = 1;
            var quoteRequest = new QuoteRequest();

            _wholesalerRepositoryMock.Setup(r => r.FindById(wholesalerId))
                .ReturnsAsync((WholeSalerModelView)null);

            var ex = Assert.ThrowsAsync<Exception>(() => _wholesalerService.GetQuoteAsync(wholesalerId, quoteRequest));

            Assert.That(ex.Message, Is.EqualTo(Constantes.ErrorMessages.WholesalerMustExist));
        }

        [Test]
        public async Task GetQuoteAsync_Should_Throw_Exception_For_Empty_Order()
        {
            int wholesalerId = 1;
            var quoteRequest = new QuoteRequest();

            _wholesalerRepositoryMock.Setup(r => r.FindById(wholesalerId))
                .ReturnsAsync(new WholeSalerModelView());

            var ex = Assert.ThrowsAsync<Exception>(() => _wholesalerService.GetQuoteAsync(wholesalerId, quoteRequest));

            Assert.That(ex.Message, Is.EqualTo(Constantes.ErrorMessages.OrderCannotBeEmpty));
        }

        [Test]
        public async Task UpdateBeerStockAsync_Should_Call_Repository()
        {
            var wholesalerStock = new WholesalerStock();

            await _wholesalerService.UpdateBeerStockAsync(wholesalerStock);

            _wholesalerRepositoryMock.Verify(r => r.UpdateBeerStockAsync(wholesalerStock), Times.Once);
        }
    }
}
