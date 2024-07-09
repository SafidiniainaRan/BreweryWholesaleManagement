using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryWholesaleManagementTest.Services
{
    internal class QuoteServiceTest
    {
        private IQuoteService _quoteService;

        [SetUp]
        public void Setup()
        {
            _quoteService = new QuoteService();
        }

        [Test]
        public void GetAmountToBePaied_Returns_Correct_Amount()
        {
            // Arrange
            double discount = 0.1;
            decimal totalPrice = 100;

            // Act
            decimal amountToBePaid = _quoteService.GetAmountToBePaied(discount, totalPrice);

            // Assert
            Assert.That(amountToBePaid, Is.EqualTo(90));
        }

        [Test]
        public void GetDiscount_Returns_Correct_Discount()
        {
            // Arrange & Act
            double discount1 = _quoteService.GetDiscount(5);
            double discount2 = _quoteService.GetDiscount(15);
            double discount3 = _quoteService.GetDiscount(25);

            // Assert
            Assert.That(discount1, Is.EqualTo(0));
            Assert.That(discount2, Is.EqualTo(0.1));
            Assert.That(discount3, Is.EqualTo(0.2));
        }

        [Test]
        public void GetTotalPrice_Returns_Correct_Total_Price()
        {
            // Arrange
            var beers = new List<BeerQuoteModelView>
        {
            new BeerQuoteModelView { Quantity = 2, UnitPrice = 10 },
            new BeerQuoteModelView { Quantity = 3, UnitPrice = 15 },
            new BeerQuoteModelView { Quantity = 5, UnitPrice = 8 }
        };

            // Act
            decimal totalPrice = _quoteService.GetTotalPrice(beers);

            // Assert
            Assert.That(totalPrice, Is.EqualTo(beers.Sum(x => x.Quantity * x.UnitPrice)));
        }
    }
}
